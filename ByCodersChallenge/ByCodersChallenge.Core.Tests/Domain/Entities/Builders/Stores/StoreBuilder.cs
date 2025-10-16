using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Tests.Domain.Entities.Builders.Stores
{
    public class StoreBuilder
    {
        private string _name;
        private string _owner;

        public StoreBuilder()
        {
            _name = "BAR DO JOÃO";
            _owner = "JOÃO MACEDO";
        }

        public StoreBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public StoreBuilder WithOwner(string owner)
        {
            _owner = owner;
            return this;
        }

        public Store Build()
        {
            return new Store(_name, _owner);
        }
    }
}
