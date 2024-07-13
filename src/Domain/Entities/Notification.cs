namespace Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public User? User { get; set; }
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
