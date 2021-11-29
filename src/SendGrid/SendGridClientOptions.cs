using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Headers;

namespace SendGrid
{
    /// <summary>
    /// Defines the options to use with the SendGrid client.
    /// </summary>
    public class SendGridClientOptions : BaseClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClientOptions"/> class.
        /// </summary>
        public SendGridClientOptions()
        {
            Host = "https://api.sendgrid.com";
        }

        private string? apiKey;

        /// <summary>
        /// The Twilio SendGrid API key.
        /// </summary>
        [DisallowNull]
        public string? ApiKey
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
    }
}
