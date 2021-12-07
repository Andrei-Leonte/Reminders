using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using YRM.Migrations.Contexts.IdentityServer;

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
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
            this.reminderConfigurationDbContext = reminderConfigurationDbContext;
            this.reminderPersistedGrantDbContext = reminderPersistedGrantDbContext;
            logger = loggerFactory.CreateLogger<MigrationFunction>();

            var connectionString = configuration.GetValue<string>("ReminderDBConnectionString");

            reminderMigrationDbContext = new ReminderMigrationDbContext();
            reminderMigrationDbContext.Database.SetConnectionString(connectionString);
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

                logger.LogDebug($"{migrations.Count()} applied.");
            }
            catch(Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
