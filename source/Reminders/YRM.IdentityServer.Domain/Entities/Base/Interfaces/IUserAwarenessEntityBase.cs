namespace YRM.IdentityServer.Domain.Entities.Base.Interfaces
{
    internal interface IUserAwarenessEntityBase<T>
        where T : struct
    {
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public Guid ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
