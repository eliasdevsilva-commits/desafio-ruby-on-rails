using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.Stores;

namespace ByCodersChallenge.Core.Tests.Domain.Entities.Builders.FinancialTransactions
{
    public class FinancialTransactionBuilder
    {
        private TransactionType _type;
        private DateTime _occurrenceDate;
        private decimal _value;
        private string _cpf;
        private string _card;
        private Store _store;
        public FinancialTransactionBuilder()
        {
            _type = TransactionType.Credit;
            _occurrenceDate = DateTime.Now;
            _value = 100m;
            _cpf = "55641815063";
            _card = "1234****6678";
            _store = new StoreBuilder().Build();
        }

        public FinancialTransactionBuilder WithType(TransactionType type)
        {
            _type = type;
            return this;
        }

        public FinancialTransactionBuilder WithOccurrenceDate(DateTime occurrenceDate)
        {
            _occurrenceDate = occurrenceDate;
            return this;
        }

        public FinancialTransactionBuilder WithValue(decimal value)
        {
            _value = value;
            return this;
        }

        public FinancialTransactionBuilder WithCPF(string cpf)
        {
            _cpf = cpf;
            return this;
        }

        public FinancialTransactionBuilder WithCard(string card)
        {
            _card = card;
            return this;
        }

        public FinancialTransactionBuilder WithStore(Store store)
        {
            _store = store;
            return this;
        }

        public FinancialTransaction Build()
        {
            return new FinancialTransaction(
                _type,
                _occurrenceDate,
                _value,
                _cpf,
                _card,
                _store);
        }
    }
}
