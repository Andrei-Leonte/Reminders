using YRM.Domain.Entities.Base.Awareness;

namespace YRM.Domain.Entities
{
#pragma warning disable CS8618
    internal record Reminder: ModifiedAwarenessEntityBase<Guid>
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
