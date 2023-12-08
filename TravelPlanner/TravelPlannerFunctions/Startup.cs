using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelPlannerFunctions;

[assembly: WebJobsStartup(typeof(Startup))]
namespace TravelPlannerFunctions
    {
    public class Startup : IWebJobsStartup
        {
        public void Configure(IWebJobsBuilder builder)
            {

           // if(Environment.GetEnvironmentVariable("supported_bank") == "BARCLAYS")
                builder.Services.AddSingleton<IAccountInforRepo, BarclaysAccountInfoRepo>();
            

            // singleton
            // transient
            // scoped

            //if (Environment.GetEnvironmentVariable("supported_bank") == "HSBC")
                //builder.Services.AddSingleton<IAccountInforRepo, HSBCAccountInfoRepo>();
            }
        }
    }
