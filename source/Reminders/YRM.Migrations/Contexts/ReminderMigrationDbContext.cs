using Microsoft.EntityFrameworkCore;
using YRM.Infrastructure.Context.Base;

namespace YRM.Migrations
{
    public class ReminderMigrationDbContext : BaseReminderDbContext
    {
        public ReminderMigrationDbContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=Reminder;Trusted_Connection=True;");
        }
    }
}
