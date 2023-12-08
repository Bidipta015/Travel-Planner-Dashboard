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
    public class Function1
    {
        private readonly IAccountInforRepo accountInfoRepo;

        [FunctionName("Function1")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "name/{name}")] HttpRequest req,
            string name,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            // string name = req.Query["name"];
            // http://localhost:7071/function_name/execute?paramname1=paramvalue1
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";
             

            return new OkObjectResult(responseMessage);
        }

        public Function1(IAccountInforRepo accountInfoRepo)
            {
            this.accountInfoRepo = accountInfoRepo;
            }

        public class PaymentRequest
            {
            public    decimal Amount { get; set; }
            public Guid FromAccount { get; set; }
            public Guid ToAccount { get; set; }
            }



        public void MakeAPayment(PaymentRequest req)
            {
            decimal? currentBalance = accountInfoRepo.GetACcountBalance(req.FromAccount);
            if(currentBalance == null)
                {
                return;
                }

            if(currentBalance < req.Amount) {
                return;
                }

            //SendMoney(req.Amount, req.FromAccount, req.FromAccount);
            }

        //homework
        // 1. add vanila endpoint for healthcheck and heartbeat
        // 2. understand how dependency injection can be set in function app
        // 3. create a service to pull information about travel recommendation
        }

    public interface IAccountInforRepo
        {
        decimal? GetACcountBalance(Guid Account);
        }

    public class BarclaysAccountInfoRepo : IAccountInforRepo
        {
        public decimal? GetACcountBalance(Guid Account)
            {
            return 1_000_000;
            }
        }
    }
