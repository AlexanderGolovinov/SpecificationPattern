using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    /**
     * Effectively 'Where' clause in Linq expression
     */
    Expression<Func<T, bool>>? Criteria { get; }
    
    Expression<Func<T, object>> OrderBy { get; }
    
    Expression<Func<T, object>> OrderByDesc { get; }
}