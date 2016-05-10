using Newtonsoft.Json.Linq;
using SendGrid.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public async Task<bool> IsUnsubscribedAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, email), cancellationToken);
            response.EnsureSuccess();

            // If the email address is on the global suppression list, the response will look like this:
            //  {
            //      "recipient_email": "{email}"
            //  }
            // If the email address is not on the global suppression list, the response will be empty
            //
            // Therefore, we check for the presence of the 'recipient_email' to indicate if the email 
            // address is on the global suppression list or not.

            var responseContent = await response.Content.ReadAsStringAsync();
            var dynamicObject = JObject.Parse(responseContent);
            var propertyDictionary = (IDictionary<string, JToken>)dynamicObject;
            return propertyDictionary.ContainsKey("recipient_email");
        }

        /// <summary>
        /// Add recipient addresses to the global suppression group.
        /// </summary>
        /// <param name="emails">Array of email addresses to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html</returns>
        public async Task AddAsync(IEnumerable<string> emails, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject(new JProperty("recipient_emails", JArray.FromObject(emails.ToArray())));
            var response = await _client.Post(_endpoint, data, cancellationToken);
            response.EnsureSuccess();
        }

        /// <summary>
        /// Delete a recipient email from the global suppressions group.
        /// </summary>
        /// <param name="email">email address to be removed from the global suppressions group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/global_suppressions.html</returns>
        public async Task RemoveAsync(string email, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, email), cancellationToken);
            response.EnsureSuccess();
        }
    }
}