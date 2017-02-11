// <copyright file="SendGridClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace SendGrid
{
    using Helpers.Mail;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A HTTP client wrapper for interacting with SendGrid's API
    /// </summary>
    public class SendGridClient
    {
        /// <summary>
        /// The base URL for the API call.
        /// </summary>
        private string host;

        /// <summary>
        /// The request headers.
        /// </summary>
        private Dictionary<string, string> requestHeaders;

        /// <summary>
        /// The API version.
        /// </summary>
        private string version;

        /// <summary>
        /// The path to the API resource.
        /// </summary>
        private string urlPath;

        /// <summary>
        /// The request media type.
        /// </summary>
        private string mediaType;

        /// <summary>
        /// The web proxy.
        /// </summary>
        private IWebProxy webProxy;

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="webProxy">Web proxy.</param>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(IWebProxy webProxy, string apiKey, string host = "https://api.sendgrid.com", Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
            : this(apiKey, host, requestHeaders, version, urlPath)
        {
            this.WebProxy = webProxy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
        {
            this.Host = (host != null) ? host : "https://api.sendgrid.com";
            this.Version = this.GetType().GetTypeInfo().Assembly.GetName().Version.ToString();
            Dictionary<string, string> defaultHeaders = new Dictionary<string, string>();
            defaultHeaders.Add("Authorization", "Bearer " + apiKey);
            defaultHeaders.Add("Content-Type", "application/json");
            defaultHeaders.Add("User-Agent", "sendgrid/" + this.Version + " csharp");
            defaultHeaders.Add("Accept", "application/json");
            this.AddRequestHeader(defaultHeaders);
            if (requestHeaders != null)
            {
                this.AddRequestHeader(requestHeaders);
            }

            this.SetVersion(version);
            this.SetUrlPath(urlPath);
        }

        /// <summary>
        /// The supported API methods.
        /// </summary>
        public enum Method
        {
            /// <summary>
            /// Remove a resource.
            /// </summary>
            DELETE,

            /// <summary>
            /// Get a resource.
            /// </summary>
            GET,

            /// <summary>
            /// Modify a portion of the resource.
            /// </summary>
            PATCH,

            /// <summary>
            /// Create a resource or execute a function. (e.g send an email)
            /// </summary>
            POST,

            /// <summary>
            /// Update an entire resource.s
            /// </summary>
            PUT
        }

        /// <summary>
        /// Gets or sets the web proxy.
        /// </summary>
        public IWebProxy WebProxy
        {
            get
            {
                return this.webProxy;
            }

            set
            {
                this.webProxy = value;
            }
        }

        /// <summary>
        /// Gets or sets the request media type.
        /// </summary>
        public string MediaType
        {
            get
            {
                return this.mediaType;
            }

            set
            {
                this.mediaType = value;
            }
        }

        /// <summary>
        /// Gets or sets the path to the API resource.
        /// </summary>
        public string UrlPath
        {
            get
            {
                return this.urlPath;
            }

            set
            {
                this.urlPath = value;
            }
        }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string Version
        {
            get
            {
                return this.version;
            }

            set
            {
                this.version = value;
            }
        }

        /// <summary>
        /// Gets or sets the request headers.
        /// </summary>
        public Dictionary<string, string> RequestHeaders
        {
            get
            {
                return this.requestHeaders;
            }

            set
            {
                this.requestHeaders = value;
            }
        }

        /// <summary>
        /// Gets or sets the base URL for the API call.
        /// </summary>
        public string Host
        {
            get
            {
                return this.host;
            }

            set
            {
                this.host = value;
            }
        }

        /// <summary>
        /// Add requestHeaders to the API call
        /// </summary>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        public void AddRequestHeader(Dictionary<string, string> requestHeaders)
        {
            this.RequestHeaders = (this.RequestHeaders != null)
                    ? this.RequestHeaders.Union(requestHeaders).ToDictionary(pair => pair.Key, pair => pair.Value) : requestHeaders;
        }

        /// <summary>
        /// Set or update the UrlPath
        /// </summary>
        /// <param name="urlPath">The endpoint without a leading or trailing slash</param>
        public void SetUrlPath(string urlPath)
        {
            this.UrlPath = urlPath;
        }

        /// <summary>
        /// Get the urlPath to the API endpoint
        /// </summary>
        /// <returns>Url path to the API endpoint</returns>
        public string GetUrlPath()
        {
            return this.UrlPath;
        }

        /// <summary>
        /// Add the authorization header, override to customize
        /// </summary>
        /// <param name="header">Authorization header</param>
        /// <returns>Authorization value to add to the header</returns>
        public virtual AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            string[] split = header.Value.Split(new char[0]);
            return new AuthenticationHeaderValue(split[0], split[1]);
        }

        /// <summary>
        /// Add the version of the API, override to customize
        /// </summary>
        /// <param name="version">Version string to add to the URL</param>
        public void SetVersion(string version)
        {
            this.Version = version;
        }

        /// <summary>
        /// Get the version of the API, override to customize
        /// </summary>
        /// <returns>Version of the API</returns>
        public string GetVersion()
        {
            return this.Version;
        }

        /// <summary>
        /// Make the call to the API server, override for testing or customization
        /// </summary>
        /// <param name="client">Client object ready for communication with API</param>
        /// <param name="request">The parameters for the API call</param>
        /// <param name="cancellationToken">Cancel the asynchronous call</param>
        /// <returns>Response object</returns>
        public async Task<Response> MakeRequest(HttpClient client, HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return new Response(response.StatusCode, response.Content, response.Headers);
        }

        /// <summary>
        /// Prepare for async call to the API server
        /// </summary>
        /// <param name="method">HTTP verb</param>
        /// <param name="requestBody">JSON formatted string</param>
        /// <param name="requestHeaders">Custom request headers.</param>
        /// <param name="queryParams">JSON formatted query paramaters</param>
        /// <param name="urlPath">The path to the API endpoint.</param>
        /// <param name="cancellationToken">Cancel the asynchronous call.</param>
        /// <returns>Response object</returns>
        public async Task<Response> RequestAsync(
                                                 SendGridClient.Method method,
                                                 string requestBody = null,
                                                 Dictionary<string, string> requestHeaders = null,
                                                 string queryParams = null,
                                                 string urlPath = null,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            using (var client = this.BuildHttpClient())
            {
                try
                {
                    // Build the URL
                    client.BaseAddress = new Uri(this.Host);
                    if (urlPath != null)
                    {
                        this.SetUrlPath(urlPath);
                    }

                    string endpoint = this.BuildUrl(queryParams);

                    // Build the request headers
                    if (requestHeaders != null)
                    {
                        this.AddRequestHeader(requestHeaders);
                    }

                    client.DefaultRequestHeaders.Accept.Clear();
                    if (this.RequestHeaders != null)
                    {
                        foreach (KeyValuePair<string, string> header in this.RequestHeaders)
                        {
                            if (header.Key == "Authorization")
                            {
                                client.DefaultRequestHeaders.Authorization = this.AddAuthorization(header);
                            }
                            else if (header.Key == "Content-Type")
                            {
                                this.MediaType = header.Value;
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(this.MediaType));
                            }
                            else
                            {
                                client.DefaultRequestHeaders.Add(header.Key, header.Value);
                            }
                        }
                    }

                    // Build the request body
                    StringContent content = null;
                    if (requestBody != null)
                    {
                        content = new StringContent(requestBody, Encoding.UTF8, this.MediaType);
                    }

                    // Build the final request
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = new HttpMethod(method.ToString()),
                        RequestUri = new Uri(endpoint),
                        Content = content
                    };
                    return await this.MakeRequest(client, request, cancellationToken).ConfigureAwait(false);
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    string message;
                    message = (ex is HttpRequestException) ? ".NET HttpRequestException" : ".NET Exception";
                    message = message + ", raw message: \n\n";
                    response.Content = new StringContent(message + ex.Message);
                    return new Response(response.StatusCode, response.Content, response.Headers);
                }
            }
        }

        /// <summary>
        /// Make a request to send an email through SendGrid asychronously.
        /// </summary>
        /// <param name="msg">A SendGridMessage object with the details for the request.</param>
        /// <param name="cancellationToken">Cancel the asychronous call.</param>
        /// <returns>A Response object.</returns>
        public async Task<Response> SendEmailAsync(SendGridMessage msg, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await this.RequestAsync(
                                           Method.POST,
                                           msg.Serialize(),
                                           urlPath: "mail/send",
                                           cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Build the final URL
        /// </summary>
        /// <param name="queryParams">A string of JSON formatted query parameters (e.g {'param': 'param_value'})</param>
        /// <returns>Final URL</returns>
        private string BuildUrl(string queryParams = null)
        {
            string url = null;

            if (this.GetVersion() != null)
            {
                url = this.Host + "/" + this.GetVersion() + "/" + this.GetUrlPath();
            }
            else
            {
                url = this.Host + "/" + this.GetUrlPath();
            }

            if (queryParams != null)
            {
                var ds_query_params = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(queryParams);
                string query = "?";
                foreach (var pair in ds_query_params)
                {
                    if (query != "?")
                    {
                        query = query + "&";
                    }

                    query = query + pair.Key + "=" + pair.Value.ToString();
                }

                url = url + query;
            }

            return url;
        }

        /// <summary>
        /// Factory method to return the right HttpClient settings.
        /// </summary>
        /// <returns>Instance of HttpClient</returns>
        private HttpClient BuildHttpClient()
        {
            // Add the WebProxy if set
            if (this.WebProxy != null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = this.WebProxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };
                return new HttpClient(httpClientHandler);
            }

            return new HttpClient();
        }
    }
}
