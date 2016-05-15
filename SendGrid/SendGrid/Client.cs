using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using SendGrid.Resources;
using System.Net;
using Newtonsoft.Json.Linq;

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
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = _baseUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "sendgrid/" + Version + ";csharp");

                    switch (method)
                    {
                        case Methods.GET:
                            return await client.GetAsync(endpoint).ConfigureAwait(false);
                        case Methods.POST:
                            return await client.PostAsJsonAsync(endpoint, data).ConfigureAwait(false);
                        case Methods.PATCH:
                            endpoint = _baseUri + endpoint;
                            StringContent content = new StringContent(data.ToString(), Encoding.UTF8, MediaType);
                            HttpRequestMessage request = new HttpRequestMessage
                            {
                                Method = new HttpMethod("PATCH"),
                                RequestUri = new Uri(endpoint),
                                Content = content
                            };
                            return await client.SendAsync(request).ConfigureAwait(false);
                        case Methods.DELETE:
                            return await client.DeleteAsync(endpoint).ConfigureAwait(false);
                        default:
                            HttpResponseMessage response = new HttpResponseMessage();
                            response.StatusCode = HttpStatusCode.MethodNotAllowed;
                            var message = "{\"errors\":[{\"message\":\"Bad method call, supported methods are GET, POST, PATCH and DELETE\"}]}";
                            response.Content = new StringContent(message);
                            return response;
                    }
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    string message;
                    message = (ex is HttpRequestException) ? ".NET HttpRequestException" : ".NET Exception";
                    message = message + ", raw message: \n\n";
                    response.Content = new StringContent(message + ex.Message);
                    return response;
                }
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Get(string endpoint)
        {
            return RequestAsync(Methods.GET, endpoint, null);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Post(string endpoint, JObject data)
        {
            return RequestAsync(Methods.POST, endpoint, data);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Delete(string endpoint)
        {
            return RequestAsync(Methods.DELETE, endpoint, null);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Patch(string endpoint, JObject data)
        {
            return RequestAsync(Methods.PATCH, endpoint, data);
        }
    }
}
