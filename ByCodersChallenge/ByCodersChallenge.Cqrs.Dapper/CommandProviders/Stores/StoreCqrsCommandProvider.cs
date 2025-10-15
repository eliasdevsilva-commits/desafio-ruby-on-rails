using BasePoint.Core.Cqrs.Dapper.CommandProviders;
using BasePoint.Core.Domain.Cqrs;
using BasePoint.Core.Shared;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Cqrs.Dapper.EntityCommands.Stores;
using Dapper;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.CommandProviders.Stores
{
    public class StoreCqrsCommandProvider : DapperCqrsCommandProvider<Store>, IStoreCqrsCommandProvider
    {
        private readonly string SqlSelectCommand = @"SELECT
											 s.Id,
											 s.Name,
											 s.Owner
											 FROM Store s";


        public StoreCqrsCommandProvider(IDbConnection connection) : base(connection)
        {
        }

        public override IEntityCommand GetAddCommand(Store entity)
        {
            return new StoreCommand(_connection, entity);
        }

        public override IEntityCommand GetDeleteCommand(Store entity)
        {
            return new StoreCommand(_connection, entity);
        }

        public override IEntityCommand GetUpdateCommand(Store entity)
        {
            return new StoreCommand(_connection, entity);
        }

        public override async Task<Store> GetById(Guid id)
        {
            var sql = SqlSelectCommand
                 + Constants.StringEnter
                 + "Where" + Constants.StringEnter
                 + "t.Id = @Id;";

            var parameters = new DynamicParameters();

            parameters.Add("@Id", id);

            return await _connection.QueryFirstOrDefaultAsync<Store>(sql, parameters);
        }

        public async Task<Store> GetStoreByName(string name)
        {
            var sql = SqlSelectCommand
                 + Constants.StringEnter
                 + "Where" + Constants.StringEnter
                 + "t.Name = @Name;";

            var parameters = new DynamicParameters();

            parameters.Add("@Name", name);

            return await _connection.QueryFirstOrDefaultAsync<Store>(sql, parameters);
        }

        public async Task<Store> GetAnotherStoreByName(Store store, string name)
        {
            var sql = SqlSelectCommand
                 + Constants.StringEnter
                 + "Where" + Constants.StringEnter
                 + "t.Name = @Name and t.Id <> @Id";

            var parameters = new DynamicParameters();

            parameters.Add("@Name", name);
            parameters.Add("@Id", store.Id);

            return await _connection.QueryFirstOrDefaultAsync<Store>(sql, parameters);
        }
    }
}