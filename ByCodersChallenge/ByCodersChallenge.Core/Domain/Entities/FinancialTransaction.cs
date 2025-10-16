using BasePoint.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Extensions;

namespace ByCodersChallenge.Core.Domain.Entities
{
    public class FinancialTransaction : BaseEntity
    {
        public FinancialTransaction(
            TransactionType type,
            DateTime occurrenceDate,
            decimal value,
            string cpf,
            string card,
            Store store)
        {
            Type = type;
            OccurrenceDate = occurrenceDate;
            Value = value;
            CPF = cpf;
            Card = card;
            Store = store;
        }

        private decimal _decimalValue;
        public virtual TransactionType Type { get; protected set; }
        public virtual DateTime OccurrenceDate { get; protected set; }
        public virtual decimal Value { get { return Type.GetSignedValue(Math.Abs(_decimalValue)); } protected set { _decimalValue = Math.Abs(value); } }
        public virtual string CPF { get; protected set; }
        public virtual string Card { get; protected set; }

        public virtual Store Store { get; protected set; }

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