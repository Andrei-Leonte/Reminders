namespace YRM.ASPIdentity.Application.Entities.JWTs
{
    public class JwtBearerAudience
    {
        public string ValidAudience { get; set; }
        public string IssuerKey { get; set; }

        public string GetValidAudience()
        {
            if (string.IsNullOrEmpty(ValidAudience))
            {
                throw new InvalidOperationException("ValidAudience is null");
            }

            return ValidAudience;
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
