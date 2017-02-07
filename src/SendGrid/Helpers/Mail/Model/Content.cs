using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Specifies the content of your email. You can include multiple mime types of content, but you must specify at least one. To include more than one mime type, simply add another object to the array containing the type and value parameters. If included, text/plain and text/html must be the first indices of the array in this order. If you choose to include the text/plain or text/html mime types, they must be the first indices of the content array in the order text/plain, text/html.*Content is NOT mandatory if you using a transactional template and have defined the template_id in the Request
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Allow for plaintext and plainhtml subclasses
        /// </summary>
        public Content()
        {
        }
        
        /// <summary>
        /// Creates the initial Content obect
        /// </summary>
        /// <param name="type">The mime type of the content you are including in your email. For example, text/plain or text/html.</param>
        /// <param name="value">The actual content of the specified mime type that you are including in your email.</param>
        public Content(string type, string value)
        {
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// The mime type of the content you are including in your email. For example, text/plain or text/html.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The actual content of the specified mime type that you are including in your email.
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }

    /// <summary>
    /// Helper class for plain text mime types
    /// </summary>
    public class PlainTextContent : Content
    {
        /// <summary>
        /// Create a content object with type text/plain
        /// </summary>
        /// <param name="value">The actual content of the specified mime type that you are including in your email.</param>
        public PlainTextContent(string value)
        {
            this.Type = MimeType.Text;
            this.Value = value;
        }
    }

    /// <summary>
    /// Helper class for plain html mime types
    /// </summary>
    public class HtmlContent : Content
    {
        /// <summary>
        /// Create a content object with type text/plain
        /// </summary>
        /// <param name="value">The actual content of the specified mime type that you are including in your email.</param>
        public HtmlContent(string value)
        {
            this.Type = MimeType.Html;
            this.Value = value;
        }
    }
}
