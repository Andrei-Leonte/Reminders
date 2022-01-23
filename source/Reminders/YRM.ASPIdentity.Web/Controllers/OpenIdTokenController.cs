using Microsoft.AspNetCore.Mvc;

namespace YRM.ASPIdentity.Web.Controllers
{
    [ApiController]
    public class OpenIdTokenController : ControllerBase
    {
        [HttpPost("token")]
        public IActionResult GetToken()
        {
            return Ok();
        }
    }
}
