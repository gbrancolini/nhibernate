using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Specifications.InventorySpecifications;

namespace NHibernatePoc.Core.Tests.Specifications.InventorySpecifications
{
    [TestFixture]
    public class LowInventorySpecificationTests
    {
        [Test]
        public void IsSatisfiedBy_InventoryBelowThreshold_ReturnsTrue()
        {
            // Arrange
            var specification = new LowInventorySpecification(10);
            var inventory = new Inventory { Quantity = 9 };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(inventory);

            // Assert
            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void IsSatisfiedBy_InventoryAboveThreshold_ReturnsFalse()
        {
            // Arrange
            var specification = new LowInventorySpecification(10);
            var inventory = new Inventory { Quantity = 11 };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(inventory);

            // Assert
            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void ToExpression_UsedInLinqQuery_CorrectlyFiltersCollection()
        {
            // Arrange
            var specification = new LowInventorySpecification(10);
            var inventories = new List<Inventory>
            {
                new Inventory { Quantity = 5 },
                new Inventory { Quantity = 15 },
                new Inventory { Quantity = 10 }
            };

            // Act
            var expression = specification.ToExpression();
            var result = inventories.AsQueryable().Where(expression).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.True(result.All(i => i.Quantity <= 10));
        }
    }
}