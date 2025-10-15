using BasePoint.Core.Extensions;
using ByCodersChallenge.Core.Application.Dtos.Stores;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.Dtos.Validators.Stores
{
    public class ImportFinancialTransactionsInputValidator : AbstractValidator<ImportFinancialTransactionsInput>
    {
        public ImportFinancialTransactionsInputValidator()
        {
            RuleFor(v => v.TransactionLines)
                .NotEmpty()
                .WithMessage("Transaction lines cannot be empty.");

            When(v => !v.TransactionLines.IsNullOrEmpty(), () =>
            {
                RuleFor(v => !v.TransactionLines.HasDuplicates(x => x))
                    .NotEmpty()
                    .WithMessage("There are duplicated transactions.");

                RuleFor(v => !v.TransactionLines.Any(x => x.IsNullOrEmpty()))
                   .NotEmpty()
                   .WithMessage("There are invalid transactions.");
            });
        }
    }
}