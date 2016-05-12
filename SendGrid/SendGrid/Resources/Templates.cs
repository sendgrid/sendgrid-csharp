using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
    public class Templates
    {
        private readonly string _endpoint;
        private readonly Client _client;

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

        public async Task<Template> CreateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject
            {
                { "name", name }
            };
            var response = await _client.Post(_endpoint, data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var template = JObject.Parse(responseContent).ToObject<Template>();
            return template;
        }

        public async Task<Template[]> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(_endpoint, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Response looks like this:
            // {
            //   "templates": [
            //     {
            //       "id": "e8ac01d5-a07a-4a71-b14c-4721136fe6aa",
            //       "name": "example template name",
            //       "versions": [
            //         {
            //           "id": "5997fcf6-2b9f-484d-acd5-7e9a99f0dc1f",
            //           "template_id": "9c59c1fb-931a-40fc-a658-50f871f3e41c",
            //           "active": 1,
            //           "name": "example version name",
            //           "updated_at": "2014-03-19 18:56:33"
            //         }
            //       ]
            //     }
            //   ]
            // }
            // We use a dynamic object to get rid of the 'templates' property and simply return an array of templates
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.templates;

            var templates = dynamicArray.ToObject<Template[]>();
            return templates;
        }

        public async Task<Template> GetAsync(string templateId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, templateId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var template = JObject.Parse(responseContent).ToObject<Template>();
            return template;
        }

        public async Task<Template> UpdateAsync(string templateId, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject 
            {
                { "name", name }
            };
            var response = await _client.Patch(string.Format("{0}/{1}", _endpoint, templateId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var template = JObject.Parse(responseContent).ToObject<Template>();
            return template;
        }

        public async Task DeleteAsync(string templateId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, templateId), cancellationToken);
            response.EnsureSuccess();
        }

        public async Task<TemplateVersion> CreateVersionAsync(string templateId, string name, string subject, string htmlContent, string textContent, bool isActive, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject
            {
                { "name", name },
                { "subject", subject },
                { "html_content", htmlContent },
                { "plain_content", textContent },
                { "active", isActive ? 1 : 0 }
            };
            var response = await _client.Post(string.Format("{0}/{1}/versions", _endpoint, templateId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var templateVersion = JObject.Parse(responseContent).ToObject<TemplateVersion>();
            return templateVersion;
        }

        public async Task<TemplateVersion> ActivateVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Post(string.Format("{0}/{1}/versions/{2}/activate", _endpoint, templateId, versionId), (JObject)null, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var templateVersion = JObject.Parse(responseContent).ToObject<TemplateVersion>();
            return templateVersion;
        }

        public async Task<TemplateVersion> GetVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}/versions/{2}", _endpoint, templateId, versionId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var templateVersion = JObject.Parse(responseContent).ToObject<TemplateVersion>();
            return templateVersion;
        }

        public async Task<TemplateVersion> UpdateVersionAsync(string templateId, string versionId, string name = null, string subject = null, string htmlContent = null, string textContent = null, bool? isActive = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject();
            if (!string.IsNullOrEmpty(name)) data.Add("name", name);
            if (!string.IsNullOrEmpty(subject)) data.Add("subject", subject);
            if (!string.IsNullOrEmpty(htmlContent)) data.Add("html_content", htmlContent);
            if (!string.IsNullOrEmpty(textContent)) data.Add("plain_content", textContent);
            if (isActive.HasValue) data.Add("active", isActive.Value ? 1 : 0);

            var response = await _client.Patch(string.Format("{0}/{1}/versions/{2}", _endpoint, templateId, versionId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var templateVersion = JObject.Parse(responseContent).ToObject<TemplateVersion>();
            return templateVersion;
        }

        public async Task DeleteVersionAsync(string templateId, string versionId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}/versions/{2}", _endpoint, templateId, versionId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }
    }
}