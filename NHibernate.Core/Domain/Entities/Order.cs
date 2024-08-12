using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Domain.Entities
{
    public class Order : IEntity
    {
        public virtual int Id { get; set; }
        public virtual DateTime OrderDate { get; set; }

        public virtual OrderStatus Status { get; set; }
        public virtual ShippingPriority Priority { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
