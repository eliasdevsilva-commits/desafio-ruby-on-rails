using BasePoint.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;

namespace ByCodersChallenge.Core.Domain.Entities
{
    public class FinancialTransaction(
        TransactionType type,
        DateTime occurrenceDate,
        decimal value,
        string cpf,
        string card,
        Store store) : BaseEntity
    {
        public virtual TransactionType Type { get; protected set; } = type;
        public virtual DateTime OccurrenceDate { get; protected set; } = occurrenceDate;
        public virtual decimal Value { get; protected set; } = value;
        public virtual string CPF { get; protected set; } = cpf;
        public virtual string Card { get; protected set; } = card;

        public virtual Store Store { get; protected set; } = store;
    }
}