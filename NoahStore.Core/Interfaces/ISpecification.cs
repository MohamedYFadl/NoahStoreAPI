using System.Linq.Expressions;

namespace NoahStore.Core.Interfaces
{
    public interface ISpecification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; set; } 
        public List<Expression<Func<T, object>>> Includes { get; set; } 


        public Expression<Func<T, object>> OrderBy { get; set; }
        public Expression<Func<T, object>> OrderByDesc { get; set; }
    }
}
