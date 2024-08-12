using Microsoft.Extensions.Configuration;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NHibernatePoc.Infraestructure.Data.Mappings;
using NHibernate.Driver;
using NHibernate.Dialect;

namespace NHibernatePoc.Infraestructure.Data.Configurations
{
    public static class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static Configuration _configuration;

        public static void Initialize(IConfiguration config)
        {
            var nhConfig = Fluently.Configure()
            .Database(MsSqlConfiguration.MsSql2012
                .Driver<MicrosoftDataSqlClientDriver>()
                .Dialect<MsSql2012Dialect>()
                .ConnectionString(config.GetConnectionString("DefaultConnection")))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<InventoryMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PartMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<OrderDetailMap>())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ShipmentMap>())
            .BuildConfiguration();

            _configuration = nhConfig;
            _sessionFactory = nhConfig.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }

        public static void UpdateSchema()
        {
            new SchemaUpdate(_configuration).Execute(false, true);
        }

        public static void CreateSchema()
        {
            new SchemaExport(_configuration).Create(false, true);
        }
    }
}