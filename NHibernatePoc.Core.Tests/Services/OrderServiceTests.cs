using Moq;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private Mock<IOrderRepository> _orderRepositoryMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<IValidationService> _validationServiceMock;
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private OrderService _orderService;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _notificationServiceMock = new Mock<INotificationService>();
            _validationServiceMock = new Mock<IValidationService>();
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();

            _orderService = new OrderService(
                _orderRepositoryMock.Object,
                _notificationServiceMock.Object,
                _validationServiceMock.Object,
                _inventoryRepositoryMock.Object);
        }

        [Test]
        public void CreateOrder_WithSufficientInventory_CreatesOrderAndUpdatesInventory()
        {
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { Part = new Part { Id = 1 }, Quantity = 2 }
            };

            var inventories = new List<Inventory>
            {
                new Inventory { Part = new Part { Id = 1 }, Quantity = 5 }
            };

            _inventoryRepositoryMock.Setup(x => x.GetByPartId(1)).Returns(inventories.First());
            _orderRepositoryMock.Setup(x => x.Add(It.IsAny<Order>()));

            var order = _orderService.CreateOrder(orderDetails);

            _inventoryRepositoryMock.Verify(x => x.Update(It.Is<Inventory>(i => i.Quantity == 3)), Times.Once);
            _notificationServiceMock.Verify(n => n.NotifyOrderStatusChanged(order.Id, OrderStatus.Created.ToString()), Times.Once);
            Assert.AreEqual(OrderStatus.Created, order.Status);
        }

        [Test]
        public void ProcessOrder_ValidOrder_MarksAsProcessed()
        {
            var order = new Order { Id = 1, Status = OrderStatus.Created, OrderDetails = new List<OrderDetail>() };
            _orderRepositoryMock.Setup(x => x.GetById(1)).Returns(order);

            _orderService.ProcessOrder(1);

            Assert.AreEqual(OrderStatus.Processed, order.Status);
            _notificationServiceMock.Verify(n => n.NotifyOrderStatusChanged(1, OrderStatus.Processed.ToString()), Times.Once);
        }

        [Test]
        public void CancelOrder_OrderNotDelivered_MarksAsCancelled()
        {
            var order = new Order { Id = 1, Status = OrderStatus.Created };
            _orderRepositoryMock.Setup(x => x.GetById(1)).Returns(order);

            _orderService.CancelOrder(1);

            Assert.AreEqual(OrderStatus.Cancelled, order.Status);
            _notificationServiceMock.Verify(n => n.NotifyOrderStatusChanged(1, OrderStatus.Cancelled.ToString()), Times.Once);
        }

        [Test]
        public void CreateOrder_InsufficientInventory_ThrowsInventoryShortageException()
        {
            var orderDetails = new List<OrderDetail>
            {
                new OrderDetail { Part = new Part { Id = 1 }, Quantity = 10 }
            };

            var inventories = new List<Inventory>
            {
                new Inventory { Part = new Part { Id = 1 }, Quantity = 5 }
            };

            _inventoryRepositoryMock.Setup(x => x.GetByPartId(1)).Returns(inventories.First());

            Assert.Throws<InventoryShortageException>(() => _orderService.CreateOrder(orderDetails));
        }
    }
}
