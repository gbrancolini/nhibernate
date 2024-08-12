using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IInventoryService: IService<Inventory>
    {
        void UpdateInventoryForPart(int partId, int deltaQuantity);
        IEnumerable<Inventory> GetLowInventories(int threshold);
    }
}
