namespace Users.Dtos
{
    public class ConfirmEmailRequestDto
    {
        public string UserId { get; set; } = null!;
        public string? ReturnUrl { get; set; }
    }
}
