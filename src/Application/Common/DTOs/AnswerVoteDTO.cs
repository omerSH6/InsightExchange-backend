namespace Application.Common.DTOs
{
    public class AnswerVoteDTO : VoteDTO
    {
        public required int AnswerId { get; set; }
    }
}
