namespace ByCodersChallenge.Core.Application.Dtos.Stores
{
    public record ImportFinancialTransactionsInput
    {
        public List<string> TransactionLines { get; init; }
    }
}