using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class TopProject
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int Rank { get; set; }

        [JsonIgnore]
        public virtual Project Project { get; set; } = null!;
    }
}

