using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace YRM.Migrations.Contexts.IdentityServer.Reminders
{
    public class ReminderPersistedGrantDbContext : PersistedGrantDbContext
    {
        public ReminderPersistedGrantDbContext(
            DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }
    }
}
