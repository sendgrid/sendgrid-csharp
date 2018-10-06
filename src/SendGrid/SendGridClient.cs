// <copyright file="SendGridClient.cs" company="SendGrid">
// Copyright (c) SendGrid. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Newtonsoft.Json;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Reliability;
using System;
using System.IO;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SendGrid.Helpers.ContactDb;

namespace SendGrid
{
    /// <summary>
    /// A HTTP client wrapper for interacting with SendGrid's API
    /// </summary>
    public class SendGridClient : ISendGridClient
    {
        private readonly SendGridClientOptions options = new SendGridClientOptions();

        /// <summary>
        /// The HttpClient instance to use for all calls from this SendGridClient instance.
        /// </summary>
        private HttpClient client;

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
        public SendGridClient(IWebProxy webProxy, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
        {
            // Create client with WebProxy if set
            if (webProxy != null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = webProxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };

                var retryHandler = new RetryDelegatingHandler(httpClientHandler, this.options.ReliabilitySettings);

                this.client = new HttpClient(retryHandler);
            }
            else
            {
                this.client = this.CreateHttpClientWithRetryHandler();
            }

            this.InitiateClient(apiKey, host, requestHeaders, version, urlPath);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="options">A <see cref="SendGridClientOptions"/> instance that defines the configuration settings to use with the client </param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(SendGridClientOptions options)
            : this(null, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(HttpClient httpClient, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
            : this(httpClient, new SendGridClientOptions() { ApiKey = apiKey, Host = host, RequestHeaders = requestHeaders, Version = version, UrlPath = urlPath })
        {
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
            : this(httpClient: null, apiKey: apiKey, host: host, requestHeaders: requestHeaders, version: version, urlPath: urlPath)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional http client which may me injected in order to facilitate testing.</param>
        /// <param name="options">A <see cref="SendGridClientOptions"/> instance that defines the configuration settings to use with the client </param>
        /// <returns>Interface to the SendGrid REST API</returns>
        internal SendGridClient(HttpClient httpClient, SendGridClientOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            this.options = options;
            this.client = (httpClient == null) ? this.CreateHttpClientWithRetryHandler() : httpClient;

            this.InitiateClient(options.ApiKey, options.Host, options.RequestHeaders, options.Version, options.UrlPath);
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
        /// Gets or sets the path to the API resource.
        /// </summary>
        public string UrlPath { get; set; }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Gets or sets the request media type.
        /// </summary>
        public string MediaType { get; set; }

        /// <summary>
        /// Add the authorization header, override to customize
        /// </summary>
        /// <param name="header">Authorization header</param>
        /// <returns>Authorization value to add to the header</returns>
        public virtual AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            string[] split = header.Value.Split();
            return new AuthenticationHeaderValue(split[0], split[1]);
        }

        /// <summary>
        /// Make the call to the API server, override for testing or customization
        /// </summary>
        /// <param name="request">The parameters for the API call</param>
        /// <param name="cancellationToken">Cancel the asynchronous call</param>
        /// <returns>Response object</returns>
        public virtual async Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await this.client.SendAsync(request, cancellationToken).ConfigureAwait(false);
            return new Response(response.StatusCode, response.Content, response.Headers);
        }

        /// <summary>
        /// Prepare for async call to the API server
        /// </summary>
        /// <param name="method">HTTP verb</param>
        /// <param name="requestBody">JSON formatted string</param>
        /// <param name="queryParams">JSON formatted query paramaters</param>
        /// <param name="urlPath">The path to the API endpoint.</param>
        /// <param name="cancellationToken">Cancel the asynchronous call.</param>
        /// <returns>Response object</returns>
        /// <exception cref="Exception">The method will NOT catch and swallow exceptions generated by sending a request
        /// through the internal http client. Any underlying exception will pass right through.
        /// In particular, this means that you may expect
        /// a TimeoutException if you are not connected to the internet.</exception>
        public async Task<Response> RequestAsync(
            SendGridClient.Method method,
            string requestBody = null,
            string queryParams = null,
            string urlPath = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var endpoint = this.client.BaseAddress + this.BuildUrl(urlPath, queryParams);

            // Build the request body
            StringContent content = null;
            if (requestBody != null)
            {
                content = new StringContent(requestBody, Encoding.UTF8, this.MediaType);
            }

            // Build the final request
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method.ToString()),
                RequestUri = new Uri(endpoint),
                Content = content
            };
            return await this.MakeRequest(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Make a request to send an email through SendGrid asynchronously.
        /// </summary>
        /// <param name="msg">A SendGridMessage object with the details for the request.</param>
        /// <param name="cancellationToken">Cancel the asynchronous call.</param>
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
        /// <param name="urlPath">The URL path.</param>
        /// <param name="queryParams">A string of JSON formatted query parameters (e.g {'param': 'param_value'})</param>
        /// <returns>
        /// Final URL
        /// </returns>
        private string BuildUrl(string urlPath, string queryParams = null)
        {
            string url = null;

            // create urlPAth - from parameter if overridden on call or from ctor parameter
            var urlpath = urlPath ?? this.UrlPath;

            if (this.Version != null)
            {
                url = this.Version + "/" + urlpath;
            }
            else
            {
                url = urlpath;
            }

            if (queryParams != null)
            {
                var ds_query_params = ParseJson(queryParams);
                string query = "?";
                foreach (var pair in ds_query_params)
                {
                    foreach(var element in pair.Value) 
                    {
                        if (query != "?")
                        {
                            query = query + "&";
                        }

                        query = query + pair.Key + "=" + element;
                    }
                }

                url = url + query;
            }

            return url;
        }

        private HttpClient CreateHttpClientWithRetryHandler()
        {
            return new HttpClient(new RetryDelegatingHandler(this.options.ReliabilitySettings));
        }

        /// <summary>
        /// Common method to initiate internal fields regardless of which constructor was used.
        /// </summary>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        private void InitiateClient(string apiKey, string host, Dictionary<string, string> requestHeaders, string version, string urlPath)
        {
            this.UrlPath = urlPath;
            this.Version = version;

            var baseAddress = host ?? "https://api.sendgrid.com";
            var clientVersion = this.GetType().GetTypeInfo().Assembly.GetName().Version.ToString();

            // standard headers
            this.client.BaseAddress = new Uri(baseAddress);
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "Authorization", "Bearer " + apiKey },
                { "Content-Type", "application/json" },
                { "User-Agent", "sendgrid/" + clientVersion + " csharp" },
                { "Accept", "application/json" }
            };

            // set header overrides
            if (requestHeaders != null)
            {
                foreach (var header in requestHeaders)
                {
                    headers[header.Key] = header.Value;
                }
            }

            // add headers to httpClient
            foreach (var header in headers)
            {
                if (header.Key == "Authorization")
                {
                    var split = header.Value.Split();
                    this.client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(split[0], split[1]);
                }
                else if (header.Key == "Content-Type")
                {
                    this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                    this.MediaType = header.Value;
                }
                else
                {
                    this.client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Parses a JSON string without removing duplicate keys.
        /// </summary>
        /// <remarks>
        /// This function flattens all Objects/Array.
        /// This means that for example <code>{'id': 1, 'id': 2, 'id': 3}</code> and 
        /// <code>{'id': [1, 2, 3]}</code> result in the same output.
        /// </remarks>
        /// <param name="json">The JSON string to parse.</param>
        /// <returns>A dictionary of all values.</returns>
        private Dictionary<string, List<object>> ParseJson(string json)
        {
            var dict = new Dictionary<string, List<object>>();
            
            using(var sr = new StringReader(json))
            using(var reader = new JsonTextReader(sr))
            {
                var propertyName = "";
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.PropertyName:
                        {
                            propertyName = reader.Value.ToString();
                            if(!dict.ContainsKey(propertyName))
                                dict.Add(propertyName, new List<object>());
                            break;
                        }
                        case JsonToken.Boolean:
                        case JsonToken.Integer:
                        case JsonToken.Float:
                        case JsonToken.Bytes:
                        case JsonToken.String:
                        case JsonToken.Date:
                        {
                            dict[propertyName].Add(reader.Value);
                            break;
                        }
                    }
                }
            }
            return dict;
        }

        /// <summary>
        /// Creates an instance of the wrapper for the Contacts API
        /// </summary>
        /// <returns>An instance of the contacts API wrapper</returns>
        public IContactDbClient CreateContactDbClient()
        {
            return new ContactDbClient(this.client);
        }
    }
}
