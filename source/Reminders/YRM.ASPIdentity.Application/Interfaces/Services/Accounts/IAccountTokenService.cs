﻿using YRM.ASPIdentity.Application.Entities.JWTTokens.Webs;

namespace YRM.ASPIdentity.Application.Interfaces.Services.Accounts
{
    public interface IAccountTokenService
    {
        string WebAuthorizeGenerate(AuthorizeTokenDetails generateWebTokenEntity);
    }
}
