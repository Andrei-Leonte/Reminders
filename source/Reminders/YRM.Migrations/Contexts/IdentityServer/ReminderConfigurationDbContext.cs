using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;

namespace YRM.Migrations.Contexts.IdentityServer
{
    public class ReminderConfigurationDbContext : ConfigurationDbContext
    {
        public ReminderConfigurationDbContext(
            DbContextOptions<ConfigurationDbContext> options, ConfigurationStoreOptions storeOptions)
                : base(options, storeOptions)
        {
        }
    }
}
