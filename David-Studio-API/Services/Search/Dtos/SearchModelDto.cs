namespace Search.Dtos
{
    public class SearchModelDto
    {
        public string? SearchText { get; set; }

        public IEnumerable<int>? TagIds { get; set; }
        public int TagsLimit { get; set; } = 25;

        public int Page { get; set; }
        public int Count { get; set; }
    }
}
