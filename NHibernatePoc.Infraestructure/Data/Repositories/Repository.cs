using NHibernate;
using NHibernatePoc.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace NHibernatePoc.Infraestructure.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        internal readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public TEntity GetById(int id)
        {
            return _session.Get<TEntity>(id);
        }

        public void Add(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Save(entity);
                transaction.Commit();
            }
        }

        public void Update(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Update(entity);
                transaction.Commit();
            }
        }

        public void Remove(TEntity entity)
        {
            using (var transaction = _session.BeginTransaction())
            {
                _session.Delete(entity);
                transaction.Commit();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _session.Query<TEntity>().ToList();
        }

        public IEnumerable<TEntity> FindBySpecification(Expression<Func<TEntity, bool>> specification)
        {
            return _session.Query<TEntity>().Where(specification).ToList();
        }
    }
}
