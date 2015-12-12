using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace SendGrid.Resources
{
    public class UnsubscribeGroups
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid UnsubscribeGroups object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public UnsubscribeGroups(Client client, string endpoint = "v3/asm/groups")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Retrieve all suppression groups associated with the user.
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html</returns>
        public async Task<HttpResponseMessage> Get()
        {
            return await _client.Get(_endpoint);
        }

        /// <summary>
        /// Get information on a single suppression group.
        /// </summary>
        /// <param name="unsubscribeGroupId">ID of the suppression group to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html</returns>
        public async Task<HttpResponseMessage> Get(int unsubscribeGroupId)
        {
            return await _client.Get(_endpoint + "/" + unsubscribeGroupId);
        }

        /// <summary>
        /// Create a new suppression group.
        /// </summary>
        /// <param name="unsubscribeGroupName">The name of the new suppression group</param>
        /// <param name="unsubscribeGroupDescription">A description of the suppression group</param>
        /// <param name="unsubscribeGroupIsDefault">Default value is false</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html</returns>
        public async Task<HttpResponseMessage> Post(string unsubscribeGroupName, 
                                                    string unsubscribeGroupDescription,
                                                    bool unsubscribeGroupIsDefault)
        {
            var data = new JObject {{"name", unsubscribeGroupName},
                                    {"description", unsubscribeGroupDescription},
                                    {"is_default", unsubscribeGroupIsDefault}};
            return await _client.Post(_endpoint, data);
        }

        /// <summary>
        /// Delete a suppression group.
        /// </summary>
        /// <param name="unsubscribeGroupId">ID of the suppression group to delete</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Suppression_Management/groups.html</returns>
        public async Task<HttpResponseMessage> Delete(string unsubscribeGroupId)
        {
            return await _client.Delete(_endpoint + "/" + unsubscribeGroupId);
        }
    }
}