using Microsoft.EntityFrameworkCore;
using quiz.hub.Application.Interfaces.IRepositories.Comman;
using quiz.hub.Domain.Comman;
using quiz.hub.Infrastructure.Data;
using System.Linq.Expressions;

namespace quiz.hub.Infrastructure.Repositories.Comman
{
    public class BaseRepository<T>(AppDbContext _context) : IBaseRepository<T> where T : class
    {

        public async Task<T?> FindById(Guid Id, CancellationToken token, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes is not null && includes.Any())
                foreach (var include in includes)
                    query = include(query);

            return await query
                .SingleOrDefaultAsync(e => EF.Property<Guid>(e, "Id") == Id, token);
        }


        public async Task<T?> FindUnique(Expression<Func<T, bool>> criteria, CancellationToken token, params Func<IQueryable<T>, IQueryable<T>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes is not null && includes.Any())
                foreach (var include in includes)
                    query = include(query);

            return await query
                .SingleOrDefaultAsync(criteria, token);
        }


        public async Task<List<T>> GetAll(QueryFilters<T> filters, CancellationToken token)
        {
            IQueryable<T> query = _context.Set<T>();

            // Apply where clauses
            if (filters.Criteria.Any())
                foreach (var criteria in filters.Criteria)
                    query = query.Where(criteria);

            // Apply includes
            if (filters.Includes.Any())
                foreach (var include in filters.Includes)
                    query = include(query);

            // Apply ordering
            if (filters.OrderBy is not null)
                query = filters.OrderByDescending
                    ? query.OrderByDescending(filters.OrderBy)
                    : query.OrderBy(filters.OrderBy);

            // Apply pagination
            query = query.Skip(filters.Skip).Take(filters.Take);

            return await query.AsNoTracking().ToListAsync(token);
        }


        public async Task<T> AddAsync(T entity, CancellationToken token)
        {
            await _context.Set<T>().AddAsync(entity, token);
            return entity;
        }


        public async Task AddRangeAsync(ICollection<T> entities, CancellationToken token)
        {
            await _context.Set<T>().AddRangeAsync(entities, token);
        }


        public T Edit(T entity)
        {
            _context.Set<T>().Update(entity);
            return entity;
        }

        public void  EditRange(ICollection<T> entities)
        {
             _context.Set<T>().UpdateRange(entities);
        }


        public async Task<bool> DeleteAsync(T entity, CancellationToken token)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync(token) > 0;
        }


        public async Task DeleteRangeAsync(ICollection<T> entities, CancellationToken token)
        {
            _context.Set<T>().RemoveRange(entities);
            await _context.SaveChangesAsync(token);
        }


        public async Task<bool> IsExistAsync(Expression<Func<T, bool>> criteria, CancellationToken token)
        {
            return await _context.Set<T>().AnyAsync(criteria, token);
        }
    }
}
