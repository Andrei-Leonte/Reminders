using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using YRM.Migrations.Interfaces.Contexts.AspIdentity;

namespace YRM.Migrations.Functions
{
    internal class AspIdentityServerMigrationFunction
    {
        private readonly IAspIdentityMigrationDbContext aspIdentityMigrationDbContext;

        private readonly ILogger logger;

        public AspIdentityServerMigrationFunction(
            IAspIdentityMigrationDbContext aspIdentityMigrationDbContext,
            ILoggerFactory loggerFactory)
            => (this.aspIdentityMigrationDbContext, logger)
                = (aspIdentityMigrationDbContext,
                    loggerFactory.CreateLogger<IdentityServerMigrationFunction>());

        [Function("asp/identity/migrate")]
        public async Task IdentityServerMigrateAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await aspIdentityMigrationDbContext.MigrateAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }

        [Function("asp/identity/seeds")]
        public async Task AddIdentityServerSeedsAsync(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
        {
            try
            {
                await aspIdentityMigrationDbContext.MigrateDefaultAspUserAsync();
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
            }
        }
    }
}
