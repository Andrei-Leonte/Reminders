using System.Security.Claims;
using YRM.Domain.Entities.Identity;

namespace YRM.ASPIdentity.Application.Entities.JWTTokens.Webs
{
    public class AuthorizeTokenDetails
    {
        public AuthorizeTokenDetails(
            ApplicationUser applicationUser,
            IEnumerable<Claim> claims,
            TimeSpan validateFromTimeSpan,
            TimeSpan expireAtTimeSpan)
        {
            ApplicationUser = applicationUser;
            Claims = claims;
            ValidateFromTimeSpan = validateFromTimeSpan;
            ExpireAtTimeSpan = expireAtTimeSpan;
        }

        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
        public TimeSpan ValidateFromTimeSpan { get; set; }
        public TimeSpan ExpireAtTimeSpan { get; set; }
    }
}
