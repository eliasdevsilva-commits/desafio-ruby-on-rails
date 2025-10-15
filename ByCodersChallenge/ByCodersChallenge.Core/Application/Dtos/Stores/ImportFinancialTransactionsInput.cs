namespace ByCodersChallenge.Core.Application.Dtos.Stores
{
    public record ImportFinancialTransactionsInput
    {
        public MemoryStream MemoryStream { get; init; }
    }
}