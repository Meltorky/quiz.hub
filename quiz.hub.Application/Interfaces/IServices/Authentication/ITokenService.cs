using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices.Authentication
{
    public interface ITokenService
    {
        Task<CreatedTokenDTO> CreateTokenAsync(ApplicationUser user);

    }
}
