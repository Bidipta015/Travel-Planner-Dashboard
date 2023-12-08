using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace TravelPlannerFunctions
{
    public static class HealthCheckFunction
    {
        [FunctionName("HealthCheckFunction")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "healthcheck")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Health check request received.");
            log.LogInformation($"Heartbeat: {DateTime.UtcNow}");
            return Task.FromResult<IActionResult>(new OkObjectResult("OK"));
        }
    }
}
