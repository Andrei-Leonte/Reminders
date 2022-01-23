namespace YRM.ASPIdentity.Application.Dtos.OpenIdConnect
{
    public class AuthenticationRequestDto
    {
        public string scope { get; set; }
        public string response_type { get; set; }
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        public string state { get; set; }
    }
}
