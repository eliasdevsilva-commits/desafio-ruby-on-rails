using BasePoint.Core.Exceptions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Shared;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.Stores;
using FluentAssertions;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Domain.Entities
{
    public class FinancialTransactionTests
    {
        public FinancialTransactionTests()
        {

        }

        [Fact]
        public void Constructor_EverythingIsOk_CreatesInstance()
        {
            var store = new StoreBuilder().Build();

            var ocurrenceDate = DateTime.Now;

            var entity = new FinancialTransaction(
                TransactionType.Credit,
                ocurrenceDate,
                100m,
                "55641815063",
                "1234****6678",
                store
                );

            entity.Type.Should().Be(TransactionType.Credit);
            entity.OccurrenceDate.Should().Be(ocurrenceDate);
            entity.Value.Should().Be(100m);
            entity.CPF.Should().Be("55641815063");
            entity.Card.Should().Be("1234****6678");
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_WhenStoreIsNullShouldThrowValidationException()
        {
            var store = new StoreBuilder().Build();

            var ocurrenceDate = DateTime.Now;

            Action createEntity = () => new FinancialTransaction(
                TransactionType.Credit,
                ocurrenceDate,
                100m,
                "55641815063",
                "1234****6678",
                null);

            createEntity.Should().Throw<ValidationException>()
               .WithMessage(SharedConstants.ErrorMessages.GivenStoreIsInvalid);
        }

        [Fact]
        public void UpdateStore_WhenGivenStoreIsValidShouldUpdateStoreProperty()
        {
            var store = new StoreBuilder()
                .WithName("BAR DO JOÃO")
                .WithOwner("JOÃO MACEDO")
                .Build();

            var newStore = new StoreBuilder()
                .WithName("MERCEARIA 3 IRMÃOS")
                .WithOwner("JOSÉ COSTA")
                .Build();

            var ocurrenceDate = DateTime.Now;

            var entity = new FinancialTransaction(
                TransactionType.Credit,
                ocurrenceDate,
                100m,
                "55641815063",
                "1234****6678",
                store
                );

            entity.UpdateStore(newStore);

            entity.Store.Name.Should().Be("MERCEARIA 3 IRMÃOS");
            entity.Store.Owner.Should().Be("JOSÉ COSTA");
        }

        [Fact]
        public void UpdateStore_WhenGivenStoreIsNullShouldThrowValidationException()
        {
            var store = new StoreBuilder()
                .WithName("BAR DO JOÃO")
                .WithOwner("JOÃO MACEDO")
                .Build();

            var ocurrenceDate = DateTime.Now;

            var entity = new FinancialTransaction(
                TransactionType.Credit,
                ocurrenceDate,
                100m,
                "55641815063",
                "1234****6678",
                store
                );

            Action updateStore = () => entity.UpdateStore(null);

            updateStore.Should().Throw<ValidationException>()
             .WithMessage(SharedConstants.ErrorMessages.GivenStoreIsInvalid);
        }
    }
}
