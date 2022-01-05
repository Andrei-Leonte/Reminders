using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;

namespace YRM.UserManager.Function
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

        [AllowAnonymous, Function("HomeFunctionText")]
        public HttpResponseData GetHomeText(
            [HttpTrigger("get", Route = "text")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");
            var payload = new { Message = "Hello from cloud", Type = "Text" };
            response.WriteString(JsonConvert.SerializeObject(payload));

            return response;
        }

        [AllowAnonymous, Function("HomeFunctionJson")]
        public IActionResult GetHomeJson(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "json")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult(new { Message = "Hello from cloud", Type = "Json" });
        }
    }
}
