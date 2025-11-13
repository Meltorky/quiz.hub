using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.Auth
{
    public class AuthenticatedUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> RoleNames { get; set; } = new();
        public CreatedTokenDTO CreatedToken { get; set; } = default!;
    }
}
