using NHibernate;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Infraestructure.Data.Repositories
{
    public class InventoryRepository : Repository<Inventory>, IInventoryRepository
    {
        public InventoryRepository(ISession session): base(session)
        {
        }


        public Inventory GetByPartId(int partId)
        {
            return _session.Query<Inventory>().FirstOrDefault(i => i.Part.Id == partId);
        }

        public IEnumerable<Inventory> GetByWarehouseId(int warehouseId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Inventory> FindBySpecification(ISpecification<Inventory> specification)
        {
            var query = _session.Query<Inventory>().Where(specification.ToExpression());
            return query.ToList();
        }
    }
}
