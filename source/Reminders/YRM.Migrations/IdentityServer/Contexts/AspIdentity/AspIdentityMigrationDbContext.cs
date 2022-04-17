using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using YRM.IdentityServer.Domain.Entities.Identity;
using YRM.IdentityServer.Infrastructure.Contexts;
using YRM.Migrations.Entities;
using YRM.Migrations.Interfaces.Contexts.AspIdentity;
using YRM.Migrations.Interfaces.Services;
using YRM.Reminders.Domain.Configurations;

namespace YRM.Migrations.Contexts.AspIdentity
{
    internal class AspIdentityMigrationDbContext : AspIdentityDbContext, IAspIdentityMigrationDbContext
    {
        private readonly IUserManagerService userManagerService;
        private readonly IConfiguration configuration;

        public AspIdentityMigrationDbContext(
            IUserManagerService userManagerService,
            IConfiguration configuration,
            DbContextOptions<AspIdentityDbContext> options)
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
