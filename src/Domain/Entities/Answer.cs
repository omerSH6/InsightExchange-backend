namespace Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<AnswerVote> Votes { get; set; } = new List<AnswerVote>();
        public int QuestionId { get; set; }
        public required Question Question { get; set; }
    }
}
