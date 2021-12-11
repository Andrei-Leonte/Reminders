using System.Threading.Tasks;

namespace YRM.Migrations.Interfaces.Contexts.Reminders
{
    internal interface IReminderMigrationDbContext
    {
        Task MigrateAsync();
        Task MigrateDefaultAspUserAsync();
    }
}
