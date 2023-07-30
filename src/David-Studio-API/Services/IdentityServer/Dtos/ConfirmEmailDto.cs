namespace IdentityServer.Dtos
{
    public class ConfirmEmailDto
    {
        public string Token { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
