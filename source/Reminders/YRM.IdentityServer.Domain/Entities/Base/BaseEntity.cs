namespace YRM.IdentityServer.Domain.Entities.Base
{
    public abstract record BaseEntity<T>
        where T : struct
    {
        public T Id { get; set; }
    }
}
