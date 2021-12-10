using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YRM.Domain.Entities.Identity;
using YRM.Migrations.Contexts.IdentityServer;
using YRM.Migrations.Contexts.IdentityServer.Reminders;

namespace YRM.Migrations
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddDbContext<ReminderConfigurationDbContext>(options =>
                        options.UseSqlServer(@"Server=.;Database=ReminderDB;Trusted_Connection=True;"));

                    services.AddDbContext<ReminderPersistedGrantDbContext>(options =>
                        options.UseSqlServer(@"Server=.;Database=ReminderDB;Trusted_Connection=True;"));

                    services.AddDbContext<ReminderMigrationDbContext>(options =>
                        options.UseSqlServer(@"Server=.;Database=ReminderDB;Trusted_Connection=True;"));

                    //services.AddIdentity<ApplicationUser, IdentityRole>()
                    //    .AddEntityFrameworkStores<ReminderPersistedGrantDbContext>()
                    //    .AddDefaultTokenProviders();

                    services.AddIdentityServer(options =>
                    {
                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;
                        options.EmitStaticAudienceClaim = true;
                    }).AddConfigurationStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                            b.UseSqlServer(
                                @"Server=.;Database=ReminderDB;Trusted_Connection=True;");
                    }).AddOperationalStore(options =>
                    {
                        options.ConfigureDbContext = b =>
                           b.UseSqlServer(
                                @"Server=.;Database=ReminderDB;Trusted_Connection=True;");

                        // this enables automatic token cleanup. this is optional.
                        options.EnableTokenCleanup = true;
                    });
                })
                .Build();

            host.Run();
        }
    }
}