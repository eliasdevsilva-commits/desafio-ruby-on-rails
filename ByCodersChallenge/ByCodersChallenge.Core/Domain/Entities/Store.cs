using BasePoint.Core.Domain.Entities;
using BasePoint.Core.Exceptions;
using ByCodersChallenge.Core.Shared;

namespace ByCodersChallenge.Core.Domain.Entities
{
    public class Store : BaseEntity
    {
        protected Store()
        {
        }
        public Store(string name, string owner) : this()
        {
            ValidationException.ThrowIfNullOrEmpty(name, SharedConstants.ErrorMessages.StoreNameIsInvalid);
            ValidationException.ThrowIfNullOrEmpty(owner, SharedConstants.ErrorMessages.StoreOwnerIsInvalid);

            Name = name;
            Owner = owner;
        }
        public virtual string Name { get; protected set; }
        public virtual string Owner { get; protected set; }
    }
}