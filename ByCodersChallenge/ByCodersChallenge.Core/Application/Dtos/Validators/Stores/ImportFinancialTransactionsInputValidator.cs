using BasePoint.Core.Extensions;
using ByCodersChallenge.Core.Application.Dtos.Stores;
using ByCodersChallenge.Core.Shared;
using FluentValidation;

namespace ByCodersChallenge.Core.Application.Dtos.Validators.Stores
{
    public class ImportFinancialTransactionsInputValidator : AbstractValidator<ImportFinancialTransactionsInput>
    {
        public ImportFinancialTransactionsInputValidator()
        {
            RuleFor(v => v.MemoryStream)
                .NotNull()
                .WithMessage(SharedConstants.ErrorMessages.PropertyIsInvalid.Format(nameof(MemoryStream)));

            When(v => v.MemoryStream != null, () =>
            {
                RuleFor(v => v.MemoryStream.Length)
               .GreaterThan(0)
               .WithMessage(SharedConstants.ErrorMessages.FileIsEmpty);
            });
        }
    }
}