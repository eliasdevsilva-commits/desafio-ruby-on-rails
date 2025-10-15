using BasePoint.Core.Cqrs.Dapper.Handlers;
using BasePoint.Core.Cqrs.Dapper.UnitOfWork;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Cqrs.QueryProviders.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores;
using ByCodersChallenge.Core.Domain.Repositories.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;
using ByCodersChallenge.Core.Domain.Repositories.Stores;
using ByCodersChallenge.Cqrs.Dapper.CommandProviders.FinancialTransactions;
using ByCodersChallenge.Cqrs.Dapper.CommandProviders.Stores;
using ByCodersChallenge.Cqrs.Dapper.QueryProviders.FinancialTransactions;
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
            service.AddSingleton<IFinancialTransactionCqrsCommandProvider, FinancialTransactionCqrsCommandProvider>();
        }

        public static void MapQueryProviders(this IServiceCollection service)
        {
            service.AddSingleton<IFinancialTransactionListItemOutputCqrsQueryProvider, FinancialTransactionListItemOutputCqrsQueryProvider>();
        }

        public static void MapRepositories(this IServiceCollection service)
        {
            service.AddSingleton<IStoreRepository, StoreRepository>();
            service.AddSingleton<IFinancialTransactionRepository, FinancialTransactionRepository>();
        }
    }
}