using System.IO;
using System.Text.Json.Serialization;

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
        [JsonPropertyName("type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        [JsonIgnore]
        public Stream Data { get; set; }

        /// <summary>
        /// Gets or sets the file name.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the content identifier.
        /// </summary>
        /// <value>
        /// The content identifier.
        /// </value>
        [JsonPropertyName("content-id")]
        public string ContentId { get; set; }
    }
}
