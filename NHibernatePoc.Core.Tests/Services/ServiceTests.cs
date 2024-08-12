using Moq;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class ServiceTests
    {
        private Mock<IRepository<DummyEntity>> _repositoryMock;
        private Service<DummyEntity> _service;

        [SetUp]
        public void Setup()
        {
            _repositoryMock = new Mock<IRepository<DummyEntity>>();
            _service = new Service<DummyEntity>(_repositoryMock.Object);
        }

        [Test]
        public void Add_CallsAddOnRepository()
        {
            var entity = new DummyEntity { Id = 1 };
            _service.Add(entity);
            _repositoryMock.Verify(r => r.Add(entity), Times.Once);
        }

        [Test]
        public void Update_CallsUpdateOnRepository()
        {
            var entity = new DummyEntity { Id = 1 };
            _service.Update(entity);
            _repositoryMock.Verify(r => r.Update(entity), Times.Once);
        }

        [Test]
        public void Delete_ExistingEntity_CallsRemoveOnRepository()
        {
            var entity = new DummyEntity { Id = 1 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(entity);

            _service.Delete(1);
            _repositoryMock.Verify(r => r.Remove(entity), Times.Once);
        }

        [Test]
        public void Delete_NonExistingEntity_DoesNotCallRemove()
        {
            _repositoryMock.Setup(r => r.GetById(1)).Returns((DummyEntity)null);

            _service.Delete(1);
            _repositoryMock.Verify(r => r.Remove(It.IsAny<DummyEntity>()), Times.Never);
        }

        [Test]
        public void GetById_CallsGetByIdOnRepository()
        {
            var entity = new DummyEntity { Id = 1 };
            _repositoryMock.Setup(r => r.GetById(1)).Returns(entity);

            var result = _service.GetById(1);
            _repositoryMock.Verify(r => r.GetById(1), Times.Once);
            Assert.AreEqual(entity, result);
        }

        [Test]
        public void GetAll_CallsGetAllOnRepository()
        {
            var entities = new List<DummyEntity>
            {
                new DummyEntity { Id = 1 },
                new DummyEntity { Id = 2 }
            };
            _repositoryMock.Setup(r => r.GetAll()).Returns(entities.AsQueryable());

            var result = _service.GetAll().ToList();
            _repositoryMock.Verify(r => r.GetAll(), Times.Once);
            Assert.AreEqual(2, result.Count);
        }
    }

    public class DummyEntity : IEntity
    {
        public int Id { get; set; }
    }
}
