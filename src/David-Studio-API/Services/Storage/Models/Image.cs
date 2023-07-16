using System.ComponentModel.DataAnnotations;

namespace Storage.Models
{
    public class Image
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string UniqueName { get; set; } = null!;
    }
}

