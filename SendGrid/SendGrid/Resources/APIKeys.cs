using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class APIKeys
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid APIKeys object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public APIKeys(Client client, string endpoint = "v3/api_keys")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get a list of active API Keys
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html</returns>
        public Task<HttpResponseMessage> Get()
        {
            return _client.Get(_endpoint);
        }

        /// <summary>
        /// Create a new API key
        /// </summary>
        /// <param name="apiKeyName">Name of the new API Key</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html</returns>
        public Task<HttpResponseMessage> Post(string apiKeyName)
        {
            var data = new JObject {{"name", apiKeyName}};
            return _client.Post(_endpoint, data);
        }

        /// <summary>
        /// Delete a API key
        /// </summary>
        /// <param name="apiKeyId">ID of the API Key to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html</returns>
        public Task<HttpResponseMessage> Delete(string apiKeyId)
        {
            return _client.Delete(_endpoint + "/" + apiKeyId);
        }

        /// <summary>
        /// Patch a API key
        /// </summary>
        /// <param name="apiKeyId">ID of the API Key to rename</param>
        /// <param name="apiKeyName">New API Key name</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html</returns>
        public Task<HttpResponseMessage> Patch(string apiKeyId, string apiKeyName)
        {
            var data = new JObject { { "name", apiKeyName } };
            return _client.Patch(_endpoint + "/" + apiKeyId, data);
        }

    }
}