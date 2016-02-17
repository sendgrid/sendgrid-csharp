using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class Segment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("list_id")]
        public long ListId { get; set; }

        [JsonProperty("conditions")]
        public Condition[] Conditions { get; set; }
    }
}
