using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class CustomFieldMetadata : Field
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
