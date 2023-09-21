namespace Search.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Uri Link { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public List<TagDto> Tags { get; set; } = new();
    }
}
