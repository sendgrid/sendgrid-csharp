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
    public class Segments
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Campaigns object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/campaigns.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Segments(Client client, string endpoint = "v3/contactdb/segments")
        {
            _endpoint = endpoint;
            _client = client;
        }

        public async Task<Segment> CreateAsync(string name, long listId, IEnumerable<Condition> conditions, CancellationToken cancellationToken = default(CancellationToken))
        {
            conditions = (conditions ?? Enumerable.Empty<Condition>());

            var data = new JObject()
            {
                { "name", name },
                { "list_id", listId },
                { "conditions", JArray.FromObject(conditions.ToArray()) }
            };
            var response = await _client.Post(_endpoint, data, cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var segment = JObject.Parse(responseContent).ToObject<Segment>();
            return segment;
        }

        public async Task<Segment[]> GetAllAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(_endpoint, cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //   "segments": [
            //     {
            //       "id": 1,
            //       "name": "Last Name Miller",
            //       "list_id": 4,
            //       "conditions": [
            //         {
            //           "field": "last_name",
            //           "value": "Miller",
            //           "operator": "eq",
            //           "and_or": ""
            //         }
            //       ],
            //       "recipient_count": 1
            //     }
            //   ]
            // }
            // We use a dynamic object to get rid of the 'segments' property and simply return an array of segments
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.segments;

            var segments = dynamicArray.ToObject<Segment[]>();
            return segments;
        }

        public async Task<Segment> GetAsync(long segmentId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, segmentId), cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var segment = JObject.Parse(responseContent).ToObject<Segment>();
            return segment;
        }

        public async Task<Segment> UpdateAsync(long segmentId, string name = null, long? listId = null, IEnumerable<Condition> conditions = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            conditions = (conditions ?? Enumerable.Empty<Condition>());

            var data = new JObject();
            if (!string.IsNullOrEmpty(name)) data.Add("name", name);
            if (listId.HasValue) data.Add("list_id", listId.Value);
            if (conditions.Any()) data.Add("conditions", JArray.FromObject(conditions.ToArray()));

            var response = await _client.Patch(string.Format("{0}/{1}", _endpoint, segmentId), data, cancellationToken);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var segment = JObject.Parse(responseContent).ToObject<Segment>();
            return segment;
        }

        public async Task DeleteAsync(long segmentId, bool deleteMatchingContacts = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["delete_contacts"] = (deleteMatchingContacts ? "true" : "false");

            var response = await _client.Delete(string.Format("{0}/{1}?{2}", _endpoint, segmentId, query), cancellationToken);
            response.EnsureSuccess();
        }

        public async Task<Contact[]> GetRecipientsAsync(long segmentId, int recordsPerPage = 100, int page = 1, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["page_size"] = recordsPerPage.ToString(CultureInfo.InvariantCulture);
            query["page"] = page.ToString(CultureInfo.InvariantCulture);

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query), cancellationToken);
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
    }
}