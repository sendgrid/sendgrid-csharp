using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
    public class ApiKeys
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid APIKeys object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/API_Keys/index.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public ApiKeys(Client client, string endpoint = "v3/api_keys")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get an existing API Key
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApiKey> GetAsync(string keyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, keyId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apikey = JObject.Parse(responseContent).ToObject<ApiKey>();
            return apikey;
        }

        /// <summary>
        /// Get all API Keys belonging to the authenticated user
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApiKey[]> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(_endpoint, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Response looks like this:
            // {
            //   "result": [
            //     {
            //       "name": "A New Hope",
            //       "api_key_id": "xxxxxxxx"
            //     }
            //	]
            // }
            // We use a dynamic object to get rid of the 'result' property and simply return an array of api keys
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.result;

            var apikeys = dynamicArray.ToObject<ApiKey[]>();
            return apikeys;
        }

        /// <summary>
        /// Generate a new API Key
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scopes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApiKey> CreateAsync(string name, IEnumerable<string> scopes = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            scopes = (scopes ?? Enumerable.Empty<string>());

            var data = CreateJObjectForApiKey(name, scopes);
            var response = await _client.Post(_endpoint, data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apikey = JObject.Parse(responseContent).ToObject<ApiKey>();
            return apikey;
        }

        /// <summary>
        /// Revoke an existing API Key
        /// </summary>
        /// <param name="campaignId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task DeleteAsync(string keyId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, keyId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        /// <summary>
        /// Update an API Key
        /// </summary>
        /// <param name="name"></param>
        /// <param name="scopes"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ApiKey> UpdateAsync(string keyId, string name = null, IEnumerable<string> scopes = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            scopes = (scopes ?? Enumerable.Empty<string>());

            HttpResponseMessage response = null;
            var data = CreateJObjectForApiKey(name, scopes);
            if (scopes.Any())
            {
                response = await _client.Put(string.Format("{0}/{1}", _endpoint, keyId), data, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                response = await _client.Patch(string.Format("{0}/{1}", _endpoint, keyId), data, cancellationToken).ConfigureAwait(false);
            }
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var apikey = JObject.Parse(responseContent).ToObject<ApiKey>();
            return apikey;
        }

        private static JObject CreateJObjectForApiKey(string name = null, IEnumerable<string> scopes = null)
        {
            var result = new JObject();
            if (!string.IsNullOrEmpty(name)) result.Add("name", name);
            if (scopes != null && scopes.Any()) result.Add("scopes", JArray.FromObject(scopes.ToArray()));
            return result;
        }
    }
}
