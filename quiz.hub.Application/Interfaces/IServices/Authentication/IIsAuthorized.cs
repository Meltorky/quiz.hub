using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices.Authentication
{
    public interface IIsAuthorized
    {
        Task<bool> IsAdmin(string userId);
        //Task<bool> IsHost(Guid Id, CancellationToken token);
        //Task<bool> IsCandidate(Guid Id, CancellationToken token);
    }
}
