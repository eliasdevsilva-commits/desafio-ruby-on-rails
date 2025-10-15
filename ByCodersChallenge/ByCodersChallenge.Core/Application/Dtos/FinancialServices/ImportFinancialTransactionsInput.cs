namespace ByCodersChallenge.Core.Application.Dtos.FinancialTransactions
{
    public record ImportFinancialTransactionsInput
    {
        public MemoryStream MemoryStream { get; init; }
    }
}