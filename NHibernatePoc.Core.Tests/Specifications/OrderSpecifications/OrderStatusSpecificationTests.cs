using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Specifications.OrderSpecifications;

namespace NHibernatePoc.Core.Tests.Specifications.OrderSpecifications
{
    [TestFixture]
    public class OrderStatusSpecificationTests
    {
        [Test]
        public void IsSatisfiedBy_OrderWithMatchingStatus_ReturnsTrue()
        {
            // Arrange
            var specification = new OrderStatusSpecification(OrderStatus.Processed);
            var order = new Order { Status = OrderStatus.Processed };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(order);

            // Assert
            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void IsSatisfiedBy_OrderWithDifferentStatus_ReturnsFalse()
        {
            // Arrange
            var specification = new OrderStatusSpecification(OrderStatus.Processed);
            var order = new Order { Status = OrderStatus.Created };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(order);

            // Assert
            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void ToExpression_UsedInLinqQuery_CorrectlyFiltersCollection()
        {
            // Arrange
            var specification = new OrderStatusSpecification(OrderStatus.Processed);
            var orders = new List<Order>
            {
                new Order { Status = OrderStatus.Processed },
                new Order { Status = OrderStatus.Created },
                new Order { Status = OrderStatus.Processed }
            };

            // Act
            var expression = specification.ToExpression();
            var result = orders.AsQueryable().Where(expression).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.True(result.All(o => o.Status == OrderStatus.Processed));
        }
    }
}
