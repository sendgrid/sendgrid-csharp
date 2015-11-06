using System;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace SendGrid.Resources
{

    public class APIKeysData
    {
        public string name { get; set; }
    }

    public class APIKeys
    {
        public string Name { get; set; }
        public string Endpoint { get; set; }
        private Client _client;

        public APIKeys(Client client)
        {
            Endpoint = "/v3/api_keys";
            _client = client;

        }

        public HttpResponseMessage Get()
        {
            return _client.Get(Endpoint);
        }

        public HttpResponseMessage Post(String Name)
        {
            var Data = new APIKeysData() {name = Name};
            return _client.Post(Endpoint, Data);
        }

        public HttpResponseMessage Delete(String ApiKeyID)
        {
            return _client.Delete(Endpoint + "/" + ApiKeyID);
        }

        public HttpResponseMessage Patch(String ApiKeyID, String Name)
        {
            var Data = new APIKeysData() { name = Name };
            return _client.Patch(Endpoint + "/" + ApiKeyID, Data);
        }

    }
}
