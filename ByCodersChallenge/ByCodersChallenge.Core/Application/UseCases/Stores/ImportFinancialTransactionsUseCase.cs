using BasePoint.Core.Application.UseCases;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Dtos.Stores;
using ByCodersChallenge.Core.Application.Services.Stores.Interfaces;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.UseCases.Stores
{
    public class ImportFinancialTransactionsUseCase : CommandUseCase<ImportFinancialTransactionsInput, ImportFinancialTransactionsOutput>
    {
        private readonly IConvertFinancialTransactionStringsToFinancialTransactions _convertFinancialTransactionStringsToFinancialTransactions;
        private readonly IValidator<ImportFinancialTransactionsInput> _validator;

        protected override string SaveChangesErrorMessage => "Error while importing financial transactions";

        public ImportFinancialTransactionsUseCase(
            IValidator<ImportFinancialTransactionsInput> validator,
            IConvertFinancialTransactionStringsToFinancialTransactions convertFinancialTransactionStringsToFinancialTransactions,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _validator = validator;
            _convertFinancialTransactionStringsToFinancialTransactions = convertFinancialTransactionStringsToFinancialTransactions;
        }

        public override async Task<UseCaseOutput<ImportFinancialTransactionsOutput>> InternalExecuteAsync(ImportFinancialTransactionsInput input)
        {
            _validator.ValidateAndThrow(input);

            _convertFinancialTransactionStringsToFinancialTransactions.Convert(input.MemoryStream);

            await SaveChangesAsync();

            return CreateSuccessOutput(new ImportFinancialTransactionsOutput());
        }
    }
}