using BasePoint.Core.Domain.Cqrs.CommandProviders;
using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Domain.Cqrs.CommandProviders.Stores
{
    public interface IStoreCqrsCommandProvider : ICqrsCommandProvider<Store>
    {
        Task<Store> GetStoreByName(string name);
    }
}