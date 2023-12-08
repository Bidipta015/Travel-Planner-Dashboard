using Microsoft.AspNetCore.Mvc;

namespace TravelPlanner.Controllers
    {
    [Route("healthcheck")]
    [ApiController]
    public class HealthCheckController : ControllerBase
        {
        [HttpGet]
        public ActionResult<HealthCheckResponse> CheckHealth()
            {
            return new HealthCheckResponse { Status = "ok" };
            }
        }
    }
