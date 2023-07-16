using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
    public class Tag
    {
        public Tag()
        {
            Projects = new HashSet<Project>();
            ProjectTags = new HashSet<ProjectTag>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression("^#[0-9A-Fa-f]{6,6}$")]
        public string Color { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectTag> ProjectTags { get; set; }
    }
}

