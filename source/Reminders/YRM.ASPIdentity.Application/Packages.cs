using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using YRM.ASPIdentity.Application.Interfaces.Managers;
using YRM.ASPIdentity.Application.Interfaces.Services.Accounts;
using YRM.ASPIdentity.Application.Managers;
using YRM.ASPIdentity.Application.Services.Accounts;

namespace YRM.ASPIdentity.Application
{
    public static class Packages
    {
        public static void RegistersApplicationPackages(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IAccountTokenService, AccountTokenService>();
            serviceCollection.AddScoped<IAccountSignInManagerService, AccountSignInManagerService>();
           
            serviceCollection.AddScoped<IAccountManager, AccountManager>();
        }
    }
}
