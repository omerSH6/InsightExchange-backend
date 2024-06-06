namespace Application.DTOs
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO User { get; set; }
        public bool WasAskedByCurrentUser { get; set; }
        public bool WasVotedByCurrentUser { get; set; }
        public int TotalVotes { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}
