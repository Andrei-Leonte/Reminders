using System.Diagnostics.CodeAnalysis;
using YRM.Domain.Entities.Base.Awareness;

namespace YRM.Domain.Entities
{
    internal record Reminder: ModifiedAwarenessEntityBase<Guid>
    {
        [MaybeNull]
        public string Title { get; set; }

        [MaybeNull]
        public string Description { get; set; }
    }
}
