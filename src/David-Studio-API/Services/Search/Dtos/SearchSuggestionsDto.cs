namespace Search.Dtos
{
    public class SearchSuggestionsDto
    {
        public IEnumerable<string> ProjectNames { get; set; } = null!;
        public IEnumerable<TagDto> Tags { get; set; } = null!;
    }
}
