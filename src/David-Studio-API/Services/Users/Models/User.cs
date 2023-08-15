using System.ComponentModel.DataAnnotations;

namespace Users.Models
{
    public class User
    {
        [Key]
        [Required]
        public string Id { get; set; } = null!;

        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string PasswordConfirm { get; set; } = null!;


        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }
    }
}
