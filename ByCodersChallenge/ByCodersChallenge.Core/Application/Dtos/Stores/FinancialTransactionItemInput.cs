using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;

namespace ByCodersChallenge.Core.Application.Dtos.Stores
{
    public record FinancialTransactionItemInput
    {
        public TransactionType Type { get; init; }
        public DateTime OccurrenceDate { get; init; }
        public decimal Value { get; init; }
        public string CPF { get; init; }
        public string Card { get; init; }
        public Store StoreName { get; init; }
        public Store StoreOwner { get; init; }
    }
}