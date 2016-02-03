using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class SuppressionGroup
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("is_default")]
        public bool IsDefault { get; set; }
    }
}
