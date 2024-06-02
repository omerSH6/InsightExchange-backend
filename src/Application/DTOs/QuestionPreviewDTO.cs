﻿namespace Application.DTOs
{
    public class QuestionPreviewDTO
    {
        public int Id { get; set; }
        public required string PreviewContent { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO User { get; set; }
        public bool WasAskedByCurrentUser { get; set; }
        public bool WasVotedByCurrentUser { get; set; }
        public int TotalVotes { get; set; }
        public int TotalAnswers { get; set; }
        public List<TagDTO> Tags { get; set; } = new List<TagDTO>();
    }
}