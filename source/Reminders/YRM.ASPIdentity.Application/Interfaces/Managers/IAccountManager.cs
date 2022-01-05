using YRM.ASPIdentity.Application.Dtos.Signin;

namespace YRM.ASPIdentity.Application.Interfaces.Managers
{
    public interface IAccountManager
    {
        Task<WebSigninResponseDto> SigninAsync(WebSigninRequestDto requestDto);
    }
}
