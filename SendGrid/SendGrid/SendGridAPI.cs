using System;
using System.Collections.Generic;
using System.Reflection;
using SendGrid.Interfaces;
using SendGrid.Concrete;

namespace SendGrid
{
    public class SendGridAPIClient : IDisposable
    {
        private const string ApiVersion = "v3";
        private const string BaseUri = "https://api.sendgrid.com";

        private string _apiKey;
        private Uri _baseUri;

        public string Version { get
            {
                return GetVersion();
            }
        }
        public SendGridHttpClient Client { get; private set; }

        #region Services list

        public IMailService Mail { get; private set; }

        #endregion

        #region Constructors
        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
        public SendGridAPIClient(string apiKey, string baseUri, string version)
        {
            _baseUri = new Uri(baseUri);
            _apiKey = apiKey;

            Client = new SendGridHttpClient(host: baseUri, requestHeaders: GetRequestHeaders(), version: version);

            InitializeServices();            
        }

        public SendGridAPIClient(string apiKey, string baseUri)
        {
            _baseUri = new Uri(BaseUri);
            _apiKey = apiKey;

            Client = new SendGridHttpClient(host: baseUri, requestHeaders: GetRequestHeaders(), version: ApiVersion);

            InitializeServices();          
        }

        public SendGridAPIClient(string apiKey)
        {
            _baseUri = new Uri(BaseUri);
            _apiKey = apiKey;

            Client = new SendGridHttpClient(host: BaseUri, requestHeaders: GetRequestHeaders(), version: ApiVersion);

            InitializeServices();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Request Headers to add
        /// </summary>
        /// <returns>Request Headers to add to the http call</returns>
        private Dictionary<string, string> GetRequestHeaders()
        {
            var requestHeaders = new Dictionary<String, String>();
            requestHeaders.Add("Authorization", "Bearer " + _apiKey);
            requestHeaders.Add("Content-Type", "application/json");
            requestHeaders.Add("User-Agent", "sendgrid/" + Version + " csharp");
            requestHeaders.Add("Accept", "application/json");

            return requestHeaders;
        }

        /// <summary>
        /// Get version of the api
        /// </summary>
        /// <returns>Version number of the api</returns>
        private string GetVersion()
        {
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        /// <summary>
        /// Initialize component api services
        /// </summary>
        private void InitializeServices()
        {
            this.Mail = new MailService(Client);
        }

        #endregion

        /// <summary>
        /// Dispose elements
        /// </summary>
        public void Dispose()
        {
            Client = null;
            Mail = null;
        }
    }
}
