using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class GlobalSuppressions
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Global Suppressions object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public GlobalSuppressions(Client client, string endpoint = "v3/asm/suppressions/global")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Check if a recipient address is in the global suppressions group.
        /// </summary>
        /// <param name="email">email address to check</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html</returns>
        public async Task<HttpResponseMessage> Get(string email)
        {
            return await _client.Get(_endpoint + "/" + email);
        }

        /// <summary>
        /// Add recipient addresses to the global suppression group.
        /// </summary>
        /// <param name="recipient_emails">Array of email addresses to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html</returns>
        public async Task<HttpResponseMessage> Post(string[] emails)
        {
            JArray receipient_emails = new JArray();
            foreach (string email in emails) { receipient_emails.Add(email); }
            var data = new JObject(new JProperty("recipient_emails", receipient_emails));
            return await _client.Post(_endpoint, data);
        }

        /// <summary>
        /// Delete a recipient email from the global suppressions group.
        /// </summary>
        /// <param name="email">email address to be removed from the global suppressions group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html</returns>
        public async Task<HttpResponseMessage> Delete(string email)
        {
            return await _client.Delete(_endpoint + "/" + email);
        }
    }
}