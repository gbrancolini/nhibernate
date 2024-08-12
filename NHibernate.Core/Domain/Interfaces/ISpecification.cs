using System.Linq.Expressions;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface ISpecification<T> where T : IEntity
    {
        bool IsSatisfiedBy(T entity);
        Expression<Func<T, bool>> ToExpression();
    }
}
