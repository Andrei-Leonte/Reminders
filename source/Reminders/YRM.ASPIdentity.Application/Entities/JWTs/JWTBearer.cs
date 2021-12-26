using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace YRM.ASPIdentity.Application.Entities.JWTs
{
    public class JWTBearer
    {
        public JWTBearer()
        {
            Audiences = new List<JwtBearerAudience>();
        }

        public string? Issuer { get; set; }
        public IEnumerable<JwtBearerAudience> Audiences { get; set; }

        public string GetIssuer()
        {
            if (Issuer is null)
            {
                throw new ArgumentNullException("Issuer is null");
            }

            return Issuer;
        }

        public IEnumerable<string> GetValidAudiences()
        {
            return Audiences.Select(audience =>
            {
                if (string.IsNullOrEmpty(audience.ValidAudience))
                {
                    throw new ArgumentException("One or validAudience values are null.");
                }

                return audience.ValidAudience;
            });
        }

        public IEnumerable<SecurityKey> GetSecurityKeys()
        {
            return Audiences.Select(audience =>
            {
                if (string.IsNullOrEmpty(audience.IssuerKey))
                {
                    throw new ArgumentException("One or issuerKey values are null.");
                }

                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(audience.IssuerKey));
            });
        }

        public JwtBearerAudience GetAudienceByName(string name)
        {
            return Audiences
                .First(audience => audience.GetValidAudience().Equals(name));
        }
    }
}
