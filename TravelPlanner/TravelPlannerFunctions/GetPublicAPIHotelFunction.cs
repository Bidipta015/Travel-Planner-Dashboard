using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Reflection;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace TravelPlannerFunctions
{
public class GetPublicApiHotelFunction
    {
        [FunctionName("GetDataAndStoreToFile")]
        public static async Task<OkObjectResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                const string targetFolder = "Fixture";
                var currentDirectory = Directory.GetParent(Directory.GetParent(Directory.GetParent
                    (Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!)?.FullName!)!
                    .FullName)!.FullName)!.FullName;
                currentDirectory = Path.Combine(currentDirectory, targetFolder);
                //Call the Function to retrieve the data
                var apiData = await GetDataFromApiAsync();

                // Store the data in a fixture file
                var filePath = Path.Combine(currentDirectory, "hotel_fixture.json");
                await File.WriteAllTextAsync(filePath, apiData.ToString());

                return new OkObjectResult($"Data stored in file: {filePath}");
            }
            catch(Exception e)
            {
                log.LogError($"Error: {e.Message}");
                return null;
            }
        }

        private static async Task<object> GetDataFromApiAsync()
        {
            ////rapidapi.com/apiheya/api/sky-scrapper ->api/v1/hotels/searchHotels
            var client = new HttpClient();

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                

                RequestUri = new Uri("https://sky-scrapper.p.rapidapi.com/api/v1/hotels/searchHotels?entityId=27537542&checkin=2023-12-09&checkout=2023-12-11&adults=1&rooms=1&limit=30&currency=USD&market=en-US&countryCode=US"),
                Headers =
                {
                    { "X-RapidAPI-Key", "66c7750668mshd34b412bc6b74e2p13e0e5jsn6d8e962a70de" },
                    { "X-RapidAPI-Host", "sky-scrapper.p.rapidapi.com" },
                },
            };

            try
            {
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                string jsonContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<object>(jsonContent);
            }
            finally
            {
                client.Dispose();
            }
        }
    }
}

