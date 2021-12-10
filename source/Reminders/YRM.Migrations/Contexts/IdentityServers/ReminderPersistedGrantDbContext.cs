using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;

namespace YRM.Migrations.Contexts.IdentityServers
{
    internal class ReminderPersistedGrantDbContext : PersistedGrantDbContext, IReminderPersistedGrantDbContext
    {
        private readonly string clients;

        public ReminderPersistedGrantDbContext(
            IConfiguration configuration,
            DbContextOptions<PersistedGrantDbContext> options, OperationalStoreOptions storeOptions)
            : base(options, storeOptions)
        {
            configuration.GetValue<string>
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

        public async Task AddStartResourcesAsync()
        {

        }
    }
}