using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Text;
using SendGrid.Resources;
using System.Web.Script.Serialization;

namespace SendGrid
{
    public class Client
    {
        private HttpResponseMessage _response;
        private String _apiKey;
        public Uri BaseUri;
        public APIKeys ApiKeys;

        public Client(String api_key)
        {
            BaseUri = new Uri("https://api.sendgrid.com/");
            _apiKey = api_key;
            ApiKeys = new APIKeys(this);
        }

        async Task RequestAsync(String method, String endpoint, Object data)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    client.BaseAddress = BaseUri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
                    var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "sendgrid/" + version + ";csharp");
                    StringContent content;

                    switch (method.ToLower())
                    {
                        case "get":
                            _response = await client.GetAsync(endpoint);
                            break;
                        case "post":
                            content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
                            _response = await client.PostAsJsonAsync(endpoint, data);
                            break;
                        case "patch":
                            endpoint = BaseUri + endpoint.Remove(0, 1);
                            content = new StringContent(data.ToString(), Encoding.UTF8, "application/json");
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
                            _response = null;
                            break;
                    }

                    if (_response == null)
                    {
                        //Console.WriteLine("Bad method call: " + method);
                        //Console.ReadKey();
                    }
                    string responseBodyAsText = await _response.Content.ReadAsStringAsync();
                    responseBodyAsText = responseBodyAsText.Replace("<br>", Environment.NewLine);
                    if (_response.IsSuccessStatusCode)
                    {
                        //Console.WriteLine(_response.StatusCode);
                        //Console.WriteLine(responseBodyAsText);
                        //Console.ReadKey();
                    }
                    else
                    {
                        //Console.WriteLine(responseBodyAsText);
                        //Console.ReadKey();
                    }
                }
                catch (HttpRequestException hre)
                {
                    //Console.WriteLine(hre.ToString());
                    //Console.ReadKey();
                }
                catch (Exception ex)
                {
                    //Console.WriteLine(ex.ToString());
                    //Console.ReadKey();
                }
            }
        }

        public HttpResponseMessage Get(String endpoint)
        {
            RequestAsync("GET", endpoint, null).Wait();
            return _response;
        }

        public HttpResponseMessage Post(String endpoint, object data)
        {
            RequestAsync("POST", endpoint, data).Wait();
            return _response;
        }

        public HttpResponseMessage Delete(String endpoint)
        {
            RequestAsync("DELETE", endpoint, null).Wait();
            return _response;
        }

        public HttpResponseMessage Patch(String endpoint, object data)
        {
            var json = new JavaScriptSerializer().Serialize(data);
            RequestAsync("PATCH", endpoint, json).Wait();
            return _response;
        }

    }
}
