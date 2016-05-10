using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class ApiKey
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("api_key")]
        public string Key { get; set; }

        [JsonProperty("api_key_id")]
        public string KeyId { get; set; }

        [JsonProperty("scopes")]
        public string[] Scopes { get; set; }
    }
}
