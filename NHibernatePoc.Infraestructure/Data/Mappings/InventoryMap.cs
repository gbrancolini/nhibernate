using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class InventoryMap : ClassMap<Inventory>
    {
        public InventoryMap()
        {
            Table("Inventories");

            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.Quantity);
            Map(x => x.Status).CustomType<InventoryStatus>();

            References(x => x.Part)
                .Column("PartId")
                .Not.Nullable();

            References(x => x.Warehouse)
                .Column("WarehouseId")
                .Not.Nullable();
        }
    }
}
