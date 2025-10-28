﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using quiz.hub.Application.DTOs.Auth;
using quiz.hub.Application.Interfaces.IServices;
using quiz.hub.Application.Options;
using quiz.hub.Domain.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace quiz.hub.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions _jwtOptions;

        public TokenService(UserManager<ApplicationUser> userManager, IOptions<JwtOptions> jwt)
        {
            _userManager = userManager;
            _jwtOptions = jwt.Value;
        }

        public async Task<CreateTokenDTO> CreateTokenAsync(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user); 
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles) 
                roleClaims.Add(new Claim(ClaimTypes.Role,role));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.UserName!)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var tokenHandler = new JwtSecurityTokenHandler();
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtOptions.Issuer,
                Audience = _jwtOptions.Audience,
                Expires = DateTime.UtcNow.AddHours(_jwtOptions.LifeTimeInHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key)),SecurityAlgorithms.HmacSha256)
            };

            var createdToken = tokenHandler.CreateToken(descriptor);
            var token = tokenHandler.WriteToken(createdToken);

            return new CreateTokenDTO 
            { 
                Token = token,
                ExpiresOn = createdToken.ValidTo
            };
        }
    }
}
