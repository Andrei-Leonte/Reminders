using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;

namespace YRM.Migrations.Contexts.IdentityServers
{
    public class ReminderPersistedGrantDbContext : PersistedGrantDbContext, IReminderPersistedGrantDbContext
    {
        public ReminderPersistedGrantDbContext(
            DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }
    }
}