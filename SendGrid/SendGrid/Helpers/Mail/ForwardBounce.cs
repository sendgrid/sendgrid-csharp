namespace SendGrid.Helpers.Mail
{
    using Newtonsoft.Json;

    public class ForwardBounce
    {
        [JsonProperty(PropertyName = "enable")]
        public bool Enable { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}
