using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class InsightExchangeDbContext : DbContext
    {
        public InsightExchangeDbContext(DbContextOptions<InsightExchangeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<AnswerVote> AnswerVote { get; set; }
        public DbSet<QuestionVote> QuestionVote { get; set; }
    }
}
