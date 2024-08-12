using NHibernate;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Infraestructure.Data.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(ISession session): base(session)
        {
        }
    }
}
