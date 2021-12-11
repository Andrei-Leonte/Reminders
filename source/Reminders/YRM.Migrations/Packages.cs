using Microsoft.Extensions.DependencyInjection;
using YRM.Migrations.Contexts.IdentityServers;
using YRM.Migrations.Contexts.Reminders;
using YRM.Migrations.Interfaces.Contexts.IndentityServers;
using YRM.Migrations.Interfaces.Contexts.Reminders;
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
            serviceCollection.AddScoped<IReminderMigrationDbContext, ReminderMigrationDbContext>();

            serviceCollection.AddScoped<IUserManagerService, UserManagerService>();
        }
    }
}
