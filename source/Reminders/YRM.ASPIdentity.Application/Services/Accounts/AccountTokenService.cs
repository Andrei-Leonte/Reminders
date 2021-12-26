using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using YRM.ASPIdentity.Application.Const;
using YRM.ASPIdentity.Application.Entities.JWTs;
using YRM.Common.Interfaces.Utils;
using YRM.Domain.Entities.Identity;

namespace YRM.ASPIdentity.Application.Services.Accounts
{
    public class AccountTokenService
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

        public string WebGenerate(
            ApplicationUser applicationUser, IEnumerable<Claim> claims, TimeSpan validateFromTimeSpan, TimeSpan expireAtTimeSpan)
        {
            var utcNow = dateTimeUtil.GetUtcNow();

            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(webJwtBearerAudience.GetIssuerKey()));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer, issuer, claims, utcNow.Add(validateFromTimeSpan), utcNow.Add(expireAtTimeSpan), credentials);

            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
