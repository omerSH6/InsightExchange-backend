namespace Application.Common.DTOs
{
    public class VoteDTO
    {
        public required int Id { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required bool isPositiveVote { get; set; }
        public required int UserId { get; set; }
    }
}
