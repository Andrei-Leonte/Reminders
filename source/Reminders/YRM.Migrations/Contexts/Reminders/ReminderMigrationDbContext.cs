using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using YRM.Domain.Configurations;
using YRM.Domain.Entities.Identity;
using YRM.Infrastructure.Contexts;
using YRM.Migrations.Entities;
using YRM.Migrations.Interfaces.Contexts.Reminders;
using YRM.Migrations.Interfaces.Services;

namespace YRM.Migrations.Contexts.Reminders
{
    internal class ReminderMigrationDbContext : ReminderDbContext, IReminderMigrationDbContext
    {
        private readonly IUserManagerService userManagerService;
        private readonly IConfiguration configuration;

        public ReminderMigrationDbContext(
            IUserManagerService userManagerService,
            IConfiguration configuration,
            DbContextOptions<ReminderDbContext> options)
            : base(options)
        { 
            this.userManagerService = userManagerService;
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ReminderConfiguration());

            base.OnModelCreating(builder);
        }

        public async Task MigrateAsync()
        {
            await Database.MigrateAsync();
        }

        public async Task MigrateDefaultAspUserAsync()
        {
            var user = await Users.ToListAsync();

            if (user.Count is 0)
            {
                var migrationAspUser = new MigrationAspUser();

                configuration.GetSection("DefaultAspUser").Bind(migrationAspUser);

                var aspUser = new ApplicationUser()
                {
                    UserName = migrationAspUser.Username,
                    Email = migrationAspUser.Email
                };

                await userManagerService.CreateUserAsync(aspUser, migrationAspUser.Password);
            }
        }
    }
}
