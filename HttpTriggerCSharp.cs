using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Zhresearches.Function
{
    public static class HttpTriggerCSharp
    {
        [FunctionName("HttpTriggerCSharp")]
        
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function,  "post", Route = null)] HttpRequest req,
            ILogger log, [ServiceBus("myqueue-items",Connection="articlecollectors_SERVICEBUS")] ICollector<Article> queueCollector )
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Article data = JsonConvert.DeserializeObject<Article>(requestBody);
           
            data.CreateTime = DateTimeOffset.Now;
            queueCollector.Add(data);
           return new OkResult();
        }
    }

    public class Article{
        public DateTimeOffset  CreateTime { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; } 
    }
}
