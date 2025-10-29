using Microsoft.AspNetCore.Identity;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }


        public async Task<LoginResultDTO> Register(RegisterDTO dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                throw new DuplicateEmailException($"Email '{dto.Email}' is already registered.");

            StringBuilder userName = new StringBuilder();
            userName.Append($"@{new MailAddress(dto.Email.ToLowerInvariant()).User}");

            bool isUserNameDuplicate = await _userManager.FindByNameAsync(userName.ToString()) is not null;

            ApplicationUser newUser = new ApplicationUser
            {
                Email = dto.Email,
                EmailConfirmed = true,
                Name = dto.FullName,
                UserName = isUserNameDuplicate ? userName.Append('0').ToString() : userName.ToString(),
            };

            var registerResult = await _userManager.CreateAsync(newUser,dto.Password);
            if (!registerResult.Succeeded)
                throw new OperationFailedException("Register Operation Falied !!");

            var asignRoleResult = await _userManager.AddToRoleAsync(newUser,RoleEnums.User.ToString());
            if (!asignRoleResult.Succeeded)
                throw new OperationFailedException("Asign Role Operation Falied !!");

            var createTokenDTO = await _tokenService.CreateTokenAsync(newUser);

            return new LoginResultDTO
            {
                CreateToken = createTokenDTO,
                Email = dto.Email,
                Name = newUser.Name,
                UserName = newUser.UserName!,
                RoleName = RoleEnums.User.ToString()
            };
        }


        public async Task<LoginResultDTO> Login(LoginDTO dto)
        {
            throw new NotImplementedException();
        }


    }
}
