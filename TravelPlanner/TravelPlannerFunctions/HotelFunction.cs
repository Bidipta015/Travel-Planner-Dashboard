using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TravelPlannerFunctions.Models;
using System.Linq;
using Microsoft.Extensions.Primitives;

namespace TravelPlannerFunctions
	{
    public class HotelFunction
		{
		[FunctionName("Hotel-Get")]
        public static Task<IActionResult> Run(
			[HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Hotel")] HttpRequest req,
            ILogger log)
            {

                //Initializing the value for Query 
                var place = req.Query["place"];
                var price = decimal.TryParse(req.Query["price"], out var parsedPrice) ? parsedPrice : 0;
                var bedrooms = int.TryParse(req.Query["bedrooms"], out var parsedBedrooms) ? parsedBedrooms : 0;
                var ratings = double.TryParse(req.Query["ratings"], out var parsedRatings) ? parsedRatings : 0;

                var hotel = new[]
                    {
                        new HotelModel { Id = 1, Place = "City A", Price = 100, Bedrooms = 2, Ratings = 4.5 },
                        new HotelModel { Id = 2, Place = "City B", Price = 150, Bedrooms = 3, Ratings = 4.0 },
                    };
                var filteredHotels = QueryData(place, price, bedrooms, ratings, hotel);

                var formattedResult = FormatResult(filteredHotels);

                return Task.FromResult<IActionResult>(new OkObjectResult(formattedResult));
            }

        private static IEnumerable<HotelModel> QueryData(StringValues place, decimal price, int bedrooms, double ratings, IEnumerable<HotelModel> hotel)
            {
            return hotel.Where(h =>
                (string.IsNullOrEmpty(place) || h.Place == place) &&
                (price == 0 || h.Price <= price) &&
                (bedrooms == 0 || h.Bedrooms == bedrooms) &&
                (ratings == 0 || h.Ratings >= ratings)
            ).ToArray();
            }

        private static string FormatResult(IEnumerable<HotelModel> filteredHotels)
        {
            return filteredHotels.Aggregate("Id\tPlace\tPrice\tBedrooms\tRatings\n", (current, hotel) => current + $"{hotel.Id}\t{hotel.Place}\t{hotel.Price}\t{hotel.Bedrooms}\t{hotel.Ratings}\n");
        }
    }
}

