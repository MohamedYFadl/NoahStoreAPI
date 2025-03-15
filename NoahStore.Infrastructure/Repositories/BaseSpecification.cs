using NoahStore.Core.Interfaces;
using System.Linq.Expressions;

namespace NoahStore.Infrastructure.Repositories
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; set ; }
        public List<Expression<Func<T, object>>> Includes { get ; set ; } = new List<Expression<Func<T, object>>> ();
        public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T,bool>> includeExpression)
        {
            Criteria = includeExpression;
        }
    }
}
