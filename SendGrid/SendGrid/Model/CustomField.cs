using Newtonsoft.Json;

namespace SendGrid.Model
{
    public class CustomField : Field
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
