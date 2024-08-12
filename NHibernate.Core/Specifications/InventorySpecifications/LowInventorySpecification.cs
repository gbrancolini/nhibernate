using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;
using System.Linq.Expressions;

namespace NHibernatePoc.Core.Specifications.InventorySpecifications
{
    public class LowInventorySpecification : ISpecification<Inventory>
    {
        private readonly int _threshold;

        public LowInventorySpecification(int threshold)
        {
            _threshold = threshold;
        }

        // this one is an specification for DB
        public Expression<Func<Inventory, bool>> ToExpression()
        {
            return inventory => inventory.Quantity <= _threshold;
        }

        public bool IsSatisfiedBy(Inventory inventory)
        {
            return inventory.Quantity <= _threshold;
        }
    }
}
