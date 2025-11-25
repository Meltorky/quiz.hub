using Microsoft.AspNetCore.Identity;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Application.Interfaces.IServices.Authentication;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Services.Authentication
{
    public class IsAuthorized : IIsAuthorized
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public IsAuthorized(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> IsAdmin(string userId) 
        {
            var user = await _userManager.FindByNameAsync(userId);

            if (user == null)
                return false;

            return await _userManager.IsInRoleAsync(user,RoleEnums.Admin.ToString());
        }

        //public async Task<bool> IsHost(Guid Id,CancellationToken token)
        //{
        //    return (await _unitOfWork.Hosts.FindById(Id,token)) is not null;
        //}

        //public async Task<bool> IsCandidate(Guid Id,CancellationToken token)
        //{
        //    return (await _unitOfWork.Candidates.FindById(Id, token)) is not null;
        //}
    }
}
