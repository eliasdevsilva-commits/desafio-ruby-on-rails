using BasePoint.Core.Cqrs.Dapper.TableDefinitions;
using ByCodersChallenge.Core.Domain.Entities;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.TableDefinitions.Stores
{
    public class StoreTableDefinition
    {
        public static readonly DapperTableDefinition TableDefinition = new DapperTableDefinition
        {
            TableName = "Store",
            ColumnDefinitions = new List<DapperTableColumnDefinition>()
            {
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Id",
                    EntityFieldName = nameof(Store.Id),
                    Type = DbType.Guid
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Name",
                    EntityFieldName = nameof(Store.Name),
                    Type = DbType.AnsiString,
                    Size = 19
                },
                new DapperTableColumnDefinition
                {
                    DbFieldName = "Owner",
                    EntityFieldName = nameof(Store.Owner),
                    Type = DbType.AnsiString,
                    Size = 14
                },
            }
        };
    }
}
