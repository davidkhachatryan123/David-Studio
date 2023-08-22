using System.ComponentModel.DataAnnotations;

namespace Messanger.Dtos
{
    public class MessageReadDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Body { get; set; } = null!;

        public bool IsReaded { get; set; }
        public DateTime SentDate { get; set; }
    }
}
