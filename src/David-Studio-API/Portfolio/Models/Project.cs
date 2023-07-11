using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Portfolio.Controllers;

namespace Portfolio.Models
{
    public class Project
    {
        public Project()
        {
            Tags = new HashSet<Tag>();
            ProjectTags = new HashSet<ProjectTag>();
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public Uri Link { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = null!;

        [JsonIgnore]
        public virtual ICollection<Tag> Tags { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectTag> ProjectTags { get; set; }

        [JsonInclude]
        public virtual TopProject? TopProject { get; set; }
    }
}

