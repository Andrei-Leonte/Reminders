using YRM.Domain.Entities.Base.Awareness;
using YRM.Domain.Misc.Enums;

namespace YRM.Domain.Entities
{
    internal abstract record User : UserAwarenessEntityBase<Guid>
    {
        public string? UserId { get; set; }
        public UserType Type { get; set; }
    }
}
