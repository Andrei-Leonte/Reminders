using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using YRM.Infrastructure;

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

                    var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
                    services
                        .AddDbContext<ReminderDbContext>(options =>
                            options.UseSqlServer(
                                configuration.GetSection("ReminderDBConnectionString").Value));
                })
                .Build();

            host.Run();
        }
    }
}