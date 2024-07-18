namespace Application.Common.DTOs
{
    public class QuestionDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required UserDTO User { get; set; }
        public required bool WasAskedByCurrentUser { get; set; }
        public required bool WasVotedByCurrentUser { get; set; }
        public required int TotalVotes { get; set; }
        public required List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
        public required List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}
