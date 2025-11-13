using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class Question
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Quiz))]
        public Guid QuizId { get; set; }
        public string Title { get; set; } = default!;
        public byte[]? Image { get; set; } 
        public double Score { get; set; }
        public int Order { get; set; }

        // nav props
        public Quiz Quiz { get; set; } = null!;
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
