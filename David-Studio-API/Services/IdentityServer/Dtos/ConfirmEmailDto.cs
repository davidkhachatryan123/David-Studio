namespace IdentityServer.Dtos
{
    public class ConfirmEmailDto
    {
        public string UserId { get; set; } = null!;
        public string Token { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
