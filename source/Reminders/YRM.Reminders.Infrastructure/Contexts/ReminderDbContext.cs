using Microsoft.EntityFrameworkCore;

namespace YRM.Reminders.Infrastructure.Contexts
{
    internal class ReminderDbContext : DbContext
    {
        public ReminderDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
