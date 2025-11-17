using quiz.hub.Domain.Comman;
using System.Linq.Expressions;

namespace quiz.hub.Application.Interfaces.IRepositories.Comman
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> FindById(Guid Id, CancellationToken token, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task<T?> FindUnique(Expression<Func<T, bool>> criteria, CancellationToken token, params Func<IQueryable<T>, IQueryable<T>>[] includes);
        Task<List<T>> GetAll(QueryFilters<T> filters, CancellationToken token);
        Task<T> AddAsync(T entity, CancellationToken token);
        Task AddRangeAsync(ICollection<T> entities, CancellationToken token);
        Task<T> Edit(T entity, CancellationToken token);
        Task EditRange(ICollection<T> entities, CancellationToken token);
        Task<bool> DeleteAsync(T entity, CancellationToken token);
        Task DeleteRangeAsync(ICollection<T> entities, CancellationToken token);
        Task<bool> IsExistAsync(Expression<Func<T, bool>> criteria, CancellationToken token);
    }
}
