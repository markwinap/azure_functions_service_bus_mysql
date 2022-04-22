using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace POCAzureFunctions
{
    public static class DBInsertServiceBusQueueTrigger
    {
        [FunctionName("DBInsertServiceBusQueueTrigger")]
        public static void Run([ServiceBusTrigger("myqueue", Connection = "pocazurefunction_SERVICEBUS")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
