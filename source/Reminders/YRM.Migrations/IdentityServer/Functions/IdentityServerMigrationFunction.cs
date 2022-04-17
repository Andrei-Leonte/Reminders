using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YRM.Migrations.Interfaces.Contexts.AspIdentity;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;

namespace YRM.Migrations
{
    internal class IdentityServerMigrationFunction
    {
        private readonly IReminderConfigurationDbContext reminderConfigurationDbContext;
        private readonly IReminderPersistedGrantDbContext reminderPersistedGrantDbContext;
        private readonly IAspIdentityMigrationDbContext aspIdentityMigrationDbContext;

        private readonly ILogger logger;

        public IdentityServerMigrationFunction(
            IReminderConfigurationDbContext reminderConfigurationDbContext,
            IReminderPersistedGrantDbContext reminderPersistedGrantDbContext,
            IAspIdentityMigrationDbContext aspIdentityMigrationDbContext,
            ILoggerFactory loggerFactory)
        {
            this.reminderConfigurationDbContext = reminderConfigurationDbContext;
            this.reminderPersistedGrantDbContext = reminderPersistedGrantDbContext;
            this.aspIdentityMigrationDbContext = aspIdentityMigrationDbContext;

            logger = loggerFactory.CreateLogger<IdentityServerMigrationFunction>();
        }

        [Function("identityServer/migrate")]
        public async Task IdentityServerMigrateAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await reminderConfigurationDbContext.MigrateAsync();
                await reminderPersistedGrantDbContext.MigrateAsync();
                await aspIdentityMigrationDbContext.MigrateAsync();
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

        [Function("identityServer/seeds")]
        public async Task AddIdentityServerSeedsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await reminderConfigurationDbContext.MigrateDefaultClientValuesAsync();
                await reminderConfigurationDbContext.MigrateDefaultApiScopes();
                await reminderConfigurationDbContext.MigrateDefaultIdentityResources();

                await aspIdentityMigrationDbContext.MigrateDefaultAspUserAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
