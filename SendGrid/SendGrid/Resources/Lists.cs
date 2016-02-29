using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SendGrid.Resources
{
    public class Lists
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Lists object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/contactdb.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Lists(Client client, string endpoint = "v3/contactdb/lists")
        {
            _endpoint = endpoint;
            _client = client;
        }

        public async Task<List> CreateAsync(string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject()
            {
                new JProperty("name", name)
            };
            var response = await _client.Post(_endpoint, data, cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var bulkUpsertResult = JObject.Parse(responseContent).ToObject<List>();
            return bulkUpsertResult;
        }

        public async Task<List[]> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(_endpoint, cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //  "lists": [
            //    {
            //      "id": 1,
            //      "name": "the jones",
            //      "recipient_count": 1
            //    }
            //  ]
            //}
            // We use a dynamic object to get rid of the 'lists' property and simply return an array of lists
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.lists;

            var lists = dynamicArray.ToObject<List[]>();
            return lists;
        }

        public async Task DeleteAsync(long listId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, listId), cancellationToken);
            response.EnsureSuccess();
        }

        public async Task DeleteAsync(IEnumerable<long> recipientIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = JArray.FromObject(recipientIds.ToArray());
            var response = await _client.Delete(_endpoint, data, cancellationToken);
            response.EnsureSuccess();
        }

        public async Task<List[]> GetAsync(int recordsPerPage = 100, int page = 1, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["page_size"] = recordsPerPage.ToString(CultureInfo.InvariantCulture);
            query["page"] = page.ToString(CultureInfo.InvariantCulture);

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query), cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //  "lists": [
            //    {
            //      "id": 1,
            //      "name": "the jones",
            //      "recipient_count": 1
            //    }
            //  ]
            //}
            // We use a dynamic object to get rid of the 'lists' property and simply return an array of lists
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.lists;

            var recipients = dynamicArray.ToObject<List[]>();
            return recipients;
        }

        public async Task<List> GetAsync(long listId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, listId), cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var list = JObject.Parse(responseContent).ToObject<List>();
            return list;
        }

        public async Task UpdateAsync(long listId, string name, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject()
            {
                new JProperty("name", name)
            };
            var response = await _client.Patch(string.Format("{0}/{1}", _endpoint, listId), data, cancellationToken);
            response.EnsureSuccess();
        }

        public async Task<Contact[]> GetRecipientsAsync(long listId, int recordsPerPage = 100, int page = 1, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["page_size"] = recordsPerPage.ToString(CultureInfo.InvariantCulture);
            query["page"] = page.ToString(CultureInfo.InvariantCulture);

            var response = await _client.Get(string.Format("{0}/{1}/recipients?{2}", _endpoint, listId, query), cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //	"recipients": [
            //		{
            //			"created_at": 1422395108,
            //			"email": "e@example.com",
            //			"first_name": "Ed",
            //			"id": "YUBh",
            //			"last_clicked": null,
            //			"last_emailed": null,
            //			"last_name": null,
            //			"last_opened": null,
            //			"updated_at": 1422395108
            //		}
            //	]
            // }
            // We use a dynamic object to get rid of the 'recipients' property and simply return an array of recipients
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.recipients;

            var recipients = dynamicArray.ToObject<Contact[]>();
            return recipients;
        }

        public async Task AddRecipientAsync(long listId, string recipientId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Post(string.Format("{0}/{1}/recipients/{2}", _endpoint, listId, recipientId), (JObject)null, cancellationToken);
            response.EnsureSuccess();
        }

        public async Task RemoveRecipientAsync(long listId, string recipientId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}/recipients/{2}", _endpoint, listId, recipientId), cancellationToken);
            response.EnsureSuccess();
        }

        public async Task AddRecipientsAsync(long listId, IEnumerable<string> recipientIds, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = JArray.FromObject(recipientIds.ToArray());
            var response = await _client.Post(string.Format("{0}/{1}/recipients", _endpoint, listId), data, cancellationToken);
            response.EnsureSuccess();
        }
    }
}