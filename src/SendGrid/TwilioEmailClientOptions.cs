using System;
using System.Net.Http.Headers;

namespace SendGrid
{
    /// <summary>
    /// Defines the options to use with the Twilio Email client.
    /// </summary>
    public class TwilioEmailClientOptions : BaseClientOptions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioEmailClientOptions"/> class.
        /// </summary>
        public TwilioEmailClientOptions()
        {
            Host = "https://email.twilio.com";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioEmailClientOptions"/> class.
        /// <param name="username">Your Twilio Email API key SID or Account SID.</param>
        /// <param name="password">Your Twilio Email API key secret or Account Auth Token.</param>
        /// </summary>
        public TwilioEmailClientOptions(string username, string password)
        : this()
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException(nameof(password));
            }

            var credentials = username + ":" + password;
            var credentialBytes = System.Text.Encoding.UTF8.GetBytes(credentials);
            var encodedCredentials = System.Convert.ToBase64String(credentialBytes);

            Auth = new AuthenticationHeaderValue("Basic", encodedCredentials);
        }
    }
}
