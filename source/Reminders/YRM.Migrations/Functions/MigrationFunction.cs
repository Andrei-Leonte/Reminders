using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace YRM.Migrations
{
    public class MigrationFunction
    {
        private readonly ILogger logger;
        private readonly ReminderMigrationDbContext reminderMigrationDbContext;

        public MigrationFunction(
            ILoggerFactory loggerFactory,
            IConfiguration configuration)
        {
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
