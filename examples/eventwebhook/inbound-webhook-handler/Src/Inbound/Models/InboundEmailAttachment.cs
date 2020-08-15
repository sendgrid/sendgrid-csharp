using Newtonsoft.Json;
using System.IO;

namespace Inbound.Models
{
    /// <summary>
    /// Strongly typed representation of the information sudmited by SendGrid in a 'inbound parse' webhook.
    /// </summary>
    public class InboundEmailAttachment
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the content-type. Defaults to text/plain if unspecified.
        /// </summary>
        /// <value>
        /// The content-type.
        /// </value>
        [JsonProperty("type", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public Stream Data { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [JsonProperty("filename", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        [JsonProperty("content-id", NullValueHandling = NullValueHandling.Ignore)]
        public string ContentId { get; set; }
    }

    
}
