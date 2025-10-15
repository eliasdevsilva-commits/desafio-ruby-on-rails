using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces
{
    public interface IConvertFinancialTransactionStringsToFinancialTransactions
    {
        List<FinancialTransaction> Convert(MemoryStream memoryStream);
    }
}