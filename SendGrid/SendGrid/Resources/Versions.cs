using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class Versions
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Versions object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/versions.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Versions(Client client, string endpoint = "v3/templates")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get information on a single template version.
        /// </summary>
        /// <param name="templateId">ID of the template to fetch</param>
        /// <param name="versionId">ID of the template version to fetch</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/versions.html</returns>
        public async Task<HttpResponseMessage> Get(string templateId, string versionId)
        {
            return await _client.Get(_endpoint + "/" + templateId + "/versions/" + versionId);
        }

        /// <summary>
        /// Create a new template version.
        /// </summary>
        /// <param name="templateId">ID of the template to add the version to</param>
        /// <param name="versionName">The name of the new version</param>
        /// <param name="subject">The subject of the new version</param>
        /// <param name="htmlContent">The HTML content of the new version</param>
        /// <param name="plainContent">The Text/plain content of the new version</param>
        /// <param name="active">Set this version as active</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/versions.html</returns>
        public async Task<HttpResponseMessage> Post(string templateId, string versionName, string subject, string htmlContent, string plainContent, bool active = false)
        {
            var data = new JObject {{"name", versionName},
                                    {"subject", subject},
                                    {"html_content", htmlContent},
                                    {"plain_content", plainContent},
                                    {"active", active ? 1 : 0}};
            return await _client.Post(_endpoint + "/" + templateId + "/versions", data);
        }

        /// <summary>
        /// Patch a template version.
        /// </summary>
        /// <param name="templateId">ID of the template to add the version to</param>
        /// <param name="versionId">ID of the template version to update</param>
        /// <param name="versionName">Updated name of the template</param>
        /// <param name="subject">Updated subject of the new version</param>
        /// <param name="htmlContent">The HTML content of the new version</param>
        /// <param name="plainContent">The Text/plain content of the new version</param>
        /// <param name="active">Set this version as active</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/versions.html</returns>
        public async Task<HttpResponseMessage> Patch(string templateId, string versionId, string versionName, string subject, string htmlContent, string plainContent, bool active = false)
        {
            var data = new JObject {{"name", versionName},
                                    {"subject", subject},
                                    {"html_content", htmlContent},
                                    {"plain_content", plainContent},
                                    {"active", active}};
            return await _client.Patch(_endpoint + "/" + templateId + "/versions/" + versionId, data);
        }

        /// <summary>
        /// Delete a template version.
        /// </summary>
        /// <param name="templateId">ID of the template to delete</param>
        /// <param name="versionId">ID of the template version to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Transactional_Templates/versions.html</returns>
        public async Task<HttpResponseMessage> Delete(string templateId, string versionId)
        {
            return await _client.Delete(_endpoint + "/" + templateId + "/versions/" + versionId);
        }

    }
}