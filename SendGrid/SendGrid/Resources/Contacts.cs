using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SendGrid.Resources
{
    public class Contacts
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Recipients object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/contactdb.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Contacts(Client client, string endpoint = "v3/contactdb/recipients")
        {
            _endpoint = endpoint;
            _client = client;
        }

        public async Task<string> CreateAsync(Contact contact)
        {
            var importResult = await ImportAsync(new[] { contact });
            if (importResult.ErrorCount > 0)
            {
                // There should only be one error message but to be safe let's combine all error messages into a single string
                var errorMsg = string.Join(Environment.NewLine, importResult.Errors.Select(e => e.Message));
                throw new Exception(errorMsg);
            }
            return importResult.PersistedRecipients.Single();
        }

        public async Task<string> UpdateAsync(Contact contact)
        {
            var data = new JArray(ConvertContactToJObject(contact));
            var response = await _client.Patch(_endpoint, data);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var importResult = JObject.Parse(responseContent).ToObject<ImportResult>();
            if (importResult.ErrorCount > 0)
            {
                // There should only be one error message but to be safe let's combine all error messages into a single string
                var errorMsg = string.Join(Environment.NewLine, importResult.Errors.Select(e => e.Message));
                throw new Exception(errorMsg);
            }
            return importResult.PersistedRecipients.Single();
        }

        public async Task<ImportResult> ImportAsync(IEnumerable<Contact> contacts)
        {
            var data = new JArray();
            foreach (var contact in contacts)
            {
                data.Add(ConvertContactToJObject(contact));
            }

            var response = await _client.Post(_endpoint, data);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var importResult = JObject.Parse(responseContent).ToObject<ImportResult>();
            return importResult;
        }

        public async Task DeleteAsync(string contactId)
        {
            await DeleteAsync(new[] { contactId });
        }

        public async Task DeleteAsync(IEnumerable<string> contactId)
        {
            var data = JArray.FromObject(contactId.ToArray());
            var response = await _client.Delete(_endpoint, data);
            response.EnsureSuccess();
        }

        public async Task<Contact[]> GetAsync(int recordsPerPage = 100, int page = 1)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["page_size"] = recordsPerPage.ToString(CultureInfo.InvariantCulture);
            query["page"] = page.ToString(CultureInfo.InvariantCulture);

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query));
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

        public async Task<long> GetBillableCountAsync()
        {
            var response = await _client.Get(string.Format("{0}/billable_count", _endpoint));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //    "recipient_count": 2
            // }
            // We use a dynamic object to get rid of the 'recipient_count' property and simply return the numerical value
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicValue = dynamicObject.recipient_count;

            var count = dynamicValue.ToObject<long>();
            return count;
        }

        public async Task<long> GetTotalCountAsync()
        {
            var response = await _client.Get(string.Format("{0}/count", _endpoint));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            // {
            //    "recipient_count": 2
            // }
            // We use a dynamic object to get rid of the 'recipient_count' property and simply return the numerical value
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicValue = dynamicObject.recipient_count;

            var count = dynamicValue.ToObject<long>();
            return count;
        }

        public async Task<Contact[]> SearchAsync(string fieldName, string value)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query[fieldName] = value;

            var response = await _client.Get(string.Format("{0}/search?{1}", _endpoint, query));
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

        public async Task<Contact[]> SearchAsync(string fieldName, DateTime value)
        {
            return await SearchAsync(fieldName, value.ToUnixTime().ToString(CultureInfo.InvariantCulture));
        }

        public async Task<Contact[]> SearchAsync(string fieldName, long value)
        {
            return await SearchAsync(fieldName, value.ToString(CultureInfo.InvariantCulture));
        }

        private static JObject ConvertContactToJObject(Contact contact)
        {
            var result = new JObject();
            if (!string.IsNullOrEmpty(contact.Id)) result.Add("id", contact.Id);
            if (!string.IsNullOrEmpty(contact.Email)) result.Add("email", contact.Email);
            if (!string.IsNullOrEmpty(contact.FirstName)) result.Add("first_name", contact.FirstName);
            if (!string.IsNullOrEmpty(contact.LastName)) result.Add("last_name", contact.LastName);

            if (contact.CustomFields != null)
            {
                foreach (var customField in contact.CustomFields.OfType<CustomField<string>>())
                {
                    result.Add(customField.Name, customField.Value);
                }
                foreach (var customField in contact.CustomFields.OfType<CustomField<int>>())
                {
                    result.Add(customField.Name, customField.Value);
                }
                foreach (var customField in contact.CustomFields.OfType<CustomField<DateTime>>())
                {
                    result.Add(customField.Name, customField.Value.ToUnixTime());
                }
            }

            return result;
        }
    }
}