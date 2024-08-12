using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Extensions.DependencyInjection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using NHibernatePoc.Infraestructure.Data.Mappings;

namespace NHibernatePoc.Infraestructure.Data.Configurations
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString, bool buildSchema)
        {
            var fluentConfiguration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                    .Driver<MicrosoftDataSqlClientDriver>()
                    .Dialect<MsSql2012Dialect>()
                    .ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<InventoryMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PartMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderDetailMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ShipmentMap>());
            var sessionFactory = fluentConfiguration.BuildSessionFactory();
            var configuration = fluentConfiguration.BuildConfiguration();

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());

            if (buildSchema)
            {
                new SchemaExport(configuration).Create(false, true);
            }

            return services;
        }
    }
}
