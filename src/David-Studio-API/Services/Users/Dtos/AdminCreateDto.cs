using System.ComponentModel.DataAnnotations;

namespace Users.Dtos
{
    public class AdminCreateDto
    {
        public string Username { get; set; } = null!;

        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The passwords do  not match")]
        public string PasswordConfirmation { get; set; } = null!;

        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }
    }
}
