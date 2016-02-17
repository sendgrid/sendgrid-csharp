using Newtonsoft.Json.Linq;
using SendGrid.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
    public class Suppressions
    {
        private string _endpoint;
        private Client _client;

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
        public async Task<string[]> GetUnsubscribedAddressesAsync(int groupId)
        {
            var response = await _client.Get(string.Format("{0}/{1}/suppressions", _endpoint, groupId));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var suppressedAddresses = JArray.Parse(responseContent).ToObject<string[]>();
            return suppressedAddresses;
        }

        /// <summary>
        /// Add recipient address to the suppressions list for a given group.
        /// 
        /// If the group has been deleted, this request will add the address to the global suppression.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <param name="email">Email address to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task AddAddressToUnsubscribeGroupAsync(int groupId, string email)
        {
            await AddAddressToUnsubscribeGroupAsync(groupId, new[] { email });
        }

        /// <summary>
        /// Add recipient addresses to the suppressions list for a given group.
        /// 
        /// If the group has been deleted, this request will add the address to the global suppression.
        /// </summary>
        /// <param name="groupId">ID of the suppression group</param>
        /// <param name="emails">Email addresses to add to the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task AddAddressToUnsubscribeGroupAsync(int groupId, IEnumerable<string> emails)
        {
            var data = new JObject(new JProperty("recipient_emails", JArray.FromObject(emails.ToArray())));
            var response = await _client.Post(string.Format("{0}/{1}/suppressions", _endpoint, groupId), data);
            response.EnsureSuccess();
        }

        /// <summary>
        /// Delete a recipient email from the suppressions list for a group.
        /// </summary>
        /// <param name="groupId">ID of the suppression group to delete</param>
        /// <param name="email">Email address to remove from the suppression group</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/suppressions.html</returns>
        public async Task RemoveAddressFromSuppressionGroupAsync(int groupId, string email)
        {
            var response = await _client.Delete(string.Format("{0}/{1}/suppressions/{2}", _endpoint, groupId, email));
            response.EnsureSuccess();
        }
    }
}