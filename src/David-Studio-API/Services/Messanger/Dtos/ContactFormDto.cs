using System.ComponentModel.DataAnnotations;

namespace Messanger.Dtos
{
    public class ContactFormDto
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
        public string Body { get; set; } = null!;
    }
}
