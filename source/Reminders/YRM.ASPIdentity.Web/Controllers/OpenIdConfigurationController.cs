using Microsoft.AspNetCore.Mvc;

namespace YRM.ASPIdentity.Web.Controllers
{
    [Route("/")]
    [ApiController]
    public class OpenIdConfigurationController : ControllerBase
    {

        [HttpGet(".well-known/openid-configuration")]
        public async Task<IActionResult> GetWellKnownOpenIdConfigurationAsync()
        {
            var json = new
            {
                issuer = "https://localhost:7245/",
                authorization_endpoint = "https://localhost:7245/connect/authorize",
                scopes_supported = new[] {"openid", "reminderClientScope", "reminderAdminScope", "offline_access" },
                claims_supported = "sub",
                response_types_supported = new [] {"code", "token"}
            };

            return Ok(json);
        }
    }
}
