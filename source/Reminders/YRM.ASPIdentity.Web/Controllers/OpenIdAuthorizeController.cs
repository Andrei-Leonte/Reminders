using Microsoft.AspNetCore.Mvc;
using System.Net;
using YRM.ASPIdentity.Application.Dtos.OpenIdConnect;

namespace YRM.ASPIdentity.Web.Controllers
{
    [ApiController]
    public class OpenIdAuthorizeController : ControllerBase
    {
        [HttpGet("connect/authorize")]
        public IActionResult ConnectAuthorizeAsync([FromForm] AuthenticationRequestDto body, [FromQuery] AuthenticationRequestDto body1)
        {
            return StatusCode((int)HttpStatusCode.Found);
        }

        [HttpGet("authorize")]
        public IActionResult AuthorizeAsync([FromForm] AuthenticationRequestDto body, [FromQuery] AuthenticationRequestDto body1)
        {
            return StatusCode((int)HttpStatusCode.Found);
        }
    }
}
