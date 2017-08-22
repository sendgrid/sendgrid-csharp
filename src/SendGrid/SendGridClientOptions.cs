namespace SendGrid
{
    using System;
    using System.Collections.Generic;
    using SendGrid.Helpers.Reliability;

    /// <summary>
    /// Defines the options to use with the SendGrid client
    /// </summary>
    public class SendGridClientOptions
    {
        private ReliabilitySettings reliabilitySettings = new ReliabilitySettings();

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClientOptions"/> class.
        /// </summary>
        public SendGridClientOptions()
        {
            RequestHeaders = new Dictionary<string, string>();
            Host = "https://api.sendgrid.com";
            Version = "v3";
        }

        /// <summary>
        /// Gets or sets the reliability settings to use on HTTP Requests
        /// </summary>
        public ReliabilitySettings ReliabilitySettings
        {
            get => this.reliabilitySettings;

            set => this.reliabilitySettings = value ?? throw new ArgumentNullException(nameof(value));
        }

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
