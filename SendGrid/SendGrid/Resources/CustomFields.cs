using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
    public class CustomFields
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Recipients object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/contactdb.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public CustomFields(Client client, string endpoint = "v3/contactdb/custom_fields")
        {
            _endpoint = endpoint;
            _client = client;
        }

        public async Task<CustomField> CreateAsync(string name, FieldType type)
        {
            var data = new JObject()
            {
                { "name", name },
                { "type", JToken.Parse(JsonConvert.SerializeObject(type, Formatting.None, new StringEnumConverter())).Value<string>() }
            };
            var response = await _client.Post(_endpoint, data);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var field = JObject.Parse(responseContent).ToObject<CustomField>();
            return field;
        }

        public async Task<CustomField[]> GetAllAsync()
        {
            var response = await _client.Get(_endpoint);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();

            // Response looks like this:
            //{
            //  "custom_fields": [
            //    {
            //      "id": 1,
            //      "name": "birthday",
            //      "type": "date"
            //    },
            //    {
            //      "id": 2,
            //      "name": "middle_name",
            //      "type": "text"
            //    },
            //    {
            //      "id": 3,
            //      "name": "favorite_number",
            //      "type": "number"
            //    }
            //  ]
            //}
            // We use a dynamic object to get rid of the 'custom_fields' property and simply return an array of custom fields
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.custom_fields;

            var fields = dynamicArray.ToObject<CustomField[]>();
            return fields;
        }

        public async Task<CustomField> GetAsync(int fieldId)
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, fieldId));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var field = JObject.Parse(responseContent).ToObject<CustomField>();
            return field;
        }

        public async Task DeleteAsync(int fieldId)
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, fieldId));
            response.EnsureSuccess();
        }

        public async Task<Field[]> GetReservedFieldsAsync()
        {
            var response = await _client.Get(_endpoint);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var fields = JArray.Parse(responseContent).ToObject<Field[]>();
            return fields;
        }
    }
}