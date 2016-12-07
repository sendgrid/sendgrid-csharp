using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class BypassListManagement
    {
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }
    }
}