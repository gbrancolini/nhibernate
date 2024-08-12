using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IOrderService: IService<Order>
    {
        Order CreateOrder(IEnumerable<OrderDetail> orderDetails);
        void ProcessOrder(int orderId);
        void CancelOrder(int orderId);
    }
}
