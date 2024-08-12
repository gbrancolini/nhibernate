using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class WarehouseMap : ClassMap<Warehouse>
    {
        public WarehouseMap()
        {
            Table("Warehouses");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Location).Length(500);

            HasMany(x => x.Inventories)
                .Cascade.AllDeleteOrphan()
                .Inverse()
                .KeyColumn("WarehouseId");
        }
    }
}
