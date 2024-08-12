using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IInventoryRepository : IRepository<Inventory>
    {
        Inventory GetByPartId(int partId);
        IEnumerable<Inventory> GetByWarehouseId(int warehouseId);
    }
}
