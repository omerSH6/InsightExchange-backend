namespace Domain.Entities
{
    public class AnswerVote : Vote
    {
        public int AnswerId { get; set; }
        public required Answer Answer { get; set; }
    }
}
