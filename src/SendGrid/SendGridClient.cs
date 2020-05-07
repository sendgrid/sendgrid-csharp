using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace SendGrid
{
    /// <summary>
    /// An HTTP client wrapper for interacting with Twilio SendGrid's API.
    /// </summary>
    public class SendGridClient : BaseInterface
    {
        private static readonly SendGridClientOptions DefaultOptions = new SendGridClientOptions();

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="webProxy">Web proxy.</param>
        /// <param name="apiKey">Your Twilio SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com).</param>
        /// <param name="requestHeaders">A dictionary of request headers.</param>
        /// <param name="version">API version, override AddVersion to customize.</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint).</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(IWebProxy webProxy, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null)
            : base(webProxy, buildOptions(apiKey, host, requestHeaders, version, urlPath))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
        /// <param name="apiKey">Your Twilio SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com).</param>
        /// <param name="requestHeaders">A dictionary of request headers.</param>
        /// <param name="version">API version, override AddVersion to customize.</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint).</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(HttpClient httpClient, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null)
            : base(httpClient, buildOptions(apiKey, host, requestHeaders, version, urlPath))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your Twilio SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com).</param>
        /// <param name="requestHeaders">A dictionary of request headers.</param>
        /// <param name="version">API version, override AddVersion to customize.</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint).</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null)
            : base(buildOptions(apiKey, host, requestHeaders, version, urlPath))
        {
        }

        private static SendGridClientOptions buildOptions(string apiKey, string host, Dictionary<string, string> requestHeaders, string version, string urlPath)
        {
            return new SendGridClientOptions
            {
                ApiKey = apiKey, // No default.
                Host = host ?? DefaultOptions.Host,
                RequestHeaders = requestHeaders ?? DefaultOptions.RequestHeaders,
                Version = version ?? DefaultOptions.Version,
                UrlPath = urlPath ?? DefaultOptions.UrlPath
            };
        }
    }
}
