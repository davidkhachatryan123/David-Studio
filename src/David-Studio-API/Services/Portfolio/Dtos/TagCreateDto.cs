using System.ComponentModel.DataAnnotations;

namespace Portfolio.Dtos
{
    public class TagCreateDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Color { get; set; } = null!;
    }
}

