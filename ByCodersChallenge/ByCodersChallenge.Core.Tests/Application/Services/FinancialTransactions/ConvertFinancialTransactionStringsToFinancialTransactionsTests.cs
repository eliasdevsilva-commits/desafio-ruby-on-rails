using ByCodersChallenge.Core.Application.Services.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using FluentAssertions;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Application.Services.FinancialTransactions
{
    public class ConvertFinancialTransactionStringsToFinancialTransactionsTests
    {
        private readonly ConvertFinancialTransactionStringsToFinancialTransactions _converter;

        public ConvertFinancialTransactionStringsToFinancialTransactionsTests()
        {
            _converter = new ConvertFinancialTransactionStringsToFinancialTransactions();
        }

        [Fact]
        public void Convert_WhenEverythingIsOk_ReturnsFinancialTransactions()
        {
            using var fileStream = new FileStream("Application/Services/FinancialTransactions/Scenarios/CNAB.txt", FileMode.Open);

            var memoryStream = new MemoryStream();

            fileStream.CopyTo(memoryStream);

            var transactions = _converter.Convert(memoryStream);

            transactions.Should().NotBeNull();
            transactions.Should().BeOfType<List<FinancialTransaction>>();
            transactions.Should().HaveCount(21);

            var firstTransaction = transactions.First();

            firstTransaction.Type.Should().Be(TransactionType.Financing);
            firstTransaction.OccurrenceDate.Should().Be(new DateTime(2019, 03, 01, 15, 34, 53));
            firstTransaction.Value.Should().Be(142.00m);
            firstTransaction.CPF.Should().Be("09620676017");
            firstTransaction.Card.Should().Be("4753****3153");
            firstTransaction.Store.Owner.Should().Be("JOÃO MACEDO");
            firstTransaction.Store.Name.Should().Be("BAR DO JOÃO");
        }
    }
}