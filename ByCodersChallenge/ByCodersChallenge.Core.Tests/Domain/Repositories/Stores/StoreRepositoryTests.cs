using BasePoint.Core.Domain.Enumerators;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores;
using ByCodersChallenge.Core.Domain.Repositories.Stores;
using ByCodersChallenge.Core.Tests.Domain.Entities.Builders.Stores;
using FluentAssertions;
using Moq;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Domain.Repositories.Stores
{
    public class StoreRepositoryTests
    {
        private readonly Mock<IStoreCqrsCommandProvider> _commandProvider;
        private readonly StoreRepository _repository;

        public StoreRepositoryTests()
        {
            _commandProvider = new Mock<IStoreCqrsCommandProvider>();

            _repository = new StoreRepository(_commandProvider.Object);
        }

        [Fact]
        public async Task GetStoreByName_WhenCommandProviderReturnEntity_ShouldReturnEntityProxy()
        {
            var store = new StoreBuilder().Build();

            _commandProvider.Setup(x => x.GetStoreByName(It.IsAny<string>()))
                .ReturnsAsync(store);

            var selectedStore = await _repository.GetStoreByName("MERCEARIA 3 IRMÃOS");

            selectedStore.Should().BeEquivalentTo(store);
            selectedStore.GetType().Name.Should().Be("StoreProxy");
            selectedStore.State.Should().Be(EntityState.Unchanged);
        }
    }
}