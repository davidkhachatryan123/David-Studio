namespace IdentityServer.Dtos
{
    public class MfaCodeDto
    {
        public string Code { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
