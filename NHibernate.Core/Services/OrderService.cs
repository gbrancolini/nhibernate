using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Core.Services
{
    public class OrderService : Service<Order>, IOrderService
    {
        private readonly INotificationService _notificationService;
        private readonly IValidationService _validationService;
        private readonly IInventoryRepository _inventoryRepository;

        public OrderService(IOrderRepository orderRepository, INotificationService notificationService, 
            IValidationService validationService, IInventoryRepository inventoryRepository): base(orderRepository)
        {
            _notificationService = notificationService;
            _validationService = validationService;
            _inventoryRepository = inventoryRepository;
        }

        public Order CreateOrder(IEnumerable<OrderDetail> orderDetails)
        {
            var order = new Order
            {
                OrderDate = DateTime.Now,
                Status = OrderStatus.Created,
                OrderDetails = orderDetails.ToList()
            };

            foreach (var detail in orderDetails)
            {
                var inventory = _inventoryRepository.GetByPartId(detail.Part.Id);
                if (inventory == null || inventory.Quantity < detail.Quantity)
                {
                    throw new InventoryShortageException(detail.Part.Id, detail.Quantity, inventory?.Quantity ?? 0);
                }
                inventory.Quantity -= detail.Quantity;
                _inventoryRepository.Update(inventory);
            }

            _repository.Add(order);
            _notificationService.NotifyOrderStatusChanged(order.Id, order.Status.ToString());
            return order;
        }

        public void ProcessOrder(int orderId)
        {
            var order = _repository.GetById(orderId);
            if (order == null)
            {
                throw new InvalidOrderOperationException($"Order with ID {orderId} not found.");
            }

            if (order.Status == OrderStatus.Processed || order.Status == OrderStatus.Delivered)
            {
                throw new InvalidOrderOperationException("Cannot process an order that has already been processed or delivered.");
            }

            _validationService.ValidateOrder(order);
            
            foreach (var detail in order.OrderDetails)
            {
                _validationService.ValidatePart(detail.Part);
            }

            order.Status = OrderStatus.Processed;
            _repository.Update(order);

            _notificationService.NotifyOrderStatusChanged(orderId, order.Status.ToString());
        }

        public void CancelOrder(int orderId)
        {
            var order = _repository.GetById(orderId);
            if (order == null)
            {
                throw new InvalidOrderOperationException($"Order with ID {orderId} not found.");
            }

            if (order.Status == OrderStatus.Delivered)
            {
                throw new InvalidOrderOperationException("Cannot cancel a delivered order.");
            }

            order.Status = OrderStatus.Cancelled;
            _repository.Update(order);

            _notificationService.NotifyOrderStatusChanged(orderId, order.Status.ToString());
        }
    }
}
