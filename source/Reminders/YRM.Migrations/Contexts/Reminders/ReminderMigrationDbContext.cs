using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.Domain.Entities.Identity;

namespace YRM.Migrations
{
    public class ReminderMigrationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ReminderMigrationDbContext(DbContextOptions<ReminderMigrationDbContext> options) : base(options)
        {
        }
    }
}
