using BasePoint.Core.Domain.Entities;
using BasePoint.Core.Exceptions;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Extensions;
using ByCodersChallenge.Core.Shared;

namespace ByCodersChallenge.Core.Domain.Entities
{
    public class FinancialTransaction : BaseEntity
    {
        protected FinancialTransaction()
        {
        }

        public FinancialTransaction(
            TransactionType type,
            DateTime occurrenceDate,
            decimal value,
            string cpf,
            string card,
            Store store) : this()
        {
            Type = type;
            OccurrenceDate = occurrenceDate;
            Value = value;
            CPF = cpf;
            Card = card;
            UpdateStore(store);
        }

        private decimal _decimalValue;
        public virtual TransactionType Type { get; protected set; }
        public virtual DateTime OccurrenceDate { get; protected set; }
        public virtual decimal Value { get { return Type.GetSignedValue(Math.Abs(_decimalValue)); } protected set { _decimalValue = Math.Abs(value); } }
        public virtual string CPF { get; protected set; }
        public virtual string Card { get; protected set; }

        public virtual Store Store { get; protected set; }

        public void UpdateStore(Store store)
        {
            ValidationException.ThrowIfNull(store, SharedConstants.ErrorMessages.GivenStoreIsInvalid);

            Store = store;
        }
    }
}