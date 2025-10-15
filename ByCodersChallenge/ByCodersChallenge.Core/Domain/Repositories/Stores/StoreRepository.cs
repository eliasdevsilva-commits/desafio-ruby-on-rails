using BasePoint.Core.Domain.Repositories;
using ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores;
using ByCodersChallenge.Core.Domain.Entities;
using ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores;

namespace ByCodersChallenge.Core.Domain.Repositories.Stores
{
    public class StoreRepository : Repository<Store>, IStoreRepository
    {
        private readonly IStoreCqrsCommandProvider _storeCqrsCommandProvider;

        public StoreRepository(IStoreCqrsCommandProvider commandProvider) : base(commandProvider)
        {
            _storeCqrsCommandProvider = commandProvider;
        }
        public async Task<Store> GetStoreByName(string name)
        {
            return HandleAfterGetFromCommandProvider(await _storeCqrsCommandProvider.GetStoreByName(name));
        }
    }
}