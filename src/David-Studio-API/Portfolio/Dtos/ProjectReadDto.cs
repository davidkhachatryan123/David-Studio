using Portfolio.Models;

namespace Portfolio.Dtos
{
    public class ProjectReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public Uri Link { get; set; } = null!;

        public string ImageUniqueId { get; set; } = null!;

        public List<Tag> Tags { get; } = new();
    }
}

