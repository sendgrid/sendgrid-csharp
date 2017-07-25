using SendGrid.Helpers.Reliability;

namespace SendGrid
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the options to use with the SendGrid client
    /// </summary>
    public class SendGridClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClientOptions"/> class.
        /// </summary>
        public SendGridClientOptions()
        {
            ReliabilitySettings = new ReliabilitySettings();
            RequestHeaders = new Dictionary<string, string>();
            Host = "https://api.sendgrid.com";
            Version = "v3";
        }

        /// <summary>
        /// Gets the reliability settings to use on HTTP Requests
        /// </summary>
        public ReliabilitySettings ReliabilitySettings { get; private set; }

        /// <summary>
        /// Gets or sets the SendGrid API key
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the request headers to use on HttpRequests sent to SendGrid
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; }

        /// <summary>
        /// Gets or sets base url (e.g. https://api.sendgrid.com, this is the default)
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets API version, override AddVersion to customize
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the path to the API endpoint.
        /// </summary>
        public string UrlPath { get; set; }
    }
}