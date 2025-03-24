using NoahStore.Core.Interfaces;
using System.Linq.Expressions;

namespace NoahStore.Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public Expression<Func<T, object>> OrderBy { get; set; } = null;
        public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> includeExpression)
        {
            Criteria = includeExpression;
        }

        public void AddOrderBy(Expression<Func<T, object>> orderBy)
        {
            OrderBy = orderBy;
        }
        public void AddOrderByDesc(Expression<Func<T, object>> orderByDesc)
        {
            OrderByDesc = orderByDesc;
        }
    }
}
