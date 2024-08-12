using Moq;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;
using System.ComponentModel.DataAnnotations;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class ShipmentServiceTests
    {
        private Mock<IShipmentRepository> _shipmentRepositoryMock;
        private Mock<IValidationService> _validationServiceMock;
        private ShipmentService _shipmentService;

        [SetUp]
        public void Setup()
        {
            _shipmentRepositoryMock = new Mock<IShipmentRepository>();
            _validationServiceMock = new Mock<IValidationService>();
            _shipmentService = new ShipmentService(_shipmentRepositoryMock.Object, _validationServiceMock.Object);
        }

        [Test]
        public void Add_ValidShipment_CallsAddOnRepository()
        {
            var shipment = new Shipment { Id = 1 };
            _shipmentService.Add(shipment);

            _validationServiceMock.Verify(v => v.ValidateShipment(shipment), Times.Once);
            _shipmentRepositoryMock.Verify(r => r.Add(shipment), Times.Once);
        }

        [Test]
        public void Add_InvalidShipment_ThrowsShippingException()
        {
            var shipment = new Shipment { Id = 1 };
            _validationServiceMock.Setup(v => v.ValidateShipment(It.IsAny<Shipment>())).Throws(new ValidationException("Invalid shipment"));

            var ex = Assert.Throws<ShippingException>(() => _shipmentService.Add(shipment));
            Assert.That(ex.Message, Does.Contain("Error creating shipment: Invalid shipment"));
        }

        [Test]
        public void Update_ValidShipment_CallsUpdateOnRepository()
        {
            var shipment = new Shipment { Id = 1 };
            _shipmentService.Update(shipment);

            _validationServiceMock.Verify(v => v.ValidateShipment(shipment), Times.Once);
            _shipmentRepositoryMock.Verify(r => r.Update(shipment), Times.Once);
        }

        [Test]
        public void Update_InvalidShipment_ThrowsShippingException()
        {
            var shipment = new Shipment { Id = 1 };
            _validationServiceMock.Setup(v => v.ValidateShipment(It.IsAny<Shipment>())).Throws(new ValidationException("Invalid shipment"));

            var ex = Assert.Throws<ShippingException>(() => _shipmentService.Update(shipment));
            Assert.That(ex.Message, Does.Contain("Error updating shipment: Invalid shipment"));
        }
    }
}
