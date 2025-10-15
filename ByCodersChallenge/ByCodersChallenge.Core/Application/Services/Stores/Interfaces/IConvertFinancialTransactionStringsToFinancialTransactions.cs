using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Application.Services.Stores.Interfaces
{
    public interface IConvertFinancialTransactionStringsToFinancialTransactions
    {
        List<FinancialTransaction> Convert(MemoryStream memoryStream);
    }
}