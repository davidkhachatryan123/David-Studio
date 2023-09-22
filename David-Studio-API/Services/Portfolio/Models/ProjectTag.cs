using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class ProjectTag
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        public int TagId { get; set; }
    }
}

