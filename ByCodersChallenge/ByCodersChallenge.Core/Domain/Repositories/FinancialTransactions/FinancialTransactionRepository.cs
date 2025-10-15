using BasePoint.Core.Domain.Repositories;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.FinancialTransactions;

namespace ByCodersChallenge.Core.Domain.Repositories.FinancialTransactions
{
    public class FinancialTransactionRepository : Repository<FinancialTransaction>, IFinancialTransactionRepository
    {
        private readonly IFinancialTransactionCqrsCommandProvider _financialTransactionCqrsCommandProvider;

        public FinancialTransactionRepository(IFinancialTransactionCqrsCommandProvider commandProvider) : base(commandProvider)
        {
            _financialTransactionCqrsCommandProvider = commandProvider;
        }
    }
}