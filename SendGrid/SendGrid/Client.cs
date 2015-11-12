using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using SendGrid.Resources;
using System.Web.Script.Serialization;
using System.Net;

namespace SendGrid
{
    public class Client
    {
        private HttpResponseMessage _response = new HttpResponseMessage();
        private string _apiKey;
        private Uri _baseUri;
        public APIKeys ApiKeys;

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="apiKey">Your SendGrid API Key</param>
        /// <param name="baseUri">Base SendGrid API Uri</param>
        public Client(string apiKey, string baseUri = "https://api.sendgrid.com/")
        {
            _baseUri = new Uri(baseUri);
            _apiKey = apiKey;
            ApiKeys = new APIKeys(this);
        }

        /// <summary>
        ///     Create a client that connects to the SendGrid Web API
        /// </summary>
        /// <param name="method">HTTP verb, case-insensitive</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An object representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private async Task RequestAsync(string method, string endpoint, Object data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = _baseUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "sendgrid/" + version + ";csharp");

                    switch (method.ToLower())
                    {
                        case "get":
                            _response = await client.GetAsync(endpoint);
                            break;
                        case "post":
                            _response = await client.PostAsJsonAsync(endpoint, data);
                            break;
                        case "patch":
                            endpoint = _baseUri + endpoint;
                            StringContent content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                            HttpRequestMessage request = new HttpRequestMessage
                            {
                                Method = new HttpMethod("PATCH"),
                                RequestUri = new Uri(endpoint),
                                Content = content
                            };
                            _response = await client.SendAsync(request);
                            break;
                        case "delete":
                            _response = await client.DeleteAsync(endpoint);
                            break;
                        default:
                            _response.StatusCode = HttpStatusCode.MethodNotAllowed;
                            _response.Content = new StringContent("Bad method call: " + method);
                            break;
                    }
                }
                catch (HttpRequestException hre)
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Content = new StringContent(hre.ToString());
                }
                catch (Exception ex)
                {
                    _response.StatusCode = HttpStatusCode.InternalServerError;
                    _response.Content = new StringContent(ex.ToString());
                }
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Get(string endpoint)
        {
            RequestAsync("GET", endpoint, null).Wait();
            return _response;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An object representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Post(string endpoint, object data)
        {
            RequestAsync("POST", endpoint, data).Wait();
            return _response;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Delete(string endpoint)
        {
            RequestAsync("DELETE", endpoint, null).Wait();
            return _response;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An object representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Patch(string endpoint, object data)
        {
            var json = new JavaScriptSerializer().Serialize(data);
            RequestAsync("PATCH", endpoint, json).Wait();
            return _response;
        }

    }
}
