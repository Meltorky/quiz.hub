using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Common.Exceptions;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Enums;
using quiz.hub.Domain.Identity;
using quiz.hub.Infrastructure.Data;

namespace quiz.hub.Infrastructure.Repositories.Comman
{
    public class CommanRepo : ICommanRepo
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AppDbContext _context;
        public CommanRepo(UserManager<ApplicationUser> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> UserExist(Guid Id, PositionEnums position, CancellationToken token)
        {
            switch (position)
            {
                case PositionEnums.Admin:
                    return await _userManager.Users.AnyAsync(x => x.Id == Id.ToString());

                case PositionEnums.Host:
                    return await _context.Hosts.AnyAsync(x => x.Id == Id, token);

                case PositionEnums.Candidate:
                    return await _context.Candidates.AnyAsync(x => x.Id == Id, token);

                default:
                    throw new NotFoundException("Invalid ID !!");
            }
        }
    }
}
