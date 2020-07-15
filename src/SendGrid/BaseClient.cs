using Newtonsoft.Json;
using SendGrid.Helpers.Errors;
using SendGrid.Helpers.Mail;
using SendGrid.Helpers.Reliability;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#if NET40
using SendGrid.Helpers.Net40;
#else
using System.Reflection;
#endif

namespace SendGrid
{
    /// <summary>
    /// The base interface for interacting with Twilio SendGrid's API.
    /// </summary>
    public abstract class BaseClient : ISendGridClient
    {
        private const string ContentType = "Content-Type";
        private const string DefaultMediaType = "application/json";

        /// <summary>
        /// The client assembly version to send in request User-Agent header.
        /// </summary>
        private static readonly string ClientVersion = typeof(BaseClient).GetTypeInfo().Assembly.GetName().Version.ToString();

        /// <summary>
        /// The configuration to use with current client instance.
        /// </summary>
        private readonly BaseClientOptions options;

        /// <summary>
        /// The HttpClient instance to use for all calls from this client instance.
        /// </summary>
        private readonly HttpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        protected BaseClient(BaseClientOptions options)
            : this(httpClient: null, options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        /// <param name="webProxy">Web proxy.</param>
        /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        protected BaseClient(IWebProxy webProxy, BaseClientOptions options)
            : this(CreateHttpClientWithWebProxy(webProxy, options), options)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseClient"/> class.
        /// </summary>
        /// <param name="httpClient">An optional HTTP client which may me injected in order to facilitate testing.</param>
        /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>Interface to the Twilio SendGrid REST API.</returns>
        protected BaseClient(HttpClient httpClient, BaseClientOptions options)
        {
            this.options = options ?? throw new ArgumentNullException(nameof(options));

            this.client = httpClient ?? CreateHttpClientWithRetryHandler(options);
            if (this.options.RequestHeaders != null && this.options.RequestHeaders.TryGetValue(ContentType, out var contentType))
            {
                this.MediaType = contentType;
            }
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
            /// Create a resource or execute a function (e.g. send an email).
            /// </summary>
            POST,

            /// <summary>
            /// Update an entire resource.
            /// </summary>
            PUT,
        }

        /// <summary>
        /// The path to the API resource.
        /// </summary>
        public string UrlPath
        {
            get => this.options.UrlPath;
            set => this.options.UrlPath = value;
        }

        /// <summary>
        /// The API version.
        /// </summary>
        public string Version
        {
            get => this.options.Version;
            set => this.options.Version = value;
        }

        /// <summary>
        /// The request media type.
        /// </summary>
        public string MediaType { get; set; } = DefaultMediaType;

        /// <summary>
        /// Add the authorization header, override to customize.
        /// </summary>
        /// <param name="header">Authorization header.</param>
        /// <returns>Authorization value to add to the header.</returns>
        public virtual AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            string[] split = header.Value.Split();
            return new AuthenticationHeaderValue(split[0], split[1]);
        }

        /// <summary>
        /// Make the call to the API server, override for testing or customization.
        /// </summary>
        /// <param name="request">The parameters for the API call.</param>
        /// <param name="cancellationToken">Cancel the asynchronous call.</param>
        /// <returns>Response object.</returns>
        public virtual async Task<Response> MakeRequest(HttpRequestMessage request, CancellationToken cancellationToken = default(CancellationToken))
        {
            HttpResponseMessage response = await this.client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode && this.options.HttpErrorAsException)
            {
                await ErrorHandler.ThrowException(response).ConfigureAwait(false);
            }

            return new Response(response.StatusCode, response.Content, response.Headers);
        }

