using YRM.ASPIdentity.Application.Dtos.Signin;
using YRM.ASPIdentity.Application.Interfaces.Managers;
using YRM.ASPIdentity.Application.Interfaces.Services.Accounts;

namespace YRM.ASPIdentity.Application.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly IAccountSignInManagerService accountSignInManagerService;
        private readonly IAccountTokenService accountTokenService;

        public AccountManager(
            IAccountSignInManagerService accountSignInManagerService,
            IAccountTokenService accountTokenService)
        {
            this.accountSignInManagerService = accountSignInManagerService;
            this.accountTokenService = accountTokenService;
        }

        public async Task<WebSigninResponseDto> SigninAsync(WebSigninRequestDto requestDto)
        {
            var user = await accountSignInManagerService.VerifyEmailAsync(requestDto.Email);

            var requireTwoFactorAuth = await accountSignInManagerService
                .VerifyPasswordAsync(user, requestDto.Password);

            if (requireTwoFactorAuth)
            {
                return new WebSigninResponseDto(requireTwoFactorAuth);
            }

            var authorizeTokenDetails = await accountSignInManagerService.GenerateAuthorizeTokenDetailsAsync(user);

            var token = accountTokenService.WebAuthorizeGenerate(authorizeTokenDetails);

            return new WebSigninResponseDto(token, requireTwoFactorAuth);
        }
    }
}
