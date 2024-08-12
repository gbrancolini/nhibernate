using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.API.Controllers
{
    public class ShipmentController : BaseController<Shipment, IShipmentService>
    {
        public ShipmentController(IShipmentService service) : base(service)
        {
        }
    }
}
