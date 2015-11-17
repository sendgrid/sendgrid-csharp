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
        private Uri _baseUri;

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
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>An asyncronous task</returns>
        private async Task<HttpResponseMessage> RequestAsync(string method, string endpoint, JObject data)
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
                            return await client.GetAsync(endpoint);
                        case "post":
                            return await client.PostAsJsonAsync(endpoint, data);
                        case "patch":
                            endpoint = _baseUri + endpoint;
                            StringContent content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                            HttpRequestMessage request = new HttpRequestMessage
                            {
                                Method = new HttpMethod("PATCH"),
                                RequestUri = new Uri(endpoint),
                                Content = content
                            };
                            return await client.SendAsync(request);
                        case "delete":
                            return await client.DeleteAsync(endpoint);
                        default:
                            HttpResponseMessage response = new HttpResponseMessage();
                            response.StatusCode = HttpStatusCode.MethodNotAllowed;
                            response.Content = new StringContent("Bad method call: " + method);
                            return response;
                    }
                }
                catch (HttpRequestException hre)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.Content = new StringContent(hre.Message);
                    return response;
                }
                catch (Exception ex)
                {
                    HttpResponseMessage response = new HttpResponseMessage();
                    response.Content = new StringContent(ex.Message);
                    return response;
                }
            }
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Get(string endpoint)
        {
            return RequestAsync("GET", endpoint, null).Result;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Post(string endpoint, JObject data)
        {
            return RequestAsync("POST", endpoint, data).Result;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Delete(string endpoint)
        {
            return RequestAsync("DELETE", endpoint, null).Result;
        }

        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        /// <param name="data">An JObject representing the resource's data</param>
        /// <returns>The resulting message from the API call</returns>
        public HttpResponseMessage Patch(string endpoint, JObject data)
        {
            return RequestAsync("PATCH", endpoint, data).Result;
        }
    }
}
