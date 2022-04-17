using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using YRM.IdentityServer.Domain.Entities.Identity;
using YRM.Migrations.Interfaces.Services;

namespace YRM.Migrations.Repositories
{
    internal class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task CreateUserAsync(ApplicationUser applicationUser, string password)
        {
            var result = await userManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Not able to create user!");
            }
        }
    }
}
