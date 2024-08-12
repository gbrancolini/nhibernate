using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class ShipmentService : Service<Shipment>, IShipmentService
    {
        private readonly IValidationService _validationService;

        public ShipmentService(IShipmentRepository shipmentRepository, IValidationService validationService): base(shipmentRepository)
        {
            _validationService = validationService;
        }

        public override void Add(Shipment shipment)
        {
            try
            {
                _validationService.ValidateShipment(shipment);

                _repository.Add(shipment);
            }
            catch (Exception ex)
            {
                throw new ShippingException($"Error creating shipment: {ex.Message}", ex);
            }
        }

        public override void Update(Shipment shipment)
        {
            try
            {
                _validationService.ValidateShipment(shipment);

                _repository.Update(shipment);
            }
            catch (Exception ex)
            {
                throw new ShippingException($"Error updating shipment: {ex.Message}", ex);
            }
        }
    }
}
