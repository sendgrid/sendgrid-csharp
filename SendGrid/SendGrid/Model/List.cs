using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class List
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("recipient_count")]
        public long RecipientCount { get; set; }
    }
}
