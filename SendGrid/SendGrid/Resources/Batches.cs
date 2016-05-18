using System.Net.Http;
using System.Threading.Tasks;

namespace SendGrid.Resources
{
    public class Batches
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Batches object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Batches(Client client, string endpoint = "v3/mail/batch")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// Validate whether or not a batch id is valid
        /// </summary>
        /// <param name="batchId">The batch id you want to check</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html</returns>
        public async Task<HttpResponseMessage> Get(string batchId)
        {
            return await _client.Get(_endpoint + "/" + batchId);
        }

        /// <summary>
        /// Create a new Batch ID
        /// </summary>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/cancel_schedule_send.html</returns>
        public async Task<HttpResponseMessage> Post()
        {
            return await _client.Post(_endpoint, null);
        }

    }
}