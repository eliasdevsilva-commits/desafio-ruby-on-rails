using BasePoint.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.FinancialTransactions;
using ByCodersChallenge.Core.Domain.Repositories.FinancialTransactions;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.FinancialTransactions;
using FluentAssertions;
using Moq;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Domain.Repositories.FinancialTransactions
{
    public class FinancialTransactionRepositoryTests
    {
        private readonly Mock<IFinancialTransactionCqrsCommandProvider> _commandProvider;
        private readonly FinancialTransactionRepository _repository;

        public FinancialTransactionRepositoryTests()
        {
            _commandProvider = new Mock<IFinancialTransactionCqrsCommandProvider>();

            _repository = new FinancialTransactionRepository(_commandProvider.Object);
        }

        [Fact]
        public async Task GetById_WhenCommandProviderReturnEntity_ShouldReturnEntityProxy()
        {
            var transaction = new FinancialTransactionBuilder().Build();

            _commandProvider.Setup(x => x.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(transaction);

            var selectedTransaction = await _repository.GetById(Guid.NewGuid());

            selectedTransaction.Should().BeEquivalentTo(transaction);
            selectedTransaction.GetType().Name.Should().Be("FinancialTransactionProxy");
            selectedTransaction.State.Should().Be(EntityState.Unchanged);
        }
    }
}
