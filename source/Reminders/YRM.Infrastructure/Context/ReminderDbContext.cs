using Microsoft.EntityFrameworkCore;
using YRM.Domain.Configurations;
using YRM.Infrastructure.Context.Base;

namespace YRM.Infrastructure
{
    public class ReminderDbContext : BaseReminderDbContext
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
