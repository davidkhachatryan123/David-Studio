namespace Search.Dtos
{
    public class SearchSuggestionsQueryDto
    {
        public string SearchText { get; set; } = null!;

        public int MaxProjects { get; set; }
        public int MaxTags { get; set; }
    }
}
