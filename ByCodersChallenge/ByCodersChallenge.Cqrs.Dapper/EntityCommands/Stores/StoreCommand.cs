using BasePoint.Core.Cqrs.Dapper.EntityCommands;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Cqrs.Dapper.TableDefinitions.Stores;
using Dapper;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.EntityCommands.Stores
{
    public class StoreCommand : DapperCommand<Store>
    {
        public StoreCommand(IDbConnection connection, Store affectedEntity) : base(connection, affectedEntity)
        {
            AddTypeMapping(nameof(Store), StoreTableDefinition.TableDefinition);
        }

        public override IList<CommandDefinition> CreateCommandDefinitions(Store entity)
        {
            CreateCommandDefinitionByState(entity);

            return CommandDefinitions;
        }
    }
}
