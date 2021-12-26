using Microsoft.Extensions.DependencyInjection;
using YRM.Common.Interfaces.Utils;
using YRM.Common.Utils;

namespace YRM.Common
{
    public static class Packages
    {
        public static void RegisterCommonPackages(this IServiceCollection services)
        {
            services.AddScoped<IDateTimeUtil, DateTimeUtil>();
        }
    }
}
