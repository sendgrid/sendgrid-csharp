using Newtonsoft.Json;
using SendGrid.Utilities;
using System;

namespace SendGrid.Model
{
    public class Bounce
    {
        [JsonProperty("created")]
        [JsonConverter(typeof(EpochConverter))]
        public DateTime CreatedOn { get; set; }

        [JsonProperty("email")]
        public string EmailAddress { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
