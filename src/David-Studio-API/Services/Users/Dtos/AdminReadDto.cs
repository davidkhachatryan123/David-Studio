namespace Users.Dtos
{
    public class AdminReadDto
    {
        public string Id { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;
        public bool EmailConfirmed { get; set; }

        public string? PhoneNumber { get; set; }
    }
}
