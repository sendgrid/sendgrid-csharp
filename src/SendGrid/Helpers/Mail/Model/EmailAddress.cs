using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An email object containing the email address and name of the sender or recipient.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Create an empty email object.
        /// </summary>
        public EmailAddress()
        {
        }
        
        /// <summary>
        /// Create an email object containing the email of the sender or recipient and optionally include a name.
        /// </summary>
        /// <param name="email">The email address of the sender or recipient.</param>
        /// <param name="name">The name of the sender or recipient.</param>
        public EmailAddress(string email, string name = null)
        {
            this.Email = email;
            this.Name = name;
        }

        /// <summary>
        /// The name of the sender or recipient.
        /// </summary>
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        /// <summary>
        /// The email address of the sender or recipient.
        /// </summary>
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
    }
}