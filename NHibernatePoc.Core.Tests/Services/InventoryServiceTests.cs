using Moq;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class InventoryServiceTests
    {
        private Mock<IInventoryRepository> _inventoryRepositoryMock;
        private Mock<INotificationService> _notificationServiceMock;
        private Mock<IValidationService> _validationServiceMock;
        private Mock<IPartRepository> _partRepositoryMock;
        private InventoryService _inventoryService;

        [SetUp]
        public void Setup()
        {
            _inventoryRepositoryMock = new Mock<IInventoryRepository>();
            _notificationServiceMock = new Mock<INotificationService>();
            _validationServiceMock = new Mock<IValidationService>();
            _partRepositoryMock = new Mock<IPartRepository>();

            _inventoryService = new InventoryService(
                _inventoryRepositoryMock.Object,
                _notificationServiceMock.Object,
                _validationServiceMock.Object,
                _partRepositoryMock.Object);
        }

        [Test]
        public void UpdateInventoryForPart_WhenPartDoesNotExist_ThrowsInventoryShortageException()
        {
            var partId = 1;
            var deltaQuantity = 5;

            _inventoryRepositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Inventory>().AsQueryable());

            Assert.Throws<InventoryShortageException>(() =>
                _inventoryService.UpdateInventoryForPart(partId, deltaQuantity));
        }

        [Test]
        public void UpdateInventoryForPart_WhenNewQuantityIsNegative_ThrowsInventoryShortageException()
        {
            var partId = 1;
            var deltaQuantity = -5;
            var inventories = new List<Inventory>
            {
                new Inventory { Part = new Part { Id = 1 }, Quantity = 3 }
            };

            _inventoryRepositoryMock.Setup(repo => repo.GetAll()).Returns(inventories.AsQueryable());

            Assert.Throws<InventoryShortageException>(() =>
                _inventoryService.UpdateInventoryForPart(partId, deltaQuantity));
        }

        [Test]
        public void UpdateInventoryForPart_WhenQuantityDropsBelowThreshold_NotifiesLowInventory()
        {
            var partId = 1;
            var deltaQuantity = -1;
            var inventories = new List<Inventory>
            {
                new Inventory { Part = new Part { Id = 1 }, Quantity = 10 }
            };

            _inventoryRepositoryMock.Setup(repo => repo.GetAll()).Returns(inventories.AsQueryable());

            _inventoryService.UpdateInventoryForPart(partId, deltaQuantity);

            _notificationServiceMock.Verify(n => n.NotifyInventoryLow(partId, 9), Times.Once);
        }

        [Test]
        public void Update_CallsValidateInventory()
        {
            var inventory = new Inventory { Part = new Part { Id = 1 }, Quantity = 50 };

            _inventoryService.Update(inventory);

            _validationServiceMock.Verify(v => v.ValidateInventory(inventory), Times.Once);
            _inventoryRepositoryMock.Verify(r => r.Update(inventory), Times.Once);
        }
    }
}
