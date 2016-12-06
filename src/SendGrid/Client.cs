using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid
{
    public class Response
    {
        public HttpStatusCode StatusCode;
        public HttpContent Body;
        public HttpResponseHeaders Headers;

        /// <summary>
        ///     Holds the response from an API call.
        /// </summary>
        /// <param name="statusCode">https://msdn.microsoft.com/en-us/library/system.net.httpstatuscode(v=vs.110).aspx</param>
        /// <param name="responseBody">https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent(v=vs.118).aspx</param>
        /// <param name="responseHeaders">https://msdn.microsoft.com/en-us/library/system.net.http.headers.httpresponseheaders(v=vs.118).aspx</param>
        public Response(HttpStatusCode statusCode, HttpContent responseBody, HttpResponseHeaders responseHeaders)
        {
            StatusCode = statusCode;
            Body = responseBody;
            Headers = responseHeaders;
        }

        /// <summary>
        ///     Converts string formatted response body to a Dictionary.
        /// </summary>
        /// <param name="content">https://msdn.microsoft.com/en-us/library/system.net.http.httpcontent(v=vs.118).aspx</param>
        /// <returns>Dictionary object representation of HttpContent</returns>
        public virtual Dictionary<string, dynamic> DeserializeResponseBody(HttpContent content)
        {
            var dsContent = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(content.ReadAsStringAsync().Result);
            return dsContent;
        }

        /// <summary>
        ///     Converts string formatted response headers to a Dictionary.
        /// </summary>
        /// <param name="content">https://msdn.microsoft.com/en-us/library/system.net.http.headers.httpresponseheaders(v=vs.118).aspx</param>
        /// <returns>Dictionary object representation of  HttpRepsonseHeaders</returns>
        public virtual Dictionary<string, string> DeserializeResponseHeaders(HttpResponseHeaders content)
        {
            var dsContent = new Dictionary<string, string>();
            foreach (var pair in content)
            {
                dsContent.Add(pair.Key, pair.Value.First());
            }
            return dsContent;
        }
    }

    public class Client
    {
        public string Host;
        public Dictionary<string, string> RequestHeaders;
        public string Version;
        public string UrlPath;
        public string MediaType;
        public IWebProxy WebProxy;
        public enum Methods
        {
            DELETE, GET, PATCH, POST, PUT
        }


        /// <summary>
        ///     REST API client.
        /// </summary>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public Client(IWebProxy webProxy, string apiKey, string host = "https://api.sendgrid.com", Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
            : this(apiKey, host, requestHeaders, version, urlPath)
        {
            WebProxy = webProxy;
        }

        /// <summary>
        ///     REST API client.
        /// </summary>
        /// <param name="host">Base url (e.g. https://api.sendgrid.com)</param>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        /// <param name="version">API version, override AddVersion to customize</param>
        /// <param name="urlPath">Path to endpoint (e.g. /path/to/endpoint)</param>
        /// <returns>Interface to the SendGrid REST API</returns>
        public Client(string apiKey, string host = null, Dictionary<string, string> requestHeaders = null, string version = "v3", string urlPath = null)
        {
            Host = (host != null) ? host : "https://api.sendgrid.com";
            Version = this.GetType().GetTypeInfo().Assembly.GetName().Version.ToString();
            Dictionary<string, string> defaultHeaders = new Dictionary<string, string>();
            defaultHeaders.Add("Authorization", "Bearer " + apiKey);
            defaultHeaders.Add("Content-Type", "application/json");
            defaultHeaders.Add("User-Agent", "sendgrid/" + Version + " csharp");
            defaultHeaders.Add("Accept", "application/json");
            AddRequestHeader(defaultHeaders);
            if (requestHeaders != null)
            {
                AddRequestHeader(requestHeaders);
            }
            SetVersion(version);
            SetUrlPath(urlPath);
        }

        /// <summary>
        ///     Add requestHeaders to the API call
        /// </summary>
        /// <param name="requestHeaders">A dictionary of request headers</param>
        public void AddRequestHeader(Dictionary<string, string> requestHeaders)
        {
            RequestHeaders = (RequestHeaders != null)
                    ? RequestHeaders.Union(requestHeaders).ToDictionary(pair => pair.Key, pair => pair.Value) : requestHeaders;
        }

        /// <summary>
        ///     Set or update the UrlPath
        /// </summary>
        /// <param name="urlPath">The endpoint without a leading or trailing slash</param>
        public void SetUrlPath(string urlPath)
        {
            UrlPath = urlPath;
        }

        /// <summary>
        ///     Get the urlPath to the API endpoint
        /// </summary>
        /// <returns>Url path to the API endpoint</returns>
        public string GetUrlPath()
        {
            return UrlPath;
        }

        /// <summary>
        ///     Build the final URL
        /// </summary>
        /// <param name="queryParams">A string of JSON formatted query parameters (e.g {'param': 'param_value'})</param>
        /// <returns>Final URL</returns>
        private string BuildUrl(string queryParams = null)
        {
            string url = null;

            if (GetVersion() != null)
            {
                url = Host + "/" + GetVersion() + "/" + GetUrlPath();
            }
            else
            {
                url = Host + "/" + GetUrlPath();
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
            if (WebProxy != null)
            {
                var httpClientHandler = new HttpClientHandler()
                {
                    Proxy = WebProxy,
                    PreAuthenticate = true,
                    UseDefaultCredentials = false,
                };

                return new HttpClient(httpClientHandler);
            }

            return new HttpClient();
        }

        /// <summary>
        ///     Add the authorization header, override to customize
        /// </summary>
        /// <param name="header">Authorization header</param>
        /// <returns>Authorization value to add to the header</returns>
        public virtual AuthenticationHeaderValue AddAuthorization(KeyValuePair<string, string> header)
        {
            string[] split = header.Value.Split(new char[0]);
            return new AuthenticationHeaderValue(split[0], split[1]);
        }

        /// <summary>
        ///     Add the version of the API, override to customize
        /// </summary>
        /// <param name="version">Version string to add to the URL</param>
        public void SetVersion(string version)
        {
            Version = version;
        }

        /// <summary>
        ///     Get the version of the API, override to customize
        /// </summary>
        /// <returns>Version of the API</param>
        public string GetVersion()
        {
            return Version;
        }

        /// <summary>
        ///     Make the call to the API server, override for testing or customization
        /// </summary>
        /// <param name="client">Client object ready for communication with API</param>
        /// <param name="request">The parameters for the API call</param>
        /// <returns>Response object</returns>
        public async virtual Task<Response> MakeRequest(HttpClient client, HttpRequestMessage request)
        {
            HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            return new Response(response.StatusCode, response.Content, response.Headers);
        }

        /// <summary>
        ///     Prepare for async call to the API server
        /// </summary>
        /// <param name="method">HTTP verb</param>
        /// <param name="requestBody">JSON formatted string</param>
        /// <param name="queryParams">JSON formatted query paramaters</param>
        /// <returns>Response object</returns>
        public async Task<Response> RequestAsync(Client.Methods method,
                                                 string requestBody = null,
                                                 Dictionary<string, string> requestHeaders = null,
                                                 string queryParams = null,
                                                 string urlPath = null)
        {
            using (var client = BuildHttpClient())
            {
                try
                {
                    // Build the URL
                    client.BaseAddress = new Uri(Host);
                    if (urlPath != null)
                    {
                        SetUrlPath(urlPath);
                    }
                    string endpoint = BuildUrl(queryParams);

                    // Build the request headers
                    if (requestHeaders != null)
                    {
                        AddRequestHeader(requestHeaders);
                    }
                    client.DefaultRequestHeaders.Accept.Clear();
                    if (RequestHeaders != null)
                    {
                        foreach (KeyValuePair<string, string> header in RequestHeaders)
                        {
                            if (header.Key == "Authorization")
                            {
                                client.DefaultRequestHeaders.Authorization = AddAuthorization(header);
                            }
                            else if (header.Key == "Content-Type")
                            {
                                MediaType = header.Value;
                                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
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
                        content = new StringContent(requestBody, Encoding.UTF8, MediaType);
                    }

                    // Build the final request
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = new HttpMethod(method.ToString()),
                        RequestUri = new Uri(endpoint),
                        Content = content
                    };
                    return await MakeRequest(client, request).ConfigureAwait(false);

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
    }
}
