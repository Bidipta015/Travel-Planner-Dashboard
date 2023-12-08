using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TravelPlannerFunctions.Models;

namespace TravelPlannerFunctions
	{
	public class PlacesFunction
		{
		[FunctionName("Places-Get")]
		public static Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "places")] HttpRequest req,
			ILogger log)
			{
			var places = new Place[]
				{
					new Place{ Name= "Hotel 1", Price=100, Rating=0.3f }
					};
			return Task.FromResult<IActionResult>(new OkObjectResult(places));
			}
		}

	}

