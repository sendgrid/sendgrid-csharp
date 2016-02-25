using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SendGrid.Resources
{
    public class GlobalStats
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid GlobalStats object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Stats/global.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public GlobalStats(Client client, string endpoint = "v3/stats")
        {
            _endpoint = endpoint;
            _client = client;
        }

        /// <summary>
        /// https://sendgrid.com/docs/API_Reference/Web_API_v3/Stats/global.html
        /// </summary>
        /// <param name="startDate">The starting date of the statistics to retrieve, formatted as YYYY-MM-DD.</param>
        /// <param name="endDate">The end date of the statistics to retrieve, formatted as YYYY-MM-DD. Defaults to today.</param>
        /// <param name="aggregatedBy">How to group the statistics, must be day|week|month</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Stats/global.html</returns>
        public Task<HttpResponseMessage> Get(string startDate, string endDate = null, string aggregatedBy = null)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["start_date"] = startDate;
            if (endDate != null)
            {
                query["end_date"] = endDate;
            }
            if (aggregatedBy != null)
            {
                query["aggregated_by"] = aggregatedBy;
            }
            return _client.Get(_endpoint + "?" + query);
        }

    }
}