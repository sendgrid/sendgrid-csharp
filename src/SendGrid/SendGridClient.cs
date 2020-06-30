using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace SendGrid
{
    /// <summary>
    /// An HTTP client wrapper for interacting with Twilio SendGrid's API.
    /// </summary>
    public class SendGridClient : BaseClient
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
        /// <param name="httpErrorAsException">Whether HTTP error responses should be raised as exceptions.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(IWebProxy webProxy, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null, bool httpErrorAsException = false)
            : base(webProxy, buildOptions(apiKey, host, requestHeaders, version, urlPath, httpErrorAsException))
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
        /// <param name="httpErrorAsException">Whether HTTP error responses should be raised as exceptions.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(HttpClient httpClient, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null, bool httpErrorAsException = false)
            : base(httpClient, buildOptions(apiKey, host, requestHeaders, version, urlPath, httpErrorAsException))
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
        /// <param name="httpErrorAsException">Whether HTTP error responses should be raised as exceptions.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = null, string urlPath = null, bool httpErrorAsException = false)
            : base(buildOptions(apiKey, host, requestHeaders, version, urlPath, httpErrorAsException))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="options">A <see cref="SendGridClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(SendGridClientOptions options)
            : base(options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional HTTP client which may me injected in order to facilitate testing.</param>
        /// <param name="options">A <see cref="SendGridClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        public SendGridClient(HttpClient httpClient, SendGridClientOptions options)
            : base(httpClient, options)
        {
        }

        private static SendGridClientOptions buildOptions(string apiKey, string host, Dictionary<string, string> requestHeaders, string version, string urlPath, bool httpErrorAsException)
        {
            return new SendGridClientOptions
            {
                ApiKey = apiKey, // No default.
                Host = host ?? DefaultOptions.Host,
                RequestHeaders = requestHeaders ?? DefaultOptions.RequestHeaders,
                Version = version ?? DefaultOptions.Version,
                UrlPath = urlPath ?? DefaultOptions.UrlPath,
                HttpErrorAsException = httpErrorAsException // No default needed for bool.
            };
        }
    }
}
