using Newtonsoft.Json.Linq;
using SendGrid.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SendGrid
{
    public class Client
    {
        private string _apiKey;
        public APIKeys ApiKeys;
        public UnsubscribeGroups UnsubscribeGroups;
        public Suppressions Suppressions;
        public GlobalSuppressions GlobalSuppressions;
        public GlobalStats GlobalStats;
        public string Version;
        private Uri _baseUri;
        private const string MediaType = "application/json";
        private enum Methods
        {
            GET, POST, PATCH, DELETE
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
        public Client(string apiKey, string baseUri = "https://api.sendgrid.com/")
        {
            _baseUri = new Uri(baseUri);
            _apiKey = apiKey;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ApiKeys = new APIKeys(this);
            UnsubscribeGroups = new UnsubscribeGroups(this);
            Suppressions = new Suppressions(this);
            GlobalSuppressions = new GlobalSuppressions(this);
            GlobalStats = new GlobalStats(this);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private async Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, JObject data)
        {
            var content = (data == null ? null : new StringContent(data.ToString(), Encoding.UTF8, MediaType));
            return await RequestAsync(method, endpoint, content);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JArray representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private async Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, JArray data)
        {
            var content = (data == null ? null : new StringContent(data.ToString(), Encoding.UTF8, MediaType));
            return await RequestAsync(method, endpoint, content);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">A StringContent representing the content of the http request</param>
        /// <returns>An asyncronous task</returns>
        private async Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, StringContent content)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = _baseUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "sendgrid/" + Version + ";csharp");

                    var methodAsString = "";
                    switch (method)
                    {
                        case Methods.GET: methodAsString = "GET"; break;
                        case Methods.POST: methodAsString = "POST"; break;
                        case Methods.PATCH: methodAsString = "PATCH"; break;
                        case Methods.DELETE: methodAsString = "DELETE"; break;
                        default:
                            var message = "{\"errors\":[{\"message\":\"Bad method call, supported methods are GET, POST, PATCH and DELETE\"}]}";
                            var response = new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)
                            {
                                Content = new StringContent(message)
                            };
                            return response;
                    }

                    var postRequest = new HttpRequestMessage()
                    {
                        Method = new HttpMethod(methodAsString),
                        RequestUri = new Uri(_baseUri + endpoint),
                        Content = content
                    };
                    return await client.SendAsync(postRequest);

                }
                catch (Exception ex)
                {
                    var message = string.Format(".NET {0}, raw message: \n\n{1}", (ex is HttpRequestException) ? "HttpRequestException" : "Exception", ex.Message);
                    var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        Content = new StringContent(message)
                    };
                    return response;
                }
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Get(string endpoint)
        {
            return await RequestAsync(Methods.GET, endpoint, (StringContent)null);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Post(string endpoint, JObject data)
        {
            return await RequestAsync(Methods.POST, endpoint, data);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An optional JArray representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Delete(string endpoint, JArray data = null)
        {
            return await RequestAsync(Methods.DELETE, endpoint, data);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Patch(string endpoint, JObject data)
        {
            return await RequestAsync(Methods.PATCH, endpoint, data);
        }
    }
}
