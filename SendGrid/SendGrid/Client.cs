using Newtonsoft.Json.Linq;
using SendGrid.Resources;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid
{
    public class Client
    {
        private string _apiKey;
        private Uri _baseUri;
        private HttpClient _httpClient;
        private const string MediaType = "application/json";
        private enum Methods
        {
            GET, POST, PATCH, DELETE
        }

        public APIKeys ApiKeys { get; private set; }
        public UnsubscribeGroups UnsubscribeGroups { get; private set; }
        public Suppressions Suppressions { get; private set; }
        public GlobalSuppressions GlobalSuppressions { get; private set; }
        public GlobalStats GlobalStats { get; private set; }
        public Bounces Bounces { get; private set; }
        public User User { get; private set; }
        public CustomFields CustomFields { get; private set; }
        public Contacts Contacts { get; private set; }
        public Lists Lists { get; private set; }
        public Segments Segments { get; private set; }
        public Templates Templates { get; private set; }
        public Categories Categories { get; private set; }
        public string Version { get; private set; }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
        public Client(string apiKey, string baseUri = "https://api.sendgrid.com/", HttpClient httpClient = null)
        {
            _baseUri = new Uri(baseUri);
            _apiKey = apiKey;

            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            ApiKeys = new APIKeys(this);
            UnsubscribeGroups = new UnsubscribeGroups(this);
            Suppressions = new Suppressions(this);
            GlobalSuppressions = new GlobalSuppressions(this);
            GlobalStats = new GlobalStats(this);
            User = new User(this);
            Contacts = new Contacts(this);
            Bounces = new Bounces(this);
            CustomFields = new CustomFields(this);
            Lists = new Lists(this);
            Segments = new Segments(this);
            Templates = new Templates(this);
            Categories = new Categories(this);

            _httpClient = httpClient ?? new HttpClient();
            _httpClient.BaseAddress = _baseUri;
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", string.Format("sendgrid/{0};csharp", Version));
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, JObject data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = (data == null ? null : new StringContent(data.ToString(), Encoding.UTF8, MediaType));
            return RequestAsync(method, endpoint, content, cancellationToken);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JArray representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, JArray data, CancellationToken cancellationToken = default(CancellationToken))
        {
            var content = (data == null ? null : new StringContent(data.ToString(), Encoding.UTF8, MediaType));
            return RequestAsync(method, endpoint, content, cancellationToken);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="content">A StringContent representing the content of the http request</param>
        /// <returns>An asyncronous task</returns>
        private async Task<HttpResponseMessage> RequestAsync(Methods method, string endpoint, StringContent content, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
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

                var httpRequest = new HttpRequestMessage
                {
                    Method = new HttpMethod(methodAsString),
                    RequestUri = new Uri(_baseUri + endpoint),
                    Content = content
                };
                return await _httpClient.SendAsync(httpRequest, cancellationToken);
            }
            catch (Exception ex)
            {
                var message = string.Format(".NET {0}, raw message: \n\n{1}", (ex is HttpRequestException) ? "HttpRequestException" : "Exception", ex.GetBaseException().Message);
                var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(message)
                };
                return response;
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Get(string endpoint, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.GET, endpoint, (StringContent)null, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Post(string endpoint, JObject data, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.POST, endpoint, data, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JArray representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Post(string endpoint, JArray data, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.POST, endpoint, data, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Delete(string endpoint, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.DELETE, endpoint, (StringContent)null, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An optional JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Delete(string endpoint, JObject data = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.DELETE, endpoint, data, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An optional JArray representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Delete(string endpoint, JArray data = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.DELETE, endpoint, data, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Patch(string endpoint, JObject data, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.PATCH, endpoint, data, cancellationToken);
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JArray representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public Task<HttpResponseMessage> Patch(string endpoint, JArray data, CancellationToken cancellationToken = default(CancellationToken))
        {
            return RequestAsync(Methods.PATCH, endpoint, data, cancellationToken);
        }
    }
}
