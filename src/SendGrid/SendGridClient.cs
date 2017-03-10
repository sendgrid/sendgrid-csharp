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
        private readonly string version;
        //private readonly string urlPath;
        private readonly string mediaType;
        private HttpClient client;


        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="webProxy">Web proxy.</param>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(IWebProxy webProxy, string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3")
        {
            this.version = version;

            var baseAddress = host ?? "https://api.sendgrid.com";
            var clientVersion = GetType().GetTypeInfo().Assembly.GetName().Version.ToString();


            //var servicePoint = ServicePointManager.FindServicePoint()

            // Add the WebProxy if set
            if (webProxy != null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = webProxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };
                client = new HttpClient(httpClientHandler);
            }
            else
            {
                client = new HttpClient(); 
            }

            // standard headers
            client.BaseAddress = new Uri(baseAddress);
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                {"Authorization", "Bearer " + apiKey},
                {"Content-Type", "application/json"},
                {"User-Agent", "sendgrid/" + clientVersion + " csharp"},
                {"Accept", "application/json"}
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
                    var split = header.Value.Split(new char[0]);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(split[0], split[1]);
                }
                else if (header.Key == "Content-Type")
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(header.Value));
                    this.mediaType = header.Value;
                }
                else
                {
                    client.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendGridClient"/> class.
        /// </summary>
        /// <param name="apiKey">Your SendGrid API key.</param>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public SendGridClient(string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3")  
            : this(null, apiKey, host, requestHeaders, version)
        {
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
        /// Make the call to the API server, override for testing or customization
        /// </summary>
        /// <param name="request">The parameters for the API call</param>
        /// <param name="cancellationToken">Cancel the asynchronous call</param>
        /// <returns>Response object</returns>
        private async Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await client.SendAsync(request, cancellationToken);
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
        public async Task<Response> RequestAsync(
                                                 SendGridClient.Method method,
                                                 string requestBody = null,
                                                 string queryParams = null,
                                                 string urlPath = null,
                                                 CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                var endpoint = BuildUrl(urlPath, queryParams);
                // Build the request body
                StringContent content = null;
                if (requestBody != null)
                {
                    content = new StringContent(requestBody, Encoding.UTF8, this.mediaType);
                }

                // Build the final request
                var request = new HttpRequestMessage
                {
                    Method = new HttpMethod(method.ToString()),
                    RequestUri = new Uri(endpoint),
                    Content = content
                };
                return await MakeRequest(request, cancellationToken);
            }
            catch (Exception ex)
            {
                    var response = new HttpResponseMessage();
                    var message = (ex is HttpRequestException) ? ".NET HttpRequestException" : ".NET Exception";
                    response.Content = new StringContent(message + ", raw message: \n\n" + ex.Message);
                    return new Response(response.StatusCode, response.Content, response.Headers);
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
            return await this.RequestAsync(Method.POST,
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

            if (version != null)
            {
                url = "/" + version + "/" + urlPath;
            }
            else
            {
                url = "/" + urlPath;
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
    }
}
