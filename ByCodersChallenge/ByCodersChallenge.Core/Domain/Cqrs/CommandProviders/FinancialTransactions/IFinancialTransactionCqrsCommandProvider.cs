using BasePoint.Core.Domain.Cqrs.CommandProviders;
using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.FinancialTransactions
{
    public interface IFinancialTransactionCqrsCommandProvider : ICqrsCommandProvider<FinancialTransaction>
    {
    }
}