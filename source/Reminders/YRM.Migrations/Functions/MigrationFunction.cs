using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;
using YRM.Migrations.Interfaces.Contexts.Reminders;

namespace YRM.Migrations
{
    internal class MigrationFunction
    {
        private readonly ILogger logger;

        private readonly IReminderConfigurationDbContext reminderConfigurationDbContext;
        private readonly IReminderPersistedGrantDbContext reminderPersistedGrantDbContext;
        private readonly IReminderMigrationDbContext reminderMigrationDbContext;

        public MigrationFunction(
            IReminderConfigurationDbContext reminderConfigurationDbContext,
            IReminderPersistedGrantDbContext reminderPersistedGrantDbContext,
            IReminderMigrationDbContext reminderMigrationDbContext,
            ILoggerFactory loggerFactory)
        {
            this.reminderConfigurationDbContext = reminderConfigurationDbContext;
            this.reminderPersistedGrantDbContext = reminderPersistedGrantDbContext;
            this.reminderMigrationDbContext = reminderMigrationDbContext;

            logger = loggerFactory.CreateLogger<MigrationFunction>();
        }

        [Function("Migrate")]
        public async Task MigrateAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await reminderConfigurationDbContext.MigrateAsync();
                await reminderPersistedGrantDbContext.MigrateAsync();
                await reminderMigrationDbContext.MigrateAsync();
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
            }
        }


        [Function("Seeds")]
        public async Task AddSeedsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await reminderConfigurationDbContext.MigrateDefaultClientValuesAsync();
                await reminderConfigurationDbContext.MigrateDefaultApiScopes();
                await reminderConfigurationDbContext.MigrateDefaultIdentityResources();

                await reminderMigrationDbContext.MigrateDefaultAspUserAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

    }
}
