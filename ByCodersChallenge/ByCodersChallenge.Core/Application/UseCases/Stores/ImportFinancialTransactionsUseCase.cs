using BasePoint.Core.Application.UseCases;
using BasePoint.Core.UnitOfWork.Interfaces;
using ByCodersChallenge.Core.Application.Dtos.Stores;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.UseCases.Stores
{
    public class ImportFinancialTransactionsUseCase : CommandUseCase<ImportFinancialTransactionsInput, ImportFinancialTransactionsOutput>
    {
        private readonly IValidator<ImportFinancialTransactionsInput> _validator;

        protected override string SaveChangesErrorMessage => "Error while importing financial transactions";

        public ImportFinancialTransactionsUseCase(
            IValidator<ImportFinancialTransactionsInput> validator,
            IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _validator = validator;
        }

        public override async Task<UseCaseOutput<ImportFinancialTransactionsOutput>> InternalExecuteAsync(ImportFinancialTransactionsInput input)
        {
            _validator.ValidateAndThrow(input);

            await SaveChangesAsync();

            return CreateSuccessOutput(new ImportFinancialTransactionsOutput());
        }
    }
}