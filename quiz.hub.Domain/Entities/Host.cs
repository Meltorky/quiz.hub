using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class Host
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [ForeignKey(nameof(ApplicationUser))]
        public string UserId { get; set; } = default!;


        // nav props
        public ApplicationUser ApplicationUser { get; set; } = null!;   // EF-friendly choice
        public ICollection<Quiz> Quizzes { get; set; } = new List<Quiz>();
    }
}
