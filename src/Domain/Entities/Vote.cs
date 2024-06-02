namespace Domain.Entities
{
    public abstract class Vote
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isPositiveVote { get; set; }
        public int UserId { get; set; }
        public required User User { get; set; }
    }
}
