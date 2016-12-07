using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class SandboxMode
    {
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }
    }
}