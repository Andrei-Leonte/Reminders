using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using YRM.Infrastructure.Contexts;
using YRM.Migrations.Interfaces.Contexts.Reminders;

namespace YRM.Migrations.Contexts.Reminders
{
    internal class ReminderMigrationDbContext : ReminderDbContext, IReminderMigrationDbContext
    {
        public ReminderMigrationDbContext(DbContextOptions<ReminderDbContext> options) : base(options)
        {
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }
    }
}
