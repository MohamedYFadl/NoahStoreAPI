using Microsoft.EntityFrameworkCore;
using NoahStore.Core.Interfaces;

namespace NoahStore.Infrastructure.Repositories
{
    public static class SpecificationEvaluator<TEntity> where TEntity : class
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specs)
        {
            var query = inputQuery;
            if(specs.Criteria != null)
            {
                query = query.Where(specs.Criteria);
            }

            query = specs.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
            return query;
        }
    }
}
