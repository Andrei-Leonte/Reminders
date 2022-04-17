namespace YRM.Reminders.Domain.Entities.Base.Interfaces
{
    internal interface IModifiedAwareness<T> : ICreatedAwareness<T>
        where T : struct
    {
        public DateTime ModifiedAtUtc { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
