namespace ByCodersChallenge.Core.Application.Dtos.Stores
{
    public record ImportFinancialTransactionsInput
    {
        public byte[] File { get; init; }
    }
}