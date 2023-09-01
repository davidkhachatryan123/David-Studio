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

        public IFormFile? File { get; set; }

        [Required]
        public List<Tag> Tags { get; set; } = new();
    }
}

