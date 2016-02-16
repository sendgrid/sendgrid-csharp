using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class Templates
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Templates object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Templates(Client client, string endpoint = "v3/templates")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Retrieve all templates associated with the user.
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html</returns>
        public async Task<HttpResponseMessage> Get()
        {
            return await _client.Get(_endpoint);
        }

        /// <summary>
        /// Get information on a single template.
        /// </summary>
        /// <param name="templateId">ID of the template to fetch</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html</returns>
        public async Task<HttpResponseMessage> Get(string templateId)
        {
            return await _client.Get(_endpoint + "/" + templateId);
        }

        /// <summary>
        /// Create a new template.
        /// </summary>
        /// <param name="templateName">The name of the new template</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html</returns>
        public async Task<HttpResponseMessage> Post(string templateName)
        {
            var data = new JObject {{"name", templateName}};
            return await _client.Post(_endpoint, data);
        }

        /// <summary>
        /// Patch a template.
        /// </summary>
        /// <param name="templateName">The new name of the template</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html</returns>
        public async Task<HttpResponseMessage> Patch(string templateId, string templateName)
        {
            var data = new JObject {{"name", templateName}};
            return await _client.Patch(_endpoint + "/" + templateId, data);
        }

        /// <summary>
        /// Delete a template.
        /// </summary>
        /// <param name="unsubscribeGroupId">ID of the template to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/templates.html</returns>
        public async Task<HttpResponseMessage> Delete(string unsubscribeGroupId)
        {
            return await _client.Delete(_endpoint + "/" + unsubscribeGroupId);
        }

    }
}