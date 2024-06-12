namespace Application.Common.DTOs
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isPositiveVote { get; set; }
        public int UserId { get; set; }
    }
}
