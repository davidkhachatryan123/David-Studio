namespace IdentityServer.Dtos
{
    public class UserLoginDto
    {
        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool RememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
