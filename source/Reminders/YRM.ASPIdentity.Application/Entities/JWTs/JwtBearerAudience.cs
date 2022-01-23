namespace YRM.ASPIdentity.Application.Entities.JWTs
{
    public class JwtBearerAudience
    {
        public string AudienceName { get; set; }
        public string ValidAudience { get; set; }
        public string IssuerKey { get; set; }

        public string GetValidAudience()
        {
            if (string.IsNullOrEmpty(AudienceName))
            {
                throw new InvalidOperationException("ValidAudience is null");
            }

            return AudienceName;
        }

        public string GetIssuerKey()
        {
            if (string.IsNullOrEmpty(IssuerKey))
            {
                throw new InvalidOperationException("IssuerKey is null");
            }

            return IssuerKey;
        }
    }
}