        /// <summary>
        /// Prepare for async call to the API server.
        /// </summary>
        /// <param name="method">HTTP verb.</param>
        /// <param name="requestBody">JSON formatted string.</param>
        /// <param name="queryParams">JSON formatted query parameters.</param>
        /// <param name="urlPath">The path to the API endpoint.</param>
        /// <param name="cancellationToken">Cancel the asynchronous call.</param>
        /// <returns>Response object.</returns>
        /// <exception cref="Exception">The method will NOT catch and swallow exceptions generated by sending a request through the internal HTTP client. Any underlying exception will pass right through.
        /// In particular, this means that you may expect a TimeoutException if you are not connected to the Internet.</exception>
        public async Task<Response> RequestAsync(
            SendGridClient.Method method,
            string requestBody = null,
            string queryParams = null,
            string urlPath = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var baseAddress = new Uri(this.options.Host);
            if (!baseAddress.OriginalString.EndsWith("/"))
            {
                baseAddress = new Uri(baseAddress.OriginalString + "/");
            }

            // Build the final request
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(method.ToString()),
                RequestUri = new Uri(baseAddress, this.BuildUrl(urlPath, queryParams)),
                Content = requestBody == null ? null : new StringContent(requestBody, Encoding.UTF8, this.MediaType),
            };

            // Drop the default UTF-8 content type charset for JSON payloads since some APIs may not accept it.
            if (request.Content != null && this.MediaType == DefaultMediaType)
            {
                request.Content.Headers.ContentType.CharSet = null;
            }

            // set header overrides
            if (this.options.RequestHeaders?.Count > 0)
            {
                foreach (var header in this.options.RequestHeaders)
                {
                    request.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            // set standard headers
            request.Headers.Authorization = this.options.Auth;
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(this.MediaType));
            request.Headers.UserAgent.TryParseAdd($"sendgrid/{ClientVersion} csharp");
            return await this.MakeRequest(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Make a request to send an email through Twilio SendGrid asynchronously.
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

        private static HttpClient CreateHttpClientWithRetryHandler(BaseClientOptions options)
        {
            return new HttpClient(new RetryDelegatingHandler(options.ReliabilitySettings));
        }

        /// <summary>
        /// Create client with WebProxy if set.
        /// </summary>
        /// <param name="webProxy">the WebProxy.</param>
        /// <param name="options">A <see cref="BaseClientOptions"/> instance that defines the configuration settings to use with the client.</param>
        /// <returns>HttpClient with RetryDelegatingHandler and WebProxy if set.</returns>
        private static HttpClient CreateHttpClientWithWebProxy(IWebProxy webProxy, BaseClientOptions options)
        {
            if (webProxy != null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = webProxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };

                var retryHandler = new RetryDelegatingHandler(httpClientHandler, options.ReliabilitySettings);

                return new HttpClient(retryHandler);
            }
            else
            {
                return CreateHttpClientWithRetryHandler(options);
            }
        }

        /// <summary>
        /// Build the final URL.
        /// </summary>
        /// <param name="urlPath">The URL path.</param>
        /// <param name="queryParams">A string of JSON formatted query parameters (e.g. {'param': 'param_value'}).</param>
        /// <returns>Final URL.</returns>
        private string BuildUrl(string urlPath, string queryParams = null)
        {
            string url = null;

            // create urlPAth - from parameter if overridden on call or from constructor parameter
            var urlpath = urlPath ?? this.options.UrlPath;

            if (this.options.Version != null)
            {
                url = this.options.Version + "/" + urlpath;
            }
            else
            {
                url = urlpath;
            }

            if (queryParams != null)
            {
                var ds_query_params = this.ParseJson(queryParams);
                string query = "?";
                foreach (var pair in ds_query_params)
                {
                    foreach (var element in pair.Value)
                    {
                        if (query != "?")
                        {
                            query += "&";
                        }

                        query = query + pair.Key + "=" + element;
                    }
                }

                url += query;
            }

            return url;
        }

        /// <summary>
        /// Parses a JSON string without removing duplicate keys.
        /// </summary>
        /// <remarks>
        /// This function flattens all Objects/Array.
        /// This means that for example {'id': 1, 'id': 2, 'id': 3} and {'id': [1, 2, 3]} result in the same output.
        /// </remarks>
        /// <param name="json">The JSON string to parse.</param>
        /// <returns>A dictionary of all values.</returns>
        private Dictionary<string, List<object>> ParseJson(string json)
        {
            var dict = new Dictionary<string, List<object>>();

            using (var sr = new StringReader(json))
            using (var reader = new JsonTextReader(sr))
            {
                var propertyName = string.Empty;
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonToken.PropertyName:
                            {
                                propertyName = reader.Value.ToString();
                                if (!dict.ContainsKey(propertyName))
                                {
                                    dict.Add(propertyName, new List<object>());
                                }

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
    }
}
