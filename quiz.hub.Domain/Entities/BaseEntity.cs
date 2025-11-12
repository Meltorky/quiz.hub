using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid(); 
        
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
