using Search.Dtos;
using Services.Common.Models;

namespace Search.Services
{
    public interface ISearchService
    {
        Task<SearchSuggestionsDto> GetSuggestionsAsync(SearchSuggestionsQueryDto searchSuggestionsQuery);
        Task<PageData<ProjectDto>> SearchAsync(SearchModelDto searchModel);
    }
}
