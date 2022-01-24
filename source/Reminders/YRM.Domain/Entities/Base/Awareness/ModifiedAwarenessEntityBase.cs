using YRM.Domain.Entities.Base.Interfaces;

namespace YRM.Domain.Entities.Base.Awareness
{
    internal abstract record ModifiedAwarenessEntityBase<T> :
        CreatedAwarenessEntityBase<T>, IModifiedAwareness<T>
            where T : struct
    {
        public DateTime ModifiedAtUtc { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
