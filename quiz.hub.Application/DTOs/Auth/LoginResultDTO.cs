using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.DTOs.Auth
{
    public class LoginResultDTO
    {
        public string Name { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public string? Cover { get; set; }
        public CreateTokenDTO CreateToken { get; set; } = default!;
    }
}
