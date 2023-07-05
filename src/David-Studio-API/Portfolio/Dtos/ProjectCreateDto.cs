using Portfolio.Models;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Dtos
{
    public class ProjectCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Uri Link { get; set; } = null!;

        [Required]
        public string ImageUniqueId { get; set; } = null!;

        [Required]
        public List<Tag> Tags { get; set; } = new();
    }
}

