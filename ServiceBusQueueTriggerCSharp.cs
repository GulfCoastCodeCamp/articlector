using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.ServiceBus;

namespace zhresearches.Function
{
    public static class ServiceBusQueueTriggerCSharp
    {
        [FunctionName("ServiceBusQueueTriggerCSharp")]
        public static void Run([ServiceBusTrigger("myqueue-items", Connection = "articlecollectors_SERVICEBUS")]Microsoft.Azure.ServiceBus.Message  myQueueItem, ILogger log)
        {
            if(myQueueItem!=null){
                 log.LogInformation($"C# ServiceBus queue trigger function processed message: {System.Text.Encoding.UTF8.GetString(myQueueItem.Body)}");
            }else{
                 log.LogInformation($"C# ServiceBus queue trigger function processed message: null");
            }
        }
    }
}
