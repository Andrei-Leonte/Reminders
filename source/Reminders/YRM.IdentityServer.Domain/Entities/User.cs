using YRM.IdentityServer.Domain.Entities.Base.Awareness;
using YRM.IdentityServer.Domain.Misc.Enums;

namespace YRM.IdentityServer.Domain.Entities
{
    internal abstract record User : UserAwarenessEntityBase<Guid>
    {
        public string? UserId { get; set; }
        public UserType Type { get; set; }
    }
}
