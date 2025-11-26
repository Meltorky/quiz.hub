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
                .HasKey(ca => new { ca.CandidateId, ca.QuizId, ca.QuestionId });

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

            builder.Entity<CandidateAnswer>(entity =>
            {
                // Composite Primary Key
                entity.HasKey(ca => new { ca.QuizId, ca.CandidateId, ca.QuestionId });

                // 1. Reuse CandidateId (from PK) as FK to ApplicationUser (Candidate)
                entity.HasOne(ca => ca.Candidate)
                      .WithMany(u => u.CandidateAnswers) // add this collection to ApplicationUser if not exists
                      .HasForeignKey(ca => ca.CandidateId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 2. Reuse (QuizId + CandidateId) as FK to QuizCandidate (bridge table)
                entity.HasOne(ca => ca.QuizCandidate)
                      .WithMany(qc => qc.CandidateAnswers) // add this collection to QuizCandidate
                      .HasForeignKey(ca => new { ca.QuizId, ca.CandidateId })
                      .OnDelete(DeleteBehavior.Restrict);

                // 3. Reuse QuestionId as FK to Question
                entity.HasOne(ca => ca.Question)
                      .WithMany(q => q.CandidateAnswers) // optional: track answers per question
                      .HasForeignKey(ca => ca.QuestionId)
                      .OnDelete(DeleteBehavior.Restrict);

                // 4. AnswerId → Answer
                entity.HasOne(ca => ca.Answer)
                      .WithMany()
                      .HasForeignKey(ca => ca.AnswerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Optional: Add indexes for performance
                entity.HasIndex(ca => ca.AnswerId);
            });

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
