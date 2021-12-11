using System.Threading.Tasks;
using YRM.Domain.Entities.Identity;

namespace YRM.Migrations.Interfaces.Services
{
    interface IUserManagerService
    {
        Task CreateUserAsync(ApplicationUser applicationUser, string password);
    }
}
