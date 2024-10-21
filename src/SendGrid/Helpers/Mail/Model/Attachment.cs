// <copyright file="Attachment.cs" company="Twilio SendGrid">
// Copyright (c) Twilio SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

#if NETSTANDARD2_0
using System.Text.Json.Serialization;
#else
using Newtonsoft.Json;
#endif

namespace SendGrid.Helpers.Mail
{
    /// <summary>
    /// Gets or sets an array of objects in which you can specify any attachments you want to include.
    /// </summary>
#if NETSTANDARD2_0
    // Globally defined by setting ReferenceHandler on the JsonSerializerOptions object
#else
    [JsonObject(IsReference = false)]
#endif
    public class Attachment
    {
        /// <summary>
        /// Gets or sets the Base64 encoded content of the attachment.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("content")]
#else
        [JsonProperty(PropertyName = "content")]
#endif
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the mime type of the content you are attaching. For example, application/pdf or image/jpeg.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("type")]
#else
        [JsonProperty(PropertyName = "type")]
#endif
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the filename of the attachment.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("filename")]
#else
        [JsonProperty(PropertyName = "filename")]
#endif
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the content-disposition of the attachment specifying how you would like the attachment to be displayed. For example, "inline" results in the attached file being displayed automatically within the message while "attachment" results in the attached file requiring some action to be taken before it is displayed (e.g. opening or downloading the file). Defaults to "attachment". Can be either "attachment" or "inline".
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("disposition")]
#else
        [JsonProperty(PropertyName = "disposition")]
#endif
        public string Disposition { get; set; }

        /// <summary>
        /// Gets or sets a unique id that you specify for the attachment. This is used when the disposition is set to "inline" and the attachment is an image, allowing the file to be displayed within the body of your email. Ex: <img src="cid:ii_139db99fdb5c3704"></img>.
        /// </summary>
#if NETSTANDARD2_0
        [JsonPropertyName("content_id")]
#else
        [JsonProperty(PropertyName = "content_id")]
#endif
        public string ContentId { get; set; }
    }
}
