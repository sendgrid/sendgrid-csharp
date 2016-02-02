using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System;
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
        /// <param name="startDate">The starting date of the statistics to retrieve.</param>
        /// <param name="endDate">The end date of the statistics to retrieve. Defaults to today.</param>
        /// <param name="aggregatedBy">How to group the statistics, must be day|week|month</param>
        /// <returns>https://sendgrid.com/docs/API_Reference/Web_API_v3/Stats/global.html</returns>
        public async Task<GlobalStat[]> GetAsync(DateTime startDate, DateTime? endDate = null, AggregateBy aggregatedBy = AggregateBy.None)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["start_date"] = startDate.ToString("yyyy-MM-dd");
            if (endDate.HasValue) query["end_date"] = endDate.Value.ToString("yyyy-MM-dd");
            if (aggregatedBy != AggregateBy.None) query["aggregated_by"] = aggregatedBy.GetDescription();

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query));
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync();
            var getStatsResult = JArray.Parse(responseContent).ToObject<GlobalStat[]>();
            return getStatsResult;
        }
    }
}