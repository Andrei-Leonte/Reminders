using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YRM.IdentityServer.Domain.Entities.Identity;
using YRM.IdentityServer.Infrastructure.Contexts;
using YRM.Migrations.Contexts.AspIdentity;
using YRM.Migrations.Contexts.IdentityServers;

namespace YRM.Migrations
{
    public class Program
    {
        public static void Main()
        {
            IConfiguration configuration = null;

            var host = new HostBuilder()
                .ConfigureAppConfiguration(app => configuration = 
                    app
                        .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                        .Build())
                .ConfigureFunctionsWorkerDefaults(builder => {

                    var sqlConnectionString = configuration.GetConnectionString("ReminderDBConnectionString");

                    builder.Services.AddDbContext<ReminderConfigurationDbContext>(options =>
                        options.UseSqlServer(sqlConnectionString));

                    builder.Services.AddDbContext<ReminderPersistedGrantDbContext>(options =>
                        options.UseSqlServer(sqlConnectionString));

                    builder.Services.AddDbContext<AspIdentityMigrationDbContext>(options =>
                        options.UseSqlServer(sqlConnectionString));

                    builder.Services.AddDbContext<AspIdentityDbContext>(options =>
                        options.UseSqlServer(sqlConnectionString));

                    //builder.Services.AddIdentity
                    builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                        .AddEntityFrameworkStores<AspIdentityDbContext>();

                    builder.Services.AddIdentityServer(options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.EmitStaticAudienceClaim = true;
                    }).AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString);
                    }).AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b => b.UseSqlServer(sqlConnectionString);

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                    });

                    builder.Services.RegisterYRMMigrationsPackages();
                })
                .Build();

            host.Run();
        }
    }
}