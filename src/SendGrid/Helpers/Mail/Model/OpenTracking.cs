using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class OpenTracking
    {
        [JsonProperty(PropertyName = "enable")]
        public bool? Enable { get; set; }

        [JsonProperty(PropertyName = "substitution_tag")]
        public string SubstitutionTag { get; set; }
    }
}