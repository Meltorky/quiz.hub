using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using quiz.hub.Domain.Entities;
using quiz.hub.Domain.Identity;

namespace quiz.hub.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Answer> Answers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<CandidateAnswer> CandidateAnswers { get; set; }
        public DbSet<Host> Hosts { get; set; }
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
                .HasKey(qc => new { qc.QuizId, qc.CandidateId });


            // create non-clustred indexes

            builder.Entity<Candidate>()
                .HasIndex(c => c.Email)
                .IsUnique();

            builder.Entity<Quiz>()
                .HasIndex(c => c.ConnectionCode)
                .IsUnique();


            // restrict all one-many relations as [Hosts > Quizzes > Questions > Answers > QuizCandidates]

            builder.Entity<Host>()
                .HasMany(h => h.Quizzes)
                .WithOne(q => q.Host)
                .HasForeignKey(q => q.HostId)
                .OnDelete(DeleteBehavior.Restrict);

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
                .HasMany(a => a.Hosts)
                .WithOne(h => h.ApplicationUser)
                .HasForeignKey(h => h.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(a => a.Candidates)
                .WithOne(h => h.ApplicationUser)
                .HasForeignKey(h => h.UserId)
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
