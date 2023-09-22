namespace Search.Dtos
{
    public class SearchSuggestionsQueryDto
    {
        public string? SearchText { get; set; }

        public int MaxProjects { get; set; } = 5;
        public int MaxTags { get; set; } = 5;
    }
}
