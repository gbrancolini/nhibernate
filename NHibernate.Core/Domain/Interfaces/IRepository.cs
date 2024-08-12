using System.Linq.Expressions;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        IEnumerable<T> FindBySpecification(Expression<Func<T, bool>> specification);
    }
}
