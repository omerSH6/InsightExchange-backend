namespace Domain.Entities
{
    public class QuestionVote : Vote
    {
        public int QuestionId { get; set; }
        public required Question Question { get; set; }
    }
}
