using System;
using System.Reflection;
using System.Collections.Generic;
using SendGrid.CSharp.HTTP.Client;

namespace SendGrid
{
    public class SendGridAPIClient
    {
        private string _apiKey;
        public string Version;
        public dynamic client;
        private Uri _baseUri;
        private enum Methods
        {
            GET, POST, PATCH, DELETE
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
        public SendGridAPIClient(string apiKey, String baseUri = "https://api.sendgrid.com", String version = "v3")
        {
            _baseUri = new Uri(baseUri);
            _apiKey = apiKey;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            Dictionary<String, String> requestHeaders = new Dictionary<String, String>();
            requestHeaders.Add("Authorization", "Bearer " + apiKey);
            requestHeaders.Add("Content-Type", "application/json");
            requestHeaders.Add("User-Agent", "sendgrid/" + Version + " csharp");
            requestHeaders.Add("Accept", "application/json");
            client = new Client(host: baseUri, requestHeaders: requestHeaders, version: version);
        }
    }
}
