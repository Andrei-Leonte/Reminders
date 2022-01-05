using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YRM.Domain.Configurations;
using YRM.Domain.Entities.Identity;

namespace YRM.Infrastructure.Contexts
{
    public class AspIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public AspIdentityDbContext(DbContextOptions<AspIdentityDbContext> options)
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
