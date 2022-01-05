using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YRM.ASPIdentity.Application.Const;
using YRM.ASPIdentity.Application.Entities.JWTs;
using YRM.ASPIdentity.Application.Entities.JWTTokens.Webs;
using YRM.ASPIdentity.Application.Interfaces.Services.Accounts;
using YRM.Common.Interfaces.Utils;

namespace YRM.ASPIdentity.Application.Services.Accounts
{
    internal class AccountTokenService : IAccountTokenService
    {
        private readonly IDateTimeUtil dateTimeUtil;

        private readonly string issuer;
        private readonly JwtBearerAudience webJwtBearerAudience;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public AccountTokenService(IDateTimeUtil dateTimeUtil, IConfiguration configuration)
        {
            var jwtBearer = new JWTBearer();
            configuration.GetSection("JWTBearer").Bind(jwtBearer);

            this.dateTimeUtil = dateTimeUtil;

            issuer = jwtBearer.GetIssuer();
            webJwtBearerAudience = jwtBearer.GetAudienceByName(TokenAudienceNameConst.Web);
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public string WebAuthorizeGenerate(
            AuthorizeTokenDetails generateWebTokenEntity)
        {
            var utcNow = dateTimeUtil.GetUtcNow();

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(webJwtBearerAudience.GetIssuerKey()));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //var jwtSecurityToken = new JwtSecurityToken(
            //    issuer,
            //    issuer,
            //    generateWebTokenEntity.Claims,
            //    utcNow.Add(generateWebTokenEntity.ValidateFromTimeSpan),
            //    utcNow.Add(generateWebTokenEntity.ExpireAtTimeSpan),
            //    credentials);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(generateWebTokenEntity.Claims),
                IssuedAt = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = "WebReminder"
            };

            var token = jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            if (token is null)
            {
                throw new ArgumentException("Token is null!");
            }

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
