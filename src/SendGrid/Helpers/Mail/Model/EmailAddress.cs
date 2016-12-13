using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class EmailAddress
    {
        public EmailAddress()
        {
        }

        public EmailAddress(string email, string name = null)
        {
            this.Email = email;
            this.Name = name;
        }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}