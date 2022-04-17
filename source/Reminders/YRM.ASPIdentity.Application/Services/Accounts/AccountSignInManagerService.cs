using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using YRM.ASPIdentity.Application.Entities.JWTTokens.Webs;
using YRM.ASPIdentity.Application.Interfaces.Services.Accounts;
using YRM.ASPIdentity.Application.Misc.Exceptions;
using YRM.IdentityServer.Domain.Entities.Identity;

namespace YRM.ASPIdentity.Application.Services.Accounts
{
    internal class AccountSignInManagerService : IAccountSignInManagerService
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountSignInManagerService(
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
            => (this.signInManager, this.userManager) = (signInManager, userManager);

        public async Task<ApplicationUser> VerifyEmailAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email);

            if (user is null)
            {
                throw new AccountException("User not found.");
            }

            return user;
        }

        public async Task<bool> VerifyPasswordAsync(ApplicationUser applicationUser, string password)
        {
            var result = await signInManager.PasswordSignInAsync(
               applicationUser, password, true, true);

            if (result.Succeeded)
            {
                return false;
            }

            if (result.RequiresTwoFactor)
            {
                return true;
            }

            if (result.IsLockedOut)
            {
                throw new AccountException("User is locked!");
            }

            if (result.IsNotAllowed)
            {
                throw new AccountException("User is not allowed to sign in!");
            }

            throw new AccountException($"Unknown result '{result}'");
        }

        public async Task<AuthorizeTokenDetails> GenerateAuthorizeTokenDetailsAsync(ApplicationUser applicationUser)
        {
            var claims = await userManager.GetClaimsAsync(applicationUser);

            claims.Add(new Claim("Username", applicationUser.UserName));
            claims.Add(new Claim("Email", applicationUser.Email));
            claims.Add(new Claim("EmailConfirmed", applicationUser.EmailConfirmed.ToString()));

            return new AuthorizeTokenDetails(
                applicationUser, claims, TimeSpan.FromDays(0), TimeSpan.FromDays(1));
        }
    }
}
