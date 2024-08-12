using Moq;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Exceptions;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Services;

namespace NHibernatePoc.Core.Tests.Services
{
    [TestFixture]
    public class PartServiceTests
    {
        private Mock<IPartRepository> _partRepositoryMock;
        private Mock<IValidationService> _validationServiceMock;
        private PartService _partService;

        [SetUp]
        public void Setup()
        {
            _partRepositoryMock = new Mock<IPartRepository>();
            _validationServiceMock = new Mock<IValidationService>();
            _partService = new PartService(_partRepositoryMock.Object, _validationServiceMock.Object);
        }

        [Test]
        public void Add_ValidPart_CallsAddOnRepository()
        {
            var part = new Part { Id = 1, Name = "Engine" };
            _partService.Add(part);
            _validationServiceMock.Verify(v => v.ValidatePart(part), Times.Once);
            _partRepositoryMock.Verify(p => p.Add(part), Times.Once);
        }

        [Test]
        public void Update_ValidPart_CallsUpdateOnRepository()
        {
            var part = new Part { Id = 1, Name = "Engine" };
            _partService.Update(part);
            _validationServiceMock.Verify(v => v.ValidatePart(part), Times.Once);
            _partRepositoryMock.Verify(p => p.Update(part), Times.Once);
        }

        [Test]
        public void Delete_ExistingPart_CallsRemoveOnRepository()
        {
            var part = new Part { Id = 1, Name = "Engine" };
            _partRepositoryMock.Setup(p => p.GetById(1)).Returns(part);

            _partService.Delete(1);
            _partRepositoryMock.Verify(p => p.Remove(part), Times.Once);
        }

        [Test]
        public void Delete_NonExistingPart_ThrowsPartNotFoundException()
        {
            _partRepositoryMock.Setup(p => p.GetById(1)).Returns((Part)null);

            Assert.Throws<PartNotFoundException>(() => _partService.Delete(1));
        }

        [Test]
        public void GetPartsByCondition_ReturnsFilteredParts()
        {
            var parts = new List<Part>
            {
                new Part { Id = 1, Name = "Engine", Condition = PartCondition.New },
                new Part { Id = 2, Name = "Tire", Condition = PartCondition.Used }
            };
            _partRepositoryMock.Setup(p => p.FindBySpecification(It.IsAny<System.Linq.Expressions.Expression<System.Func<Part, bool>>>()))
                .Returns((System.Linq.Expressions.Expression<System.Func<Part, bool>> expr) => parts.AsQueryable().Where(expr.Compile()).ToList());

            var result = _partService.GetPartsByCondition(PartCondition.New).ToList();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Engine", result.First().Name);
        }
    }
}