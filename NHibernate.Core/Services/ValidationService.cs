using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class ValidationService : IValidationService
    {
        public void ValidatePart(Part part)
        {
            if (string.IsNullOrEmpty(part.Name))
                throw new ArgumentException("Part name cannot be empty.");

            if (part.Price <= 0)
                throw new ArgumentException("Part price must be greater than zero.");
        }

        public void ValidateInventory(Inventory inventory)
        {
            if (inventory.Quantity < 0)
                throw new ArgumentException("Inventory quantity cannot be negative.");

            if (inventory.Part == null)
                throw new ArgumentException("Inventory must be associated with a part.");
        }

        public void ValidateOrder(Order order)
        {
            if (order.OrderDetails == null || order.OrderDetails.Count == 0)
                throw new ArgumentException("Order must have at least one order detail.");

            foreach (var detail in order.OrderDetails)
            {
                ValidatePart(detail.Part);
            }
        }

        public void ValidateShipment(Shipment shipment)
        {
            // Validar que la dirección de envío no sea nula o vacía
            if (string.IsNullOrWhiteSpace(shipment.ShippingAddress))
                throw new ShippingException("Shipping address cannot be empty.");

            // Validar que la fecha de envío sea razonable y no esté en el pasado
            if (shipment.ShipDate < DateTime.Now)
                throw new ShippingException("Ship date cannot be in the past.");

            // Asegurarse de que la fecha de entrega esperada sea posterior a la fecha de envío
            if (shipment.ExpectedDeliveryDate <= shipment.ShipDate)
                throw new ShippingException("Expected delivery date must be after the ship date.");
        }
    }
}
