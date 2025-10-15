using BasePoint.Core.Application.Cqrs.QueryProviders;
using ByCodersChallenge.Core.Application.Dtos.FinancialServices;

namespace ByCodersChallenge.Core.Application.Cqrs.QueryProviders.FinancialTransactions
{
    public interface IFinancialTransactionListItemOutputCqrsQueryProvider : IListItemOutputCqrsQueryProvider<FinancialTransactionListItemOutput>
    {
    }
}