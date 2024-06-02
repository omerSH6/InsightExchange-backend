namespace Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
        public List<QuestionVote> Votes { get; set; } = new List<QuestionVote>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
        public List<Answer> Answers { get; set; } = new List<Answer>();
    }
}
