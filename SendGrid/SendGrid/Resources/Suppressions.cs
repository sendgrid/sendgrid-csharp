using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class Suppressions
    {
        private readonly string _endpoint;
        private readonly Client _client;

        /// <summary>
        /// Constructs the SendGrid Suppressions object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Suppressions(Client client, string endpoint = "v3/asm/groups")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get suppressed addresses for a given group.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Get(int groupId)
        {
            return await _client.Get(string.Format("{0}/{1}/suppressions", _endpoint, groupId));
        }

        /// <summary>
        /// Add recipient addresses to the suppressions list for a given group.
        /// 
        /// If the group has been deleted, this request will add the address to the global suppression.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <param name="emails">Array of email addresses to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Post(int groupId, string[] emails)
        {
            var data = new JObject(new JProperty("recipient_emails", new JArray(emails)));

            return await _client.Post(string.Format("{0}/{1}/suppressions", _endpoint, groupId), data);
        }

        /// <summary>
        /// Delete a suppression group.
        /// </summary>
        /// <param name="groupId">ID of the suppression group to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task<HttpResponseMessage> Delete(int groupId, string email)
        {
            return await _client.Delete(string.Format("{0}/{1}/suppressions/{2}", _endpoint, groupId, email));
        }
    }
}