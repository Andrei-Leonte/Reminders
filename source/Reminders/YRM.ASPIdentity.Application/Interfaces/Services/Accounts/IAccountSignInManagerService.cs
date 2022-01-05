using System.Security.Claims;
using YRM.ASPIdentity.Application.Entities.JWTTokens.Webs;
using YRM.Domain.Entities.Identity;

namespace YRM.ASPIdentity.Application.Interfaces.Services.Accounts
{
    public interface IAccountSignInManagerService
    {
        Task<ApplicationUser> VerifyEmailAsync(string email);
        Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password);
        Task<AuthorizeTokenDetails> GenerateAuthorizeTokenDetailsAsync(ApplicationUser applicationUser);
    }
}
