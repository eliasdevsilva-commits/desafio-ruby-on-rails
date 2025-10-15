using BasePoint.Core.Application.UseCases;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.UseCases.FinancialTransactions
{
    public class ImportFinancialTransactionsUseCase : CommandUseCase<ImportFinancialTransactionsInput, ImportFinancialTransactionsOutput>
    {
        private readonly IConvertFinancialTransactionStringsToFinancialTransactions _convertFinancialTransactionStringsToFinancialTransactions;
        private readonly IStoreRepository _storeRepository;
        private readonly IFinancialTransactionRepository _financialTransactionRepository;
        private readonly IValidator<ImportFinancialTransactionsInput> _validator;

        protected override string SaveChangesErrorMessage => "Error while importing financial transactions";

        public ImportFinancialTransactionsUseCase(
            IValidator<ImportFinancialTransactionsInput> validator,
            IFinancialTransactionRepository financialTransactionRepository,
            IConvertFinancialTransactionStringsToFinancialTransactions convertFinancialTransactionStringsToFinancialTransactions,
            IStoreRepository storeRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _validator = validator;
            _convertFinancialTransactionStringsToFinancialTransactions = convertFinancialTransactionStringsToFinancialTransactions;
            _financialTransactionRepository = financialTransactionRepository;
            _storeRepository = storeRepository;
        }

        public override async Task<UseCaseOutput<ImportFinancialTransactionsOutput>> InternalExecuteAsync(ImportFinancialTransactionsInput input)
        {
            _validator.ValidateAndThrow(input);

            var transactions = _convertFinancialTransactionStringsToFinancialTransactions.Convert(input.MemoryStream);

            await PersistStoresFromTransactions(transactions);

            PersistTransactions(transactions);

            await SaveChangesAsync(); // Handle all UnitOfWork commands

            return CreateSuccessOutput(new ImportFinancialTransactionsOutput());
        }

        private void PersistTransactions(List<FinancialTransaction> transactions)
        {
            transactions.ForEach(transaction => _financialTransactionRepository.Persist(transaction, UnitOfWork));
        }

        private async Task PersistStoresFromTransactions(List<FinancialTransaction> transactions)
        {
            var distinctStores = transactions
                .Select(s => s.Store)
                .DistinctBy(s => s.Name)
                .ToList();

            var previousStores = new List<Store>();

            foreach (var store in distinctStores)
            {
                var previousStore = await _storeRepository.GetStoreByName(store.Name);

                if (previousStore is not null)
                    previousStores.Add(previousStore);
            }

            var storeNames = previousStores.Select(x => x.Name);

            distinctStores.RemoveAll(x => storeNames.Contains(x.Name)); // remove stores that already exists

            // Persist method dont go direct to database, but creates commands and add in a context
            distinctStores.ForEach(store => _storeRepository.Persist(store, UnitOfWork));

            foreach (var transaction in transactions)
            {
                // retrieve stores for database to endure integrity
                var previousStore = await _storeRepository.GetStoreByName(transaction.Store.Name);

                if (previousStore is not null)
                    transaction.UpdateStore(previousStore);
            }
        }
    }
}