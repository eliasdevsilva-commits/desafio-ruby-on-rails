using BasePoint.Core.Domain.Entities;

namespace ByCodersChallenge.Core.Domain.Entities
{
    public class Store(string name, string owner) : BaseEntity
    {
        public Store() : this(string.Empty, string.Empty)
        {
        }
        public virtual string Name { get; protected set; } = name;
        public virtual string Owner { get; protected set; } = owner;
    }
}