using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class OrderDetail : IEntity
    {
        public virtual int Id { get; set; }
        public virtual Part Part { get; set; }
        public virtual Order Order { get; set; }
        public virtual int Quantity { get; set; }
        public virtual ShippingPriority Priority { get; set; }
    }
}
