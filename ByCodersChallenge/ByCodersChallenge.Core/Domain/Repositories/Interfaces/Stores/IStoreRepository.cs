using BasePoint.Core.Domain.Repositories.Interfaces;
using ByCodersChallenge.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Domain.Repositories.Interfaces.Stores
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<Store> GetStoreByName(string name);
    }
}