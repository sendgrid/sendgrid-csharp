using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SendGrid.Resources
{
    public class Bounces
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Bounces object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Bounces(Client client, string endpoint = "v3/suppression/bounces")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Get a list of bounces
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html</returns>
        public async Task<Bounce[]> GetAsync(DateTime? start = null, DateTime? end = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            if (start.HasValue) query["start_time"] = start.Value.ToUnixTime().ToString();
            if (end.HasValue) query["end_time"] = end.Value.ToUnixTime().ToString();

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var bounces = JArray.Parse(responseContent).ToObject<Bounce[]>();
            return bounces;
        }

        /// <summary>
        /// Get a list of bounces for a given email address
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html</returns>
        public async Task<Bounce[]> GetAsync(string email)
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, email));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var bounces = JArray.Parse(responseContent).ToObject<Bounce[]>();
            return bounces;
        }
        /// <summary>
        /// Delete all bounces
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html</returns>
        public async Task DeleteAllAsync()
        {
            var response = await _client.Delete(_endpoint);
            response.EnsureSuccess();
        }

        /// <summary>
        /// Delete bounces for a specified group of email addresses
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html</returns>
        public async Task DeleteAsync(IEnumerable<string> emails)
        {
            var data = new JObject(new JProperty("emails", new JArray(emails.ToArray())));
            var response = await _client.Delete(_endpoint, data);
            response.EnsureSuccess();
        }

        /// <summary>
        /// Delete bounces for a specified email address
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/bounces.html</returns>
        public async Task DeleteAsync(string email)
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, email));
            response.EnsureSuccess();
        }
    }
}