namespace Application.DTOs
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO User { get; set; }
        public bool WasReipaiedByCurrentUser { get; set; }
        public bool WasVotedByCurrentUser { get; set; }
        public int TotalVotes { get; set; }
    }
}
