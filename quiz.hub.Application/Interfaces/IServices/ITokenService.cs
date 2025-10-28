using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface ITokenService
    {
        Task<CreateTokenDTO> CreateTokenAsync(ApplicationUser user);

    }
}
