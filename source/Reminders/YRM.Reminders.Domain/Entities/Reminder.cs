using System.Diagnostics.CodeAnalysis;
using YRM.Reminders.Domain.Entities.Base.Awareness;

namespace YRM.Reminders.Domain.Entities
{
    internal record Reminder: ModifiedAwarenessEntityBase<Guid>
    {
        [MaybeNull]
        public string Title { get; set; }

        [MaybeNull]
        public string Description { get; set; }
    }
}
