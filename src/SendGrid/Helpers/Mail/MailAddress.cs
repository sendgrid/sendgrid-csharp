using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class MailAddress
    {
        public MailAddress()
        {
        }

        public MailAddress(string email, string name = null)
        {
            this.Address = email;
            this.Name = name;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Address { get; set; }
    }
}