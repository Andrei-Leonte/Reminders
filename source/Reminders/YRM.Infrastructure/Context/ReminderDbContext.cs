using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.Domain.Configurations;
using YRM.Domain.Entities.Identity;

namespace YRM.Infrastructure
{
    public class ReminderDbContext : IdentityDbContext<ApplicationUser>
    {
        public ReminderDbContext(DbContextOptions<ReminderDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReminderConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
