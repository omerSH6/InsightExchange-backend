namespace Application.Common.DTOs
{
    public class AnswerDTO
    {
        public required int Id { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required UserDTO User { get; set; }
        public required bool WasReipaiedByCurrentUser { get; set; }
        public required bool WasVotedByCurrentUser { get; set; }
        public required int TotalVotes { get; set; }
    }
}
