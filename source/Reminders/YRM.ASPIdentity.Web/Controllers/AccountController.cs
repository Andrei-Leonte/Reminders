using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YRM.ASPIdentity.Application.Dtos.Signin;
using YRM.ASPIdentity.Application.Interfaces.Managers;

namespace YRM.ASPIdentity.Web.Controllers
{
    [ApiController, Route("api/[controller]"), Produces("application/json")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager accountManager;

        public AccountController(IAccountManager accountManager)
            => this.accountManager = accountManager;
         
        [HttpPost("signin"), AllowAnonymous]
        public async Task<IActionResult> SignInAsync(WebSigninRequestDto requestDto)
        {
            var responseDto = await accountManager.SigninAsync(requestDto);

            return Ok(responseDto);
        }

        [HttpPost, Route("authorize"), Authorize]
        public IActionResult AuthorizeAsync() =>
            Ok("You are authorize!");
    }
}
