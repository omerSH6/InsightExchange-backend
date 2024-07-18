namespace Application.Common.DTOs
{
    public class QuestionPreviewDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required string PreviewContent { get; set; }
        public required DateTime CreatedAt { get; set; }
        public required UserDTO User { get; set; }
        public required bool WasAskedByCurrentUser { get; set; }
        public required bool WasVotedByCurrentUser { get; set; }
        public required int TotalVotes { get; set; }
        public required int TotalAnswers { get; set; }
        public required List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}
