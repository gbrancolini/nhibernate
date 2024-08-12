using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class ShipmentMap : ClassMap<Shipment>
    {
        public ShipmentMap()
        {
            Table("Shipments");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.ShippingAddress).Length(255);
            Map(x => x.ShipDate);
            Map(x => x.ShippedDate);
            Map(x => x.ExpectedDeliveryDate);
            Map(x => x.Priority).CustomType<string>();
        }
    }
}
