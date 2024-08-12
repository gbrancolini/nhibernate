using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;
using NHibernatePoc.Core.Domain.Entities;
using System.Linq.Expressions;

namespace NHibernatePoc.Core.Specifications.PartSpecifications
{
    public class PartConditionSpecification : ISpecification<Part>
    {
        private readonly PartCondition _condition;

        public PartConditionSpecification(PartCondition condition)
        {
            _condition = condition;
        }

        public bool IsSatisfiedBy(Part part)
        {
            return part.Condition == _condition;
        }

        public Expression<Func<Part, bool>> ToExpression()
        {
            return part => part.Condition == _condition;
        }
    }
}
