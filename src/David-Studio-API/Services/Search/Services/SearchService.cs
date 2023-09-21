using AutoMapper;
using Nest;
using Search.Dtos;
using Search.Models;
using Services.Common.Models;

namespace Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly ElasticClient _client;
        private readonly IMapper _mapper;

        public SearchService(
            ElasticClient client,
            IMapper mapper)
        {
            _client = client;
            _mapper = mapper;
        }

        public async Task<PageData<ProjectDto>> SearchAsync(SearchModelDto searchModel)
        {
            QueryContainerDescriptor<Project> queryForText = new();

            if (searchModel.SearchText is null)
                queryForText.MatchAll();
            else
                queryForText.MatchPhrasePrefix(mpp => mpp
                    .Field(f => f.Name)
                    .Query(searchModel.SearchText)
                    .Slop(3)
                );


            QueryContainerDescriptor<Tag> queryForTags = new();

            if (searchModel.TagIds is null)
                queryForTags.MatchAll();
            else
                queryForTags.Terms(t => t
                    .Field(f => f.Id)
                    .Terms(searchModel.TagIds)
                );


            var response = await _client.SearchAsync<Project>(s => s
                .Size(0)
                .Query(q => queryForText && q
                    .HasChild<Tag>(c => c
                        .Query(q => queryForTags)
                     )
                )
                .Aggregations(aggs => aggs
                    .Terms("projects", t => t
                        .Field(f => f.Id)
                        .Aggregations(aggs => aggs
                            .Sum("rank", s => s
                                .Field(f => f.Rank)
                            )
                            .BucketSort("sorted_projects", bs => bs
                                .Sort(s => s
                                    .Descending("rank")
                                )
                                .From((searchModel.Page - 1) * searchModel.Count)
                                .Size(searchModel.Count)
                            )
                            .TopHits("project", th => th
                                .Size(1)
                            )
                            .Children<Tag>("tags", c => c
                                .Aggregations(aggs => aggs
                                    .TopHits("results", th => th
                                        .Size(searchModel.TagsLimit)
                                    )
                                )
                            )
                        )
                    )
                )
            );

            return new PageData<ProjectDto>()
            {
                Entities = response.Aggregations
                    .Terms("projects")
                    .Buckets.Select(b =>
                    {
                        ProjectDto project = _mapper.Map<ProjectDto>(
                            b.TopHits("project")
                            .Hits<Project>()
                            .FirstOrDefault()
                            !.Source
                        );

                        project.Tags = _mapper.Map<List<TagDto>>(b
                            .Children("tags")
                            .TopHits("results")
                            .Documents<Tag>()
                            .ToArray()
                        );

                        return project;
                    }),
                TotalCount = Convert.ToInt32(response.Total)
            };
        }

        public async Task<SearchSuggestionsDto> GetSearchSuggestionsAsync(SearchSuggestionsQueryDto searchSuggestionsQuery)
        {
            var response = await _client.SearchAsync<Project>(s => s
                .Size(0)
                .Query(q => q
                    .MatchPhrasePrefix(m => m
                        .Field(f => f.Name)
                        .Query(searchSuggestionsQuery.SearchText)
                    )
                ).
                Aggregations(a => a
                    .Filter("projects", fp => fp
                        .Filter(f => f
                            .Term(t => t
                                .Field(f => f.Join)
                                .Value(nameof(Project).ToLower())
                            )
                        )
                        .Aggregations(a => a
                            .Terms("unique", tu => tu
                                .Field(f => f.Id)
                                .Size(searchSuggestionsQuery.MaxProjects)
                                .Aggregations(a => a
                                    .TopHits("results", t => t
                                        .Size(1)
                                    )
                                )
                            )
                        )
                    )
                    .Filter("tags", ft => ft
                        .Filter(f => f
                            .Term(t => t
                                .Field(f => f.Join)
                                .Value(nameof(Tag).ToLower())
                            )
                        )
                        .Aggregations(a => a
                            .Terms("unique", tu => tu
                                .Field(f => f.Id)
                                .Size(searchSuggestionsQuery.MaxTags)
                                .Aggregations(a => a
                                    .TopHits("results", t => t
                                        .Size(1)
                                    )
                                )
                            )
                        )
                    )
                )
            );


            return new SearchSuggestionsDto()
            {
                ProjectNames = response.Aggregations
                .Filter("projects")
                .Terms("unique")
                .Buckets.Select(b => b
                    .TopHits("results")
                    .Hits<Project>()
                    .FirstOrDefault()
                    !.Source.Name
                ),

                Tags = response.Aggregations
                .Filter("tags")
                .Terms("unique")
                .Buckets.Select(b => b
                    .TopHits("results")
                    .Hits<Tag>()
                    .FirstOrDefault()
                ).
                Select(t => new TagDto
                {
                    Id = t.Source.Id,
                    Name = t.Source.Name,
                    Color = t.Source.Color
                })
            };
        }
    }
}
