using Newtonsoft.Json;

namespace Inbound.Models
{
    /// <summary>
    /// The SMTP envelope.
    /// </summary>
    public class InboundEmailEnvelope
    {
        /// <summary>
        /// Gets or sets to, which is a single-element array containing the address that we received the email to.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        [JsonProperty("to", NullValueHandling = NullValueHandling.Ignore)]
        public string[] To { get; set; }

        /// <summary>
        /// Gets or sets from, which is the return path for the message.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        [JsonProperty("from", NullValueHandling = NullValueHandling.Ignore)]
        public string From { get; set; }
    }

    
}
