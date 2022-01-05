using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace YRM.UserManager.Function
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services
                        .AddAuthentication("Bearer")
                        .AddJwtBearer("Bearer", options =>
                        {
                            options.Authority = "https://localhost:7245";
                            options.Audience = "https://localhost:5502";

                            //options.TokenValidationParameters = new TokenValidationParameters
                            //{
                            //    ValidateAudience = true,
                            //    ValidateIssuer = true
                            //};
                        });
                })
                .Build();

            host.Run();
        }
    }
}