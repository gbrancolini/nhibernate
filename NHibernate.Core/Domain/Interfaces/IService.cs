namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IService<TEntity> where TEntity : IEntity
    {
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(int id);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}
