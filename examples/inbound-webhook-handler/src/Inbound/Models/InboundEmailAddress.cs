using System.Text.Json.Serialization;

namespace Inbound.Models
{
    /// <summary>
    /// The address for Email recipient, including the name and email address.
    /// </summary>
    public class InboundEmailAddress
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [JsonPropertyName("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InboundEmailAddress" /> class.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="name">The name.</param>
        public InboundEmailAddress(string email, string name)
        {
            Email = email;
            Name = name;
        }
    }
}