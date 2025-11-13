using quiz.hub.Application.DTOs.Auth;

namespace quiz.hub.Application.Interfaces.IServices.Authentication
{
    public interface IAuthService
    {
        Task<AuthenticatedUserDTO> Register(RegisterDTO dto);
        Task<AuthenticatedUserDTO> Login(LoginDTO dto);
    }
}
