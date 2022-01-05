namespace YRM.ASPIdentity.Application.Dtos.Signin
{
    public class WebSigninResponseDto
    {
        public WebSigninResponseDto(bool isTwoFactorAuthentication)
            => (AuthenticateTokenValue, AuthenticateRefreshTokenValue, IsTwoFactorAuthentication)
                = (string.Empty, string.Empty, isTwoFactorAuthentication);

        public WebSigninResponseDto(string authenticateTokenValue, bool isTwoFactorAuthentication)
             => (AuthenticateTokenValue, AuthenticateRefreshTokenValue, IsTwoFactorAuthentication)
                = (authenticateTokenValue, string.Empty, isTwoFactorAuthentication);

        public string AuthenticateTokenValue { get; set; }
        public string AuthenticateRefreshTokenValue { get; set; }
        public bool IsTwoFactorAuthentication { get; set; }
    }
}
