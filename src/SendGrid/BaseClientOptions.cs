using SendGrid.Helpers.Reliability;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace SendGrid
{
    /// <summary>
    /// Defines the options to use with the client.
    /// </summary>
    public class BaseClientOptions
    {
        private ReliabilitySettings reliabilitySettings = new ReliabilitySettings();

        /// <summary>
        /// The reliability settings to use on HTTP Requests.
        /// </summary>
        public ReliabilitySettings ReliabilitySettings
        {
            get => reliabilitySettings;
            set => reliabilitySettings = value ?? throw new ArgumentNullException(nameof(reliabilitySettings));
        }

        /// <summary>
        /// The request headers to use on HTTP Requests.
        /// </summary>
        public Dictionary<string, string> RequestHeaders { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The base URL.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The API version (defaults to "v3").
        /// </summary>
        public string Version { get; set; } = "v3";

        /// <summary>
        /// The path to the API endpoint.
        /// </summary>
        public string UrlPath { get; set; }

        /// <summary>
        /// The Auth header value.
        /// </summary>
        public AuthenticationHeaderValue Auth { get; set; }
    }
}
