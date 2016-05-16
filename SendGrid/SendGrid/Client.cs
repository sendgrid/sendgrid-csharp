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
        private readonly string _apiKey;
        private readonly Uri _baseUri;
        private const string MediaType = "application/json";

        private enum Methods
        {
            GET,
            POST,
            PATCH,
            DELETE
        }

        public readonly APIKeys ApiKeys;
        public readonly UnsubscribeGroups UnsubscribeGroups;
        public readonly Suppressions Suppressions;
        public readonly GlobalSuppressions GlobalSuppressions;
        public readonly GlobalStats GlobalStats;
        public readonly string Version;

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
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent",
                        string.Format("sendgrid/{0};csharp", Version));

                    switch (method)
                    {
                        case Methods.GET:
                            return await client.GetAsync(endpoint);

                        case Methods.POST:
                            return await client.PostAsJsonAsync(endpoint, data);

                        case Methods.PATCH:
                            var request = new HttpRequestMessage
                            {
                                Method = new HttpMethod("PATCH"),
                                RequestUri = new Uri(string.Format("{0}{1}", _baseUri, endpoint)),
                                Content = new StringContent(data.ToString(), Encoding.UTF8, MediaType)
                            };

                            return await client.SendAsync(request);

                        case Methods.DELETE:
                            return await client.DeleteAsync(endpoint);

                        default:
                            return new HttpResponseMessage
                            {
                                StatusCode = HttpStatusCode.MethodNotAllowed,
                                Content =
                                    new StringContent(
                                        "{\"errors\":[{\"message\":\"Bad method call, supported methods are GET, POST, PATCH and DELETE\"}]}")
                            };
                    }
                }
                catch (Exception ex)
                {
                    return new HttpResponseMessage
                    {
                        Content = new StringContent(string.Format("{0}, raw message: \n\n{1}",
                            ex is HttpRequestException ? ".NET HttpRequestException" : ".NET Exception", ex.Message))
                    };
                }
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Get(string endpoint)
        {
            return await RequestAsync(Methods.GET, endpoint, null);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Post(string endpoint, JObject data)
        {
            return await RequestAsync(Methods.POST, endpoint, data);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public async Task<HttpResponseMessage> Delete(string endpoint)
        {
            return await RequestAsync(Methods.DELETE, endpoint, null);
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