using Microsoft.AspNetCore.Mvc;
using Nest;
using Search.Dtos;
using Search.Services;
using Services.Common.Models;

namespace Search.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly ILogger<SearchController> _logger;

        public SearchController(
            ISearchService searchService,
            ILogger<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] SearchModelDto searchModel)
        {
            PageData<ProjectDto> results = await _searchService.SearchAsync(searchModel);

            _logger.LogInformation(
                "Returning {ResultsCount} results by query:" +
                "\n\tText -> '{SearchText}'" +
                "\n\tTags count -> {TagsCount}" +
                "\n\tTags limit -> {TagsLimit}" +
                "\n\tPage -> {Page}" +
                "\n\tPage data count -> {PageDataCount}",
                results.Entities?.Count(),
                searchModel.SearchText ?? "",
                searchModel.TagIds?.Count() ?? 0,
                searchModel.TagsLimit,
                searchModel.Page,
                searchModel.Count);

            return Ok(results);
        }

        [MapToApiVersion("1.0")]
        [HttpGet]
        [Route(nameof(GetSuggestions))]
        public async Task<IActionResult> GetSuggestions([FromQuery] SearchSuggestionsQueryDto searchSuggestionsQuery)
        {
            SearchSuggestionsDto results =
                await _searchService.GetSuggestionsAsync(searchSuggestionsQuery);

            _logger.LogInformation(
                "Returning search suggestions:" +
                "\n\tProjects count -> '{ProjectsCount}'" +
                "\n\tTags count -> {TagsCount}",
                results.ProjectNames.Count(),
                results.Tags.Count());

            return Ok(results);
        }
    }
}
