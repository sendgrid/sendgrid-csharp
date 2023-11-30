using System;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace SendGrid
{
    /// <summary>
    /// Defines the options to use with the SendGrid client.
    /// </summary>
    public class SendGridClientOptions : BaseClientOptions
    {
        Dictionary<string, string> REGION_HOST_MAP = new Dictionary<string, string>
        {
            {"eu", "https://api.eu.sendgrid.com/"},
            {"global", "https://api.sendgrid.com/"}
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClientOptions"/> class.
        /// </summary>
        public SendGridClientOptions()
        {
            Host = "https://api.sendgrid.com";
        }

        private string apiKey;

        /// <summary>
        /// The Twilio SendGrid API key.
        /// </summary>
        public string ApiKey
        {
            get => apiKey;

            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(apiKey));
                }

                apiKey = value;
                Auth = new AuthenticationHeaderValue("Bearer", apiKey);
            }
        }

        /// <summary>
        /// Sets the data residency for the SendGrid client.
        /// </summary>
        /// <param name="region">The desired data residency region ("global" or "eu").</param>
        /// Global is the default residency (or region)
        /// Global region means the message will be sent through https://api.sendgrid.com
        /// EU region means the message will be sent through https://api.eu.sendgrid.com
        /// <returns>The updated SendGridClientOptions instance.</returns>
        public SendGridClientOptions SetDataResidency(string region)
        {
            if (string.IsNullOrWhiteSpace(region))
            {
                throw new ArgumentNullException(nameof(region));
            }

            if (!REGION_HOST_MAP.ContainsKey(region))
            {
                throw new InvalidOperationException("Region can only be 'global' or 'eu'.");
            }
            string result = REGION_HOST_MAP.ContainsKey(region) ? REGION_HOST_MAP[region] : "https://api.sendgrid.com";
            Host = result;
            return this;
        }
    }
}
