using BasePoint.Core.Cqrs.Dapper.EntityCommands;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Cqrs.Dapper.TableDefinitions.FinancialTransactions;
using Dapper;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.EntityCommands.FinancialTransactions
{
    public class FinancialTransactionCommand : DapperCommand<FinancialTransaction>
    {
        public FinancialTransactionCommand(IDbConnection connection, FinancialTransaction affectedEntity) : base(connection, affectedEntity)
        {
            AddTypeMapping(nameof(FinancialTransaction), FinancialTransactionTableDefinition.TableDefinition);
        }

        public override IList<CommandDefinition> CreateCommandDefinitions(FinancialTransaction entity)
        {
            CreateCommandDefinitionByState(entity);

            return CommandDefinitions;
        }
    }
}
