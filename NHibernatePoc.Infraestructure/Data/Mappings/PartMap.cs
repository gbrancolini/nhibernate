using FluentNHibernate.Mapping;
using NHibernatePoc.Core.Domain.Entities;

namespace NHibernatePoc.Infraestructure.Data.Mappings
{
    public class PartMap : ClassMap<Part>
    {
        public PartMap()
        {
            Table("Parts");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name);
            Map(x => x.Price);
            Map(x => x.Condition).CustomType<string>();        }
    }
}
