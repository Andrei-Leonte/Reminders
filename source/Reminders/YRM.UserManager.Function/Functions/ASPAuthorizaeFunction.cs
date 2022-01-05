using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace YRM.UserManager.Function.Functions
{
    public class ASPAuthorizaeFunction
    {
        private readonly ILogger _logger;

        public ASPAuthorizaeFunction(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ASPAuthorizaeFunction>();
        }

        [Authorize, Function("ASPAuthorizaeFunction")]
        public HttpResponseData Run([HttpTrigger(
            "get", "post", Route = "authorize")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }
    }
}
