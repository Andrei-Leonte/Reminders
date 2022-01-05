using Microsoft.Extensions.DependencyInjection;
using YRM.Migrations.Contexts.AspIdentity;
using YRM.Migrations.Contexts.IdentityServers;
using YRM.Migrations.Interfaces.Contexts.AspIdentity;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;
using YRM.Migrations.Interfaces.Services;
using YRM.Migrations.Repositories;

namespace YRM.Migrations
{
    internal static class Packages
    {
        public static void RegisterYRMMigrationsPackages(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IReminderConfigurationDbContext, ReminderConfigurationDbContext>();
            serviceCollection.AddScoped<IReminderPersistedGrantDbContext, ReminderPersistedGrantDbContext>();
            serviceCollection.AddScoped<IAspIdentityMigrationDbContext, AspIdentityMigrationDbContext>();

            serviceCollection.AddScoped<IUserManagerService, UserManagerService>();
        }
    }
}
