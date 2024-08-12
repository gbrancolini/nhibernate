using NHibernate;
using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;
using NHibernatePoc.Core.Domain.Interfaces;

namespace NHibernatePoc.Infraestructure.Data.Repositories
{
    public class PartRepository : Repository<Part>, IPartRepository
    {
        public PartRepository(ISession session) : base(session)
        {
        }
    }
}
