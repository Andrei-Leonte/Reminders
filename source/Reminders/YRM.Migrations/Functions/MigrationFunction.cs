using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using YRM.Migrations.Contexts.IdentityServer;
using YRM.Migrations.Contexts.IdentityServer.Reminders;

namespace YRM.Migrations
{
    public class MigrationFunction
    {
        private readonly ILogger logger;

        private readonly ReminderConfigurationDbContext reminderConfigurationDbContext;
        private readonly ReminderPersistedGrantDbContext reminderPersistedGrantDbContext;
        private readonly ReminderMigrationDbContext reminderMigrationDbContext;

        public MigrationFunction(
            ReminderConfigurationDbContext reminderConfigurationDbContext,
            ReminderPersistedGrantDbContext reminderPersistedGrantDbContext,
            ReminderMigrationDbContext reminderMigrationDbContext,
            ILoggerFactory loggerFactory)
        {
            this.reminderConfigurationDbContext = reminderConfigurationDbContext;
            this.reminderPersistedGrantDbContext = reminderPersistedGrantDbContext;
            this.reminderMigrationDbContext = reminderMigrationDbContext;

            logger = loggerFactory.CreateLogger<MigrationFunction>();
        }

        [Function("Migrate")]
        public async Task Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await reminderConfigurationDbContext.Database.MigrateAsync();
                await reminderPersistedGrantDbContext.Database.MigrateAsync();
                await reminderMigrationDbContext.Database.MigrateAsync();

                var migrations = reminderMigrationDbContext.Database.GetAppliedMigrations();

                logger.LogDebug($"ReminderConfigurationDbContext {migrations.Count()} applied.");
                logger.LogDebug($"ReminderPersistedGrantDbContext {migrations.Count()} applied.");
                logger.LogDebug($"ReminderMigrationDbContext {migrations.Count()} applied.");
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
