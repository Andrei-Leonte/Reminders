namespace YRM.ASPIdentity.Application.Dtos.Signin
{
    public class WebSigninRequestDto
    {
        public WebSigninRequestDto(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
