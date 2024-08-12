using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Table("Orders");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.OrderDate);
            Map(x => x.Status).CustomType<string>();
            Map(x => x.Priority).CustomType<string>();

            HasMany(x => x.OrderDetails)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("OrderId");
        }
    }
}
