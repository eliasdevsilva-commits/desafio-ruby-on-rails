using BasePoint.Core.Application.Dtos.Input;
using BasePoint.Core.Cqrs.Dapper.Extensions;
using BasePoint.Core.Cqrs.Dapper.QueryProviders;
using BasePoint.Core.Extensions;
using BasePoint.Core.Shared;
using ByCodersChallenge.Core.Application.Cqrs.QueryProviders.FinancialTransactions;
using ByCodersChallenge.Core.Application.Dtos.FinancialServices;
using Dapper;
using System.Data;

namespace ByCodersChallenge.Cqrs.Dapper.QueryProviders.FinancialTransactions
{
    public class FinancialTransactionListItemOutputCqrsQueryProvider : DapperCqrsListItemOutputQueryProvider<FinancialTransactionListItemOutput>, IFinancialTransactionListItemOutputCqrsQueryProvider
    {
        private readonly string SqlSelectCommand = @"SELECT
											 t.Id,
											 t.Type,
											 t.OccurrenceDate,
											 t.Value,
											 t.CPF,
											 t.Card,
											 s.Name as StoreName,
											 s.Owner as StoreOwner
											 FROM FinancialTransaction t
											 INNER JOIN Store s ON (s.Id = t.StoreId)";

        private readonly string SqlCountSelectCommand = @"SELECT
											 Count(t.Id) as Count
											 FROM FinancialTransaction t";

        public FinancialTransactionListItemOutputCqrsQueryProvider(IDbConnection connection) : base(connection)
        {
        }

        public override async Task<int> Count(IList<SearchFilterInput> filters)
        {
            var sqlCommand = SqlCountSelectCommand
                 + Constants.StringEnter;

            Dictionary<string, object> parameters;
            string sqlFilters;

            CreateParameters(filters, out parameters, out sqlFilters);

            sqlCommand += (!sqlFilters.IsNullOrEmpty() ? " WHERE " : string.Empty) + sqlFilters;

            var dapperParameters = new DynamicParameters(parameters);

            return await _connection.ExecuteScalarAsync<int>(sqlCommand, dapperParameters);
        }

        public override async Task<IList<FinancialTransactionListItemOutput>> GetPaginatedResults(IList<SearchFilterInput> filters, int pageNumber, int itemsPerPage)
        {
            var sqlCommand = SqlSelectCommand
                 + Constants.StringEnter;

            Dictionary<string, object> parameters;
            string sqlFilters;

            CreateParameters(filters, out parameters, out sqlFilters);

            sqlCommand += (!sqlFilters.IsNullOrEmpty() ? " WHERE " : string.Empty) + sqlFilters +
                    " ORDER BY TYPE LIMIT @PAGE_NUMBER, @ITENS_PER_PAGE ";

            parameters.Add("PAGE_NUMBER", (pageNumber - 1) * itemsPerPage);
            parameters.Add("ITENS_PER_PAGE", itemsPerPage);

            var dapperParameters = new DynamicParameters(parameters);

            return (await _connection.QueryAsync<FinancialTransactionListItemOutput>(sqlCommand, parameters)).ToList();
        }

        private static void CreateParameters(IList<SearchFilterInput> filters, out Dictionary<string, object> parameters, out string sqlFilters)
        {
            parameters = new Dictionary<string, object>();

            sqlFilters = string.Empty;
            foreach (var filter in filters)
            {
                if (filter.FilterValue is not null)
                {
                    if (!string.IsNullOrWhiteSpace(sqlFilters))
                        sqlFilters += " AND ";

                    sqlFilters += filter.GetSqlFilter();

                    var filterParameters = filter.GetParameters();

                    foreach (var param in filterParameters)
                    {
                        parameters.Add(param.Key, param.Value);
                    }

                }
            }
        }
    }
}
