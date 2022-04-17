namespace YRM.Reminders.Domain.Entities.Base.Interfaces
{
    internal interface ICreatedAwareness<T>
        where T : struct
    {
        public T Id { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
