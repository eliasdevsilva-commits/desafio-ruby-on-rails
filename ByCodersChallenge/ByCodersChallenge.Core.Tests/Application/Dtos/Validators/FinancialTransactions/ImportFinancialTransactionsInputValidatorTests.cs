using BasePoint.Core.Extensions;
using ByCodersChallenge.Core.Application.Dtos.Validators.FinancialTransactions;
using ByCodersChallenge.Core.Shared;
using ByCodersChallenge.Core.Tests.Application.Dtos.Builders.Stores;
using FluentAssertions;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Application.Dtos.Validators.FinancialTransactions
{
    public class ImportFinancialTransactionsInputValidatorTests
    {
        private readonly ImportFinancialTransactionsInputValidator _validator;

        public ImportFinancialTransactionsInputValidatorTests()
        {
            _validator = new ImportFinancialTransactionsInputValidator();
        }


        [Fact]
        public void Validate_InputIsValid_ReturnsIsValid()
        {
            var input = new ImportFinancialTransactionsInputBuilder()
                .Build();

            var validationResult = _validator.Validate(input);

            validationResult.IsValid.Should().BeTrue();
        }

        [Fact]
        public void Validate_WhenMemoryStreamIsNull_ReturnsIsValid()
        {
            var input = new ImportFinancialTransactionsInputBuilder()
                .WithMemoryStream(null)
                .Build();

            var validationResult = _validator.Validate(input);

            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors.Where(e =>
                e.ErrorMessage == SharedConstants.ErrorMessages.PropertyIsInvalid.Format(nameof(MemoryStream)))
                    .Should().HaveCount(1);
        }

        [Fact]
        public void Validate_WhenMemoryStreamIsEmpty_ReturnsIsValid()
        {
            var input = new ImportFinancialTransactionsInputBuilder()
                .WithMemoryStream(new MemoryStream())
                .Build();

            var validationResult = _validator.Validate(input);

            validationResult.IsValid.Should().BeFalse();

            validationResult.Errors.Where(e =>
               e.ErrorMessage == SharedConstants.ErrorMessages.FileIsEmpty)
                   .Should().HaveCount(1);
        }
    }
}
