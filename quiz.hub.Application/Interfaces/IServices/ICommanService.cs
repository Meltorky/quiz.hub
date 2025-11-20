using quiz.hub.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Application.Interfaces.IServices
{
    public interface ICommanService
    {
        Task<bool> UserExist(Guid Id, PositionEnums position, CancellationToken token);
    }
}
