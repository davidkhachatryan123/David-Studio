namespace Messanger.Dtos
{
    public class MessagesListItemDto
    {
        public int Id { get; set; }

        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? PhoneNumber { get; set; }

        public bool IsReaded { get; set; }
        public bool HasAnswer { get; set; }
        public DateTime SentDate { get; set; }
    }
}
