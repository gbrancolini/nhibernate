using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IValidationService
    {
        void ValidatePart(Part part);
        void ValidateInventory(Inventory inventory);
        void ValidateOrder(Order order);
        void ValidateShipment(Shipment shipment);
    }
}
