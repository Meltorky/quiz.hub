using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class Answer
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Question))]
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = default!;
        public bool IsCorrect { get; set; }

        // Navigation
        public Question Question { get; set; } = default!;
    }

}
