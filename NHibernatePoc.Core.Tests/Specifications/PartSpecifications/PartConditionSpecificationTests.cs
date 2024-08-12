using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Specifications.PartSpecifications;

namespace NHibernatePoc.Core.Tests.Specifications.PartSpecifications
{
    [TestFixture]
    public class PartConditionSpecificationTests
    {
        [Test]
        public void IsSatisfiedBy_PartWithMatchingCondition_ReturnsTrue()
        {
            // Arrange
            var specification = new PartConditionSpecification(PartCondition.New);
            var part = new Part { Condition = PartCondition.New };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(part);

            // Assert
            Assert.IsTrue(isSatisfied);
        }

        [Test]
        public void IsSatisfiedBy_PartWithDifferentCondition_ReturnsFalse()
        {
            // Arrange
            var specification = new PartConditionSpecification(PartCondition.New);
            var part = new Part { Condition = PartCondition.Used };

            // Act
            bool isSatisfied = specification.IsSatisfiedBy(part);

            // Assert
            Assert.IsFalse(isSatisfied);
        }

        [Test]
        public void ToExpression_UsedInLinqQuery_CorrectlyFiltersCollection()
        {
            // Arrange
            var specification = new PartConditionSpecification(PartCondition.New);
            var parts = new List<Part>
            {
                new Part { Condition = PartCondition.New },
                new Part { Condition = PartCondition.Used },
                new Part { Condition = PartCondition.New }
            };

            // Act
            var expression = specification.ToExpression();
            var result = parts.AsQueryable().Where(expression).ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.True(result.All(p => p.Condition == PartCondition.New));
        }
    }
}
