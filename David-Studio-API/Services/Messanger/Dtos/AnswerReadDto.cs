namespace Messanger.Dtos
{
    public class AnswerReadDto
    {
        public int Id { get; set; }
        public string Body { get; set; } = null!;

        public DateTime AnsweredDate { get; set; }
    }
}
