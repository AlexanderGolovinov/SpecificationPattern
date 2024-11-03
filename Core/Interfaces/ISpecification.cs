using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
    /**
     * Effectively 'Where' clause in Linq expression
     */
    Expression<Func<T, bool>>? Criteria { get; }

    /**
     * Effectively 'OrderBy' clause in Linq expression
     */
    Expression<Func<T, object>> OrderBy { get; }

    /**
     * Effectively 'OrderByDesc' clause in Linq expression
     */
    Expression<Func<T, object>> OrderByDesc { get; }

    bool IsDistinct { get; }

    int Take { get; }

    int Skip { get; }
    bool IsPaginationEnabled { get; }

    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}

public interface ISpecification<T, TResult> : ISpecification<T>
{
    Expression<Func<T, TResult>>? Select { get; }
}