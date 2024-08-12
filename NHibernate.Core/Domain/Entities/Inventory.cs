using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class Inventory: IEntity
    {
        public virtual int Id { get; set; }
        public virtual InventoryStatus Status { get; set; }
        public virtual Part Part { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
}
