using Microsoft.AspNetCore.Identity;
using quiz.hub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public byte[]? Cover { get; set; }

        // Nav
        public ICollection<Host> Hosts { get; set; } = new List<Host>();
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
