using BasePoint.Core.Exceptions;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Shared;
using FluentAssertions;
using Xunit;

namespace ByCodersChallenge.Core.Tests.Domain.Entities.Stores
{
    public class StoreTests
    {
        public StoreTests()
        {
        }

        [Fact]
        public void Constructor_EverythingIsOk_CreatesInstance()
        {
            var entity = new Store("BAR DO JOÃO", "JOÃO MACEDO");

            entity.Name.Should().Be("BAR DO JOÃO");
            entity.Owner.Should().Be("JOÃO MACEDO");
            entity.Should().NotBeNull();
        }

        [Fact]
        public void Constructor_NameIsInvalid_ShouldThrowValidationException()
        {
            Action createEntity = () => new Store("", "BAR DO JOÃO");

            createEntity.Should().Throw<ValidationException>()
                .WithMessage(SharedConstants.ErrorMessages.StoreNameIsInvalid);
        }

        [Fact]
        public void Constructor_OwnerIsInvalid_ShouldThrowValidationException()
        {
            Action createEntity = () => new Store("JOÃO MACEDO", "");

            createEntity.Should().Throw<ValidationException>()
                .WithMessage(SharedConstants.ErrorMessages.StoreOwnerIsInvalid);
        }
    }
}
