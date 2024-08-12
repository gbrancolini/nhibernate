using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : IEntity
    {
        protected readonly IRepository<TEntity> _repository;

        public Service(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual void Add(TEntity entity)
        {
            _repository.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            _repository.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entityToRemove =  _repository.GetById(id);
            if (entityToRemove != null)
            {
                _repository.Remove(entityToRemove);
            }
        }

        public virtual TEntity GetById(int id)
        {
            return _repository.GetById(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
