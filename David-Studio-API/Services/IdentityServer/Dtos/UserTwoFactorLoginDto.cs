namespace IdentityServer.Dtos
{
    public class UserTwoFactorLoginDto
    {
        public string Code { get; set; } = null!;
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
