using NHibernatePoc.Core.Domain.Entities;
using NHibernatePoc.Core.Domain.Enums;

namespace NHibernatePoc.Core.Domain.Interfaces
{
    public interface IPartService: IService<Part>
    {
        IEnumerable<Part> GetPartsByCondition(PartCondition condition);
    }
}
