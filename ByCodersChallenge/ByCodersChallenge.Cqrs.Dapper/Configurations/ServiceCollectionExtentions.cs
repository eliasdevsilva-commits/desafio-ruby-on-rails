using BasePoint.Core.Cqrs.Dapper.Handlers;
using BasePoint.Core.Cqrs.Dapper.UnitOfWork;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;
using ByCodersChallenge.Core.Domain.Repositories.Stores;
using ByCodersChallenge.Cqrs.Dapper.CommandProviders.Stores;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.Configurations
{
    public static class ServiceCollectionExtentions
    {
        public static void MapUnitOfWork(this IServiceCollection service)
        {
            service.AddSingleton<IUnitOfWork, DapperUnitOfWork>();
        }

        public static void MapConnection(this IServiceCollection service, IConfigurationSection section)
        {
            var connectionString =
               "Server=" + section["DatabaseServerName"] +
               ";Database=" + section["DatabaseName"] +
               ";Uid=" + section["DatabaseUser"] +
               ";Pwd=" + section["DatabasePassWord"] + ";";

            var connection = new MySqlConnection(connectionString);

            service.AddSingleton(c => connection);
            service.AddSingleton<IDbConnection, MySqlConnection>(c => connection);
            SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
        }

        public static void MapCommandProviders(this IServiceCollection service)
        {
            service.AddSingleton<IUnitOfWork, DapperUnitOfWork>();
            service.AddSingleton<IStoreCqrsCommandProvider, StoreCqrsCommandProvider>();
        }

        public static void MapQueryProviders(this IServiceCollection service)
        {
        }

        public static void MapRepositories(this IServiceCollection service)
        {
            service.AddSingleton<IStoreRepository, StoreRepository>();
        }
    }
}