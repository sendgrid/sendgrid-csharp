using Newtonsoft.Json;

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// An array of objects in which you can specify any attachments you want to include.
    /// </summary>
    public class Attachment
    {
        /// <summary>
        /// The Base64 encoded content of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "content")]
        public string Content { get; set; }

        /// <summary>
        /// The mime type of the content you are attaching. For example, application/pdf or image/jpeg.
        /// </summary>
        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        /// <summary>
        /// The filename of the attachment.
        /// </summary>
        [JsonProperty(PropertyName = "filename")]
        public string Filename { get; set; }

        /// <summary>
        /// The content-disposition of the attachment specifying how you would like the attachment to be displayed. For example, "inline" results in the attached file being displayed automatically within the message while "attachment" results in the attached file requiring some action to be taken before it is displayed (e.g. opening or downloading the file). Defaults to "attachment". Can be either "attachment" or "inline".
        /// </summary>
        [JsonProperty(PropertyName = "disposition")]
        public string Disposition { get; set; }

        /// <summary>
        /// A unique id that you specify for the attachment. This is used when the disposition is set to "inline" and the attachment is an image, allowing the file to be displayed within the body of your email. Ex: <img src="cid:ii_139db99fdb5c3704"></img>
        /// </summary>
        [JsonProperty(PropertyName = "content_id")]
        public string ContentId { get; set; }
    }
}