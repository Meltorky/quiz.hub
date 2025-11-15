using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace quiz.hub.Domain.Comman
{
    public class QueryFilters<T>
    {
        public List<Expression<Func<T, bool>>> Criteria { get; } = new();
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int Skip => PageSize * (PageNumber - 1);
        public int Take => PageSize;
        public Expression<Func<T, object>>? OrderBy { get; set; }
        public bool OrderByDescending { get; set; }
        public List<Func<IQueryable<T>, IQueryable<T>>> Includes { get; } = new();

        // helper methods
        public void AddCriteria(Expression<Func<T, bool>> criteria) => Criteria.Add(criteria);
        public void AddInclude(Func<IQueryable<T>, IQueryable<T>> include) => Includes.Add(include);
    }
}
