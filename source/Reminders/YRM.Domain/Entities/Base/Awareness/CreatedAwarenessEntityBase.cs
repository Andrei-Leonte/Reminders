﻿using YRM.Domain.Entities.Base.Interfaces;

namespace YRM.Domain.Entities.Base.Awareness
{
    internal abstract record CreatedAwarenessEntityBase<T> : BaseEntity<T>, ICreatedAwareness<T>
        where T : struct
    {
        public DateTime CreatedAtUtc { get; set; }
        public Guid CreatedBy { get; set; }
    }
}
