using System;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace AzureFunctionEventHubTriggerMissingInfo
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }


        [Function("Function1")]
        //From Visual Studio project template Connection is filled with real connection string instead of just a reference to local.settings.json
        public void Function1([EventHubTrigger("event-hub-a", Connection = "EventHub", ConsumerGroup = "$Default")] string[] input, FunctionContext functionContext)
        {
            //Can SystemPropertiesArray be strongly typed? 
            if (!(functionContext.BindingContext.BindingData.TryGetValue("SystemPropertiesArray", out var systemPropertiesArray) && systemPropertiesArray != null))
                throw new InvalidOperationException($"Cannot find SystemPropertiesArray");

            EventHubSystemInfo[] eventHubSystemInfos = JsonSerializer.Deserialize<EventHubSystemInfo[]>((string)systemPropertiesArray)!;
            for (var i=0;i< input.Length;i++)
            {
                var message = input[i];
                var eventHubSystemInfo = eventHubSystemInfos[i];

                //******No Event Hub Name available on SystemPropertiesArray*******
                var eventHubName = eventHubSystemInfo.Name;
                ConsumeEventHubMessage(eventHubName: eventHubSystemInfo.Name, message);
            }
        }

        [Function("Function2")]
        //From Visual Studio project template Connection is filled with real connection string instead of just a reference to local.settings.json
        public void Run([EventHubTrigger("event-hub-b", Connection = "EventHub", ConsumerGroup = "$Default")] string[] input, FunctionContext functionContext)
        {
            //Can SystemPropertiesArray be strongly typed? 
            if (!(functionContext.BindingContext.BindingData.TryGetValue("SystemPropertiesArray", out var systemPropertiesArray) && systemPropertiesArray != null))
                throw new InvalidOperationException($"Cannot find SystemPropertiesArray");

            EventHubSystemInfo[] eventHubSystemInfos = JsonSerializer.Deserialize<EventHubSystemInfo[]>((string)systemPropertiesArray)!;
            for (var i = 0; i < input.Length; i++)
            {
                var message = input[i];
                var eventHubSystemInfo = eventHubSystemInfos[i];

                //******No Event Hub Name available on SystemPropertiesArray*******
                var eventHubName = eventHubSystemInfo.Name;
                ConsumeEventHubMessage(eventHubName: eventHubSystemInfo.Name, message);
            }
        }

        public void ConsumeEventHubMessage(string eventHubName, string data)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(eventHubName);
            //Do something
        }
    }
}
