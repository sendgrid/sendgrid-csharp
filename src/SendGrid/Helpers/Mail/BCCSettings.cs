using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class BCCSettings
    {
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}