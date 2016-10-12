using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    public class Email
    {
        public Email()
        {
        }

        public Email(string email, string name = null)
        {
            this.Address = email;
            this.Name = name;
        }

        private string _name;
        private static readonly char[] Quote = {'\"'};
        [JsonProperty(PropertyName = "name")]
        public string Name {
            get { return _name; }
            set
            {
                _name = value?.Trim(Quote);
            }
        }

        [JsonProperty(PropertyName = "email")]
        public string Address { get; set; }
    }
}