namespace Application.Common.DTOs
{
    public class QuestionVoteDTO : VoteDTO
    {
        public required int QuestionId { get; set; }
    }
}
