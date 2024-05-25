namespace Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int DiscussionId { get; set; }
        public Question Discussion { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int Rating { get; set; }
    }
}
