using quiz.hub.Application.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<LoginResultDTO> Register(RegisterDTO dto);
        Task<LoginResultDTO> Login(LoginDTO dto);
    }
}
