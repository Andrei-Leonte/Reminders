using System.Threading.Tasks;
using YRM.IdentityServer.Domain.Entities.Identity;

namespace YRM.Migrations.Interfaces.Services
{
    interface IUserManagerService
    {
        Task CreateUserAsync(ApplicationUser applicationUser, string password);
    }
}
