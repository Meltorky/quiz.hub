using Microsoft.AspNetCore.Identity;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Application.Interfaces.IServices.Authentication;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;
using System.Net.Mail;
using System.Text;

namespace quiz.hub.Application.Services.Authentication
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


        public async Task<AuthenticatedUserDTO> Register(RegisterDTO dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                throw new DuplicateEmailException($"Email '{dto.Email}' is already registered.");


            // handle duplicate usernames ex: username@gmail.com & username@yahoo.com
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

            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded) 
            {
                var errors = string.Join(" & ",result.Errors.Select(e => e.Description));
                throw new OperationFailedException(errors);
            }

            var asignRoleResult = await _userManager.AddToRoleAsync(newUser, RoleEnums.User.ToString());
            if (!asignRoleResult.Succeeded)
                throw new OperationFailedException("Asign to Role Operation Falied !!");

            var createdTokenDTO = await _tokenService.CreateTokenAsync(newUser);

            return new AuthenticatedUserDTO
            {
                CreatedToken = createdTokenDTO,
                Email = dto.Email,
                Name = newUser.Name,
                UserName = newUser.UserName!,
                RoleNames = new() { RoleEnums.User.ToString() }
            };
        }


        public async Task<AuthenticatedUserDTO> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                throw new NotFoundException("Invaled email or password !!");

            var createdTokenDTO = await _tokenService.CreateTokenAsync(user);

            var roles = await _userManager.GetRolesAsync(user);

            return new AuthenticatedUserDTO
            {
                CreatedToken = createdTokenDTO,
                Email = user.Email!,
                Name = user.Name,
                UserName = user.UserName!,
                RoleNames = roles.ToList()
            };
        }


    }
}
