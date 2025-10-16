using BasePoint.Core.Cqrs.Dapper.TableDefinitions;
using ByCodersChallenge.Core.Domain.Entities;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.TableDefinitions.FinancialTransactions
{
    public class FinancialTransactionTableDefinition
    {

        public static readonly DapperTableDefinition TableDefinition = new DapperTableDefinition
        {
            TableName = "financialtransaction",
            ColumnDefinitions = new List<DapperTableColumnDefinition>()
            {
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Id",
                    EntityFieldName = nameof(FinancialTransaction.Id),
                    Type = DbType.Guid
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Type",
                    EntityFieldName = nameof(FinancialTransaction.Type),
                    Type = DbType.AnsiString,
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "OccurrenceDate",
                    EntityFieldName = nameof(FinancialTransaction.OccurrenceDate),
                    Type = DbType.DateTime,
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Value",
                    EntityFieldName = nameof(FinancialTransaction.Value),
                    Type = DbType.Decimal,
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "CPF",
                    EntityFieldName = nameof(FinancialTransaction.CPF),
                    Type = DbType.AnsiString,
                    Size = 11
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Card",
                    EntityFieldName = nameof(FinancialTransaction.Card),
                    Type = DbType.AnsiString,
                    Size = 12
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "StoreId",
                    EntityFieldName = nameof(FinancialTransaction.Store),
                    Type = DbType.AnsiString,
                },
            }
        };
    }
}
