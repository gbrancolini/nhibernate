using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class Shipment : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string ShippingAddress { get; set; }
        public virtual DateTime ShipDate { get; set; }
        public virtual DateTime ShippedDate { get; set; }
        public virtual DateTime ExpectedDeliveryDate { get; set; }
        public virtual ShippingPriority Priority { get; set; }
    }
}
