using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Extensions;
using FluentAssertions;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Extensions
{
    public class TransactionTypeExtensionTests
    {
        public TransactionTypeExtensionTests()
        {
        }

        /*
          return transactionType switch
            {
                Domain.Enumerators.TransactionType.Debit => 1,
                Domain.Enumerators.TransactionType.Boleto => -1,
                Domain.Enumerators.TransactionType.Financing => -1,
                Domain.Enumerators.TransactionType.Credit => 1,
                Domain.Enumerators.TransactionType.LoanReceipt => 1,
                Domain.Enumerators.TransactionType.Sales => 1,
                Domain.Enumerators.TransactionType.TEDReceipt => 1,
                Domain.Enumerators.TransactionType.DOCReceipt => 1,
                Domain.Enumerators.TransactionType.Rent => -1,
                _ => throw new ArgumentOutOfRangeException(nameof(transactionType), transactionType, null)
            };
         */

        [Theory]
        [InlineData(TransactionType.Debit, 1)]
        [InlineData(TransactionType.Boleto, -1)]
        [InlineData(TransactionType.Financing, -1)]
        [InlineData(TransactionType.Credit, 1)]
        [InlineData(TransactionType.LoanReceipt, 1)]
        [InlineData(TransactionType.Sales, 1)]
        [InlineData(TransactionType.TEDReceipt, 1)]
        [InlineData(TransactionType.DOCReceipt, 1)]
        [InlineData(TransactionType.Rent, -1)]
        public void GetSignal_ReturnsCorrectSignalMultiplier(TransactionType type, int expectedMultiplier)
        {
            var multiplier = type.GetSignal();

            multiplier.Should().Be(expectedMultiplier);
        }

        [Fact]
        public void GetSignal_WhenTypeIsInvalid__ShouldThrowValidationException()
        {
            const int INVALID_ENUM_VALUE = -1000;

            var type = (TransactionType)INVALID_ENUM_VALUE;

            Action createEntity = () => type.GetSignal();

            createEntity.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Theory]
        [InlineData(TransactionType.Debit, 1)]
        [InlineData(TransactionType.Boleto, -1)]
        [InlineData(TransactionType.Financing, -1)]
        [InlineData(TransactionType.Credit, 1)]
        [InlineData(TransactionType.LoanReceipt, 1)]
        [InlineData(TransactionType.Sales, 1)]
        [InlineData(TransactionType.TEDReceipt, 1)]
        [InlineData(TransactionType.DOCReceipt, 1)]
        [InlineData(TransactionType.Rent, -1)]
        public void GetSignal_WhenTypeIsInvalid_ShouldalMultiplier(TransactionType type, int expectedMultiplier)
        {
            var multiplier = type.GetSignal();

            multiplier.Should().Be(expectedMultiplier);
        }
    }
}
