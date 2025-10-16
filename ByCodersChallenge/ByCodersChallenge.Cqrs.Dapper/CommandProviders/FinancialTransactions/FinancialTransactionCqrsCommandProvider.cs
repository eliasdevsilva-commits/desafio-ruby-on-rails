using BasePoint.Core.Cqrs.Dapper.CommandProviders;
using BasePoint.Core.Domain.Cqrs;
using BasePoint.Core.Shared;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Cqrs.Dapper.EntityCommands.FinancialTransactions;
using Dapper;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.CommandProviders.FinancialTransactions
{
    public class FinancialTransactionCqrsCommandProvider : DapperCqrsCommandProvider<FinancialTransaction>, IFinancialTransactionCqrsCommandProvider
    {
        private readonly string SqlSelectCommand = @"SELECT
											 t.Id,
											 t.Type,
											 t.OccurrenceDate,
											 t.Value,
											 t.CPF,
											 t.Card,
											 s.Name,
											 s.Owner,
											 FROM financialtransaction t
											 INNER JOIN store s ON (s.Id = t.StoreId)";

        public FinancialTransactionCqrsCommandProvider(IDbConnection connection) : base(connection)
        {
        }

        public override IEntityCommand GetAddCommand(FinancialTransaction entity)
        {
            return new FinancialTransactionCommand(_connection, entity);
        }

        public override IEntityCommand GetDeleteCommand(FinancialTransaction entity)
        {
            return new FinancialTransactionCommand(_connection, entity);
        }

        public override IEntityCommand GetUpdateCommand(FinancialTransaction entity)
        {
            return new FinancialTransactionCommand(_connection, entity);
        }

        public override async Task<FinancialTransaction> GetById(Guid id)
        {
            var sql = SqlSelectCommand
                 + Constants.StringEnter
                 + "Where" + Constants.StringEnter
                 + "s.Id = @Id;";

            var parameters = new DynamicParameters();

            parameters.Add("@Id", id);

            return await _connection.QueryFirstOrDefaultAsync<FinancialTransaction>(sql, parameters);
        }
    }
}
