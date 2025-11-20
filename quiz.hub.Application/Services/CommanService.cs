using Microsoft.AspNetCore.Identity;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;


namespace quiz.hub.Application.Services
{
    public class CommanService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public CommanService(UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UserExist(Guid Id, PositionEnums position, CancellationToken token)
        {
            switch (position)
            {
                case PositionEnums.Admin:
                     return _userManager.Users.Any(x => x.Id == Id.ToString());

                case PositionEnums.Host:
                     return await _unitOfWork.Hosts.IsExistAsync(x => x.Id == Id, token);

                case PositionEnums.Candidate:
                    return await _unitOfWork.Candidates.IsExistAsync(x => x.Id == Id, token);

                default:
                    throw new NotFoundException("Invalid ID !!");
            }
        }

    }
}
