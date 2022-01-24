using YRM.ASPIdentity.Application.Entities.JWTTokens.Webs;

namespace YRM.ASPIdentity.Application.Interfaces.Services.Accounts
{
    internal interface IAccountTokenService
    {
        string WebAuthorizeGenerate(AuthorizeTokenDetails generateWebTokenEntity);
    }
}
