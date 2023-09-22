using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Messanger.Models
{
    public class Answer
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [MinLength(10)]
        [MaxLength(500)]
        public string Body { get; set; } = null!;

        public DateTime AnsweredDate { get; set; } = DateTime.Now;

        [Required]
        public int MessageId { get; set; }

        [JsonIgnore]
        public virtual Message message { get; set; } = null!;
    }
}
