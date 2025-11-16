using quiz.hub.Application.Interfaces.IRepositories.Comman;

namespace quiz.hub.Application.Services
{
    public class BaseService(IUnitOfWork _unitOfWork)
    {
        // save changes
        public async Task SaveAsync(CancellationToken token)
        {
            await _unitOfWork.SaveChangesAsync(token);
        }
    }
}
