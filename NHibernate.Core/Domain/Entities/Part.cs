using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class Part : IEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual PartType Type { get; set; }
        public virtual PartCondition Condition { get; set; }
        public virtual string Description { get; set; }
        public virtual decimal Price { get; set; }
        public virtual List<Inventory> Inventories { get; set; }
    }

}
