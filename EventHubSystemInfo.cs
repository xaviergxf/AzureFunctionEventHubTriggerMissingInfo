using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureFunctionEventHubTriggerMissingInfo
{
    public class EventHubSystemInfo
    {
        [JsonPropertyName("SequenceNumber")]
        public long SequenceNumber { get; set; }

        [JsonPropertyName("Offset")]
        public string Offset { get; set; }

        [JsonPropertyName("PartitionKey")]
        public string? PartitionKey { get; set; }

        [JsonPropertyName("EnqueuedTimeUtc")]
        public DateTime EnqueuedTimeUtc { get; set; }

        public string Name { get; set; }
    }
}
