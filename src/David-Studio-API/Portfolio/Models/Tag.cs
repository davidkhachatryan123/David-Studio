using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class Tag
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression("^#[0-9A-Fa-f]{6,6}$")]
        public string Color { get; set; } = null!;
    }
}

