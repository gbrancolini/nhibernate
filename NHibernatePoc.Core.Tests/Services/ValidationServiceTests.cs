using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class ValidationServiceTests
    {
        private ValidationService _validationService;

        [SetUp]
        public void Setup()
        {
            _validationService = new ValidationService();
        }

        [Test]
        public void ValidatePart_WithEmptyName_ThrowsArgumentException()
        {
            var part = new Part { Name = "", Price = 100 };
            var ex = Assert.Throws<ArgumentException>(() => _validationService.ValidatePart(part));
            Assert.That(ex.Message, Does.Contain("Part name cannot be empty."));
        }

        [Test]
        public void ValidatePart_WithNegativePrice_ThrowsArgumentException()
        {
            var part = new Part { Name = "Engine", Price = -1 };
            var ex = Assert.Throws<ArgumentException>(() => _validationService.ValidatePart(part));
            Assert.That(ex.Message, Does.Contain("Part price must be greater than zero."));
        }

        [Test]
        public void ValidateInventory_WithNegativeQuantity_ThrowsArgumentException()
        {
            var inventory = new Inventory { Quantity = -1, Part = new Part() };
            var ex = Assert.Throws<ArgumentException>(() => _validationService.ValidateInventory(inventory));
            Assert.That(ex.Message, Does.Contain("Inventory quantity cannot be negative."));
        }

        [Test]
        public void ValidateInventory_WithNullPart_ThrowsArgumentException()
        {
            var inventory = new Inventory { Quantity = 10, Part = null };
            var ex = Assert.Throws<ArgumentException>(() => _validationService.ValidateInventory(inventory));
            Assert.That(ex.Message, Does.Contain("Inventory must be associated with a part."));
        }

        [Test]
        public void ValidateOrder_WithNoOrderDetails_ThrowsArgumentException()
        {
            var order = new Order { OrderDetails = new System.Collections.Generic.List<OrderDetail>() };
            var ex = Assert.Throws<ArgumentException>(() => _validationService.ValidateOrder(order));
            Assert.That(ex.Message, Does.Contain("Order must have at least one order detail."));
        }

        [Test]
        public void ValidateShipment_WithEmptyAddress_ThrowsShippingException()
        {
            var shipment = new Shipment { ShippingAddress = " ", ShipDate = DateTime.Now.AddDays(1), ExpectedDeliveryDate = DateTime.Now.AddDays(2) };
            var ex = Assert.Throws<ShippingException>(() => _validationService.ValidateShipment(shipment));
            Assert.That(ex.Message, Does.Contain("Shipping address cannot be empty."));
        }

        [Test]
        public void ValidateShipment_WithPastShipDate_ThrowsShippingException()
        {
            var shipment = new Shipment { ShippingAddress = "123 Road", ShipDate = DateTime.Now.AddDays(-1), ExpectedDeliveryDate = DateTime.Now.AddDays(1) };
            var ex = Assert.Throws<ShippingException>(() => _validationService.ValidateShipment(shipment));
            Assert.That(ex.Message, Does.Contain("Ship date cannot be in the past."));
        }

        [Test]
        public void ValidateShipment_WithInvalidDeliveryDate_ThrowsShippingException()
        {
            var shipment = new Shipment { ShippingAddress = "123 Road", ShipDate = DateTime.Now.AddDays(1), ExpectedDeliveryDate = DateTime.Now };
            var ex = Assert.Throws<ShippingException>(() => _validationService.ValidateShipment(shipment));
            Assert.That(ex.Message, Does.Contain("Expected delivery date must be after the ship date."));
        }
    }
}
