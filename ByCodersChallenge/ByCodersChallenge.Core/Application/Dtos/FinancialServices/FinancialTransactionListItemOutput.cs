using ByCodersChallenge.Core.Domain.Enumerators;

namespace ByCodersChallenge.Core.Application.Dtos.FinancialServices
{
    public record FinancialTransactionListItemOutput
    {
        public TransactionType Type { get; init; }
        public DateTime OccurrenceDate { get; init; }
        public decimal Value { get; init; }
        public string CPF { get; init; }
        public string Card { get; init; }
        public string StoreName { get; init; }
        public string StoreOwner { get; init; }
    }
}