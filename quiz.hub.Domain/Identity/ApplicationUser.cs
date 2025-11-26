using Microsoft.AspNetCore.Identity;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace quiz.hub.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public byte[]? Cover { get; set; }

        // Nav props

        // The quizzes this user created (as Host)
        public ICollection<Quiz> HostedQuizzes { get; set; } = new List<Quiz>();

        // The quizzes this user joined (as Candidate)
        public ICollection<QuizCandidate> QuizCandidates { get; set; } = new List<QuizCandidate>();

        public ICollection<CandidateAnswer> CandidateAnswers { get; set; } = new List<CandidateAnswer>();
    }
}
