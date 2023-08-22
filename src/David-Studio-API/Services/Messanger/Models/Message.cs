using System.ComponentModel.DataAnnotations;

namespace Messanger.Models
{
    public class Message
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string Body { get; set; } = null!;

        public bool IsReaded { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
