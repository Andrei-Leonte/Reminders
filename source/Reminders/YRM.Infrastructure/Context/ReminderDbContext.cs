using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.Domain.Entities.Identity;

namespace YRM.Infrastructure.Context
{
    public class ReminderDbContext : IdentityDbContext<ApplicationUser>
    {
        public ReminderDbContext(DbContextOptions<ReminderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
