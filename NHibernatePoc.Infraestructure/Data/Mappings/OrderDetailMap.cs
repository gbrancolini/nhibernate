using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class OrderDetailMap : ClassMap<OrderDetail>
    {
        public OrderDetailMap()
        {
            Table("OrderDetails");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Quantity);
            Map(x => x.Priority).CustomType<string>();

            References(x => x.Part)
                .Column("PartId")
                .Not.Nullable();

            References(x => x.Order)
                .Column("OrderId")
                .Not.Nullable();
        }
    }
}
