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

        public void UpdateCard(string card)
        {
            throw new NotImplementedException();
        }

        public void UpdateCPF(string cpf)
        {
            throw new NotImplementedException();
        }

        public void UpdateOccurrenceDate(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void UpdateType(TransactionType transactionType)
        {
            throw new NotImplementedException();
        }

        public void UpdateValue(decimal value)
        {
            throw new NotImplementedException();
        }

        public void UpdateStore(Store store)
        {
            Store = store;
        }
    }
}