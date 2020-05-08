using System.Net;
using System.Net.Http;

namespace SendGrid
{
    /// <summary>
    /// An HTTP client wrapper for interacting with the Twilio Email API.
    /// </summary>
    public class TwilioEmailClient : BaseClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioEmailClient"/> class.
        /// </summary>
        /// <param name="webProxy">Web proxy.</param>
        /// <param name="username">Your Twilio Email API key SID or Account SID.</param>
        /// <param name="password">Your Twilio Email API key secret or Account Auth Token.</param>
        /// <returns>Interface to the Twilio Email REST API.</returns>
        public TwilioEmailClient(IWebProxy webProxy, string username, string password)
            : base(webProxy, new TwilioEmailClientOptions(username, password))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioEmailClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
        /// <param name="username">Your Twilio Email API key SID or Account SID.</param>
        /// <param name="password">Your Twilio Email API key secret or Account Auth Token.</param>
        /// <returns>Interface to the Twilio Email REST API.</returns>
        public TwilioEmailClient(HttpClient httpClient, string username, string password)
            : base(httpClient, new TwilioEmailClientOptions(username, password))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TwilioEmailClient"/> class.
        /// </summary>
        /// <param name="username">Your Twilio Email API key SID or Account SID.</param>
        /// <param name="password">Your Twilio Email API key secret or Account Auth Token.</param>
        /// <returns>Interface to the Twilio Email REST API.</returns>
        public TwilioEmailClient(string username, string password)
            : base(new TwilioEmailClientOptions(username, password))
        {
        }
    }
}
