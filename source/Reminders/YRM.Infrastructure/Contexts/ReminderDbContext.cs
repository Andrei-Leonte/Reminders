using Microsoft.EntityFrameworkCore;

namespace YRM.Infrastructure.Contexts
{
    internal class ReminderDbContext : DbContext
    {
        public ReminderDbContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
