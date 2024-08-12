using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class Warehouse : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Location { get; set; }
        public virtual List<Inventory> Inventories { get; set; } 
    }
}
