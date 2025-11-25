using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quiz.hub.Domain.Entities;
using quiz.hub.Domain.Identity;
using System.Reflection.Emit;

namespace quiz.hub.Infrastructure.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizCandidate> QuizCandidates { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // create composite keys

            builder.Entity<CandidateAnswer>()
                .HasKey(ca => new { ca.CandidateId, ca.QuizId, ca.AnswerId });

            builder.Entity<QuizCandidate>()
                .HasKey(qc => new { qc.QuizId, qc.CandidateUserId });


            // create non-clustred indexes

            builder.Entity<Quiz>()
                .HasIndex(c => c.ConnectionCode)
                .IsUnique();


            // restrict all one-many relations as [Hosts > Quizzes > Questions > Answers > QuizCandidates]

            builder.Entity<Quiz>()
                .HasMany(h => h.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Question>()
                .HasMany(h => h.Answers)
                .WithOne(q => q.Question)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.HostedQuizzes)
                .WithOne(h => h.Host)
                .HasForeignKey(h => h.HostUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.QuizCandidates)
                .WithOne(h => h.Candidate)
                .HasForeignKey(h => h.CandidateUserId)
                .OnDelete(DeleteBehavior.Restrict);


            // Customize a special schema for Identity Tables

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }
    }
}
