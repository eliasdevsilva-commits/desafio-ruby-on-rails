using BasePoint.Core.Application.UseCases;
using BasePoint.Core.Extensions;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Dtos.FinancialTransactions;
using ByCodersChallenge.Core.Application.Services.FinancialTransactions.Interfaces;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.UseCases.FinancialTransactions
{
    public class ImportFinancialTransactionsUseCase : CommandUseCase<ImportFinancialTransactionsInput, ImportFinancialTransactionsOutput>
    {
        private readonly IConvertFinancialTransactionStringsToFinancialTransactions _convertFinancialTransactionStringsToFinancialTransactions;
        private readonly IStoreRepository _storeRepository;
        private readonly IValidator<ImportFinancialTransactionsInput> _validator;

        protected override string SaveChangesErrorMessage => "Error while importing financial transactions";

        public ImportFinancialTransactionsUseCase(
            IValidator<ImportFinancialTransactionsInput> validator,
            IConvertFinancialTransactionStringsToFinancialTransactions convertFinancialTransactionStringsToFinancialTransactions,
            IStoreRepository storeRepository,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _validator = validator;
            _convertFinancialTransactionStringsToFinancialTransactions = convertFinancialTransactionStringsToFinancialTransactions;
            _storeRepository = storeRepository;
        }

        public override async Task<UseCaseOutput<ImportFinancialTransactionsOutput>> InternalExecuteAsync(ImportFinancialTransactionsInput input)
        {
            _validator.ValidateAndThrow(input);

            var transactions = _convertFinancialTransactionStringsToFinancialTransactions.Convert(input.MemoryStream);

            PersistStoresFromTransactions(transactions);

            await SaveChangesAsync();

            return CreateSuccessOutput(new ImportFinancialTransactionsOutput());
        }

        private void PersistStoresFromTransactions(List<FinancialTransaction> transactions)
        {
            var distinctStores = transactions
                .Select(s => s.Store)
                .DistinctBy(s => s.Name);

            // Persist method dont go direct to database, but creates commands and add in a context
            distinctStores.ForEach(store => _storeRepository.Persist(store, UnitOfWork));
        }
    }
}