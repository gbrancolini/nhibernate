using Moq;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class WarehouseServiceTests
    {
        private Mock<IWarehouseRepository> _warehouseRepositoryMock;
        private WarehouseService _warehouseService;

        [SetUp]
        public void Setup()
        {
            _warehouseRepositoryMock = new Mock<IWarehouseRepository>();
            _warehouseService = new WarehouseService(_warehouseRepositoryMock.Object);
        }

        [Test]
        public void Add_CallsAddOnRepository()
        {
            var warehouse = new Warehouse { Id = 1, Location = "Main Street" };
            _warehouseService.Add(warehouse);
            _warehouseRepositoryMock.Verify(r => r.Add(warehouse), Times.Once);
        }

        [Test]
        public void Update_CallsUpdateOnRepository()
        {
            var warehouse = new Warehouse { Id = 1, Location = "Main Street" };
            _warehouseService.Update(warehouse);
            _warehouseRepositoryMock.Verify(r => r.Update(warehouse), Times.Once);
        }

        [Test]
        public void Delete_ExistingWarehouse_CallsRemoveOnRepository()
        {
            var warehouse = new Warehouse { Id = 1, Location = "Main Street" };
            _warehouseRepositoryMock.Setup(r => r.GetById(1)).Returns(warehouse);

            _warehouseService.Delete(1);
            _warehouseRepositoryMock.Verify(r => r.Remove(warehouse), Times.Once);
        }

        [Test]
        public void GetById_ReturnsWarehouse()
        {
            var warehouse = new Warehouse { Id = 1, Location = "Main Street" };
            _warehouseRepositoryMock.Setup(r => r.GetById(1)).Returns(warehouse);

            var result = _warehouseService.GetById(1);
            _warehouseRepositoryMock.Verify(r => r.GetById(1), Times.Once);
            Assert.AreEqual(warehouse, result);
        }

        [Test]
        public void GetAll_ReturnsAllWarehouses()
        {
            var warehouses = new List<Warehouse>
            {
                new Warehouse { Id = 1, Location = "Main Street" },
                new Warehouse { Id = 2, Location = "Second Street" }
            };
            _warehouseRepositoryMock.Setup(r => r.GetAll()).Returns(warehouses.AsQueryable());

            var result = _warehouseService.GetAll().ToList();
            _warehouseRepositoryMock.Verify(r => r.GetAll(), Times.Once);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Main Street", result[0].Location);
        }
    }
}
