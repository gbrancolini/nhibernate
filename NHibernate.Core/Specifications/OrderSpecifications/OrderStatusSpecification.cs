using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace NHibernatePoc.Core.Specifications.OrderSpecifications
{
    public class OrderStatusSpecification : ISpecification<Order>
    {
        private readonly OrderStatus _requiredStatus;

        public OrderStatusSpecification(OrderStatus requiredStatus)
        {
            _requiredStatus = requiredStatus;
        }
        // used for queries in memory
        public bool IsSatisfiedBy(Order order)
        {
            return order.Status == _requiredStatus;
        }

        // and in db.... great, uh!?
        public Expression<Func<Order, bool>> ToExpression()
        {
            return order => order.Status == _requiredStatus;
        }
    }
}
