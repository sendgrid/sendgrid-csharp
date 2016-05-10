using Newtonsoft.Json.Linq;
using SendGrid.Model;
using SendGrid.Utilities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SendGrid.Resources
{
    public class Campaigns
    {
        private string _endpoint;
        private Client _client;

        /// <summary>
        /// Constructs the SendGrid Campaigns object.
        /// See https://sendgrid.com/docs/API_Reference/Web_API_v3/Marketing_Campaigns/campaigns.html
        /// </summary>
        /// <param name="client">SendGrid Web API v3 client</param>
        /// <param name="endpoint">Resource endpoint, do not prepend slash</param>
        public Campaigns(Client client, string endpoint = "v3/campaigns")
        {
            _endpoint = endpoint;
            _client = client;
        }

        public async Task<Campaign> CreateAsync(string title = null, string subject = null, long? senderId = null, string htmlContent = null, string textContent = null, IEnumerable<long> listIds = null, IEnumerable<long> segmentIds = null, IEnumerable<string> categories = null, long? suppressionGroupId = null, string customUnsubscribeUrl = null, string ipPool = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            listIds = (listIds ?? Enumerable.Empty<long>());
            segmentIds = (segmentIds ?? Enumerable.Empty<long>());
            categories = (categories ?? Enumerable.Empty<string>());

            if (!listIds.Any() && !segmentIds.Any()) throw new ArgumentException("You must specify at least one list or segment");

            var data = CreateJObjectForCampaign(title, subject, senderId, htmlContent, textContent, listIds, segmentIds, categories, suppressionGroupId, customUnsubscribeUrl, ipPool);
            var response = await _client.Post(_endpoint, data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var campaign = JObject.Parse(responseContent).ToObject<Campaign>();
            return campaign;
        }

        public async Task<Campaign[]> GetAsync(int limit = 10, int offset = 0, CancellationToken cancellationToken = default(CancellationToken))
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            query["limit"] = limit.ToString(CultureInfo.InvariantCulture);
            query["offset"] = offset.ToString(CultureInfo.InvariantCulture);

            var response = await _client.Get(string.Format("{0}?{1}", _endpoint, query), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Response looks like this:
            // {
            //   "result": [
            //     {
            //       "id": 986724,
            //       "title": "March Newsletter",
            //       "subject": "New Products for Spring!",
            //       "sender_id": 124451,
            //       "list_ids": [
            //         110,
            //         124
            //       ],
            //       "segment_ids": [
            //         110
            //       ],
            //       "categories": [
            //         "spring line"
            //       ],
            //       "suppression_group_id": 42,
            //       "custom_unsubscribe_url": "",
            //       "ip_pool": "marketing",
            //       "html_content": "<html><head><title></title></head><body><p>Check out our spring line!</p></body></html>",
            //       "plain_content": "Check out our spring line!",
            //       "status": "Draft"
            //     }
            //	]
            // }
            // We use a dynamic object to get rid of the 'result' property and simply return an array of campaigns
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicArray = dynamicObject.result;

            var campaigns = dynamicArray.ToObject<Campaign[]>();
            return campaigns;
        }

        public async Task<Campaign> GetAsync(long campaignId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}", _endpoint, campaignId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var campaign = JObject.Parse(responseContent).ToObject<Campaign>();
            return campaign;
        }

        public async Task DeleteAsync(long campaignId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}", _endpoint, campaignId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        public async Task<Campaign> UpdateAsync(long campaignId, string title = null, string subject = null, long? senderId = null, string htmlContent = null, string textContent = null, IEnumerable<long> listIds = null, IEnumerable<long> segmentIds = null, IEnumerable<string> categories = null, long? suppressionGroupId = null, string customUnsubscribeUrl = null, string ipPool = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            listIds = (listIds ?? Enumerable.Empty<long>());
            segmentIds = (segmentIds ?? Enumerable.Empty<long>());
            categories = (categories ?? Enumerable.Empty<string>());

            var data = CreateJObjectForCampaign(title, subject, senderId, htmlContent, textContent, listIds, segmentIds, categories, suppressionGroupId, customUnsubscribeUrl, ipPool);
            var response = await _client.Patch(string.Format("{0}/{1}", _endpoint, campaignId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var campaign = JObject.Parse(responseContent).ToObject<Campaign>();
            return campaign;
        }

        public async Task SendAsync(long campaignId, DateTime? sendOn = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = (sendOn.HasValue ? new JObject { { "send_at", sendOn.Value.ToUnixTime() } } : null);
            var response = await _client.Post(string.Format("{0}/{1}/schedules", _endpoint, campaignId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        public async Task UpdateScheduledDateAsync(long campaignId, DateTime sendOn, CancellationToken cancellationToken = default(CancellationToken))
        {
            var data = new JObject
            {
                { "send_at", sendOn.ToUnixTime() }
            };
            var response = await _client.Patch(string.Format("{0}/{1}/schedules", _endpoint, campaignId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        public async Task<DateTime?> GetScheduledDateAsync(long campaignId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Get(string.Format("{0}/{1}/schedules", _endpoint, campaignId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();

            var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            // Response looks like this:
            // {
            //    "send_at": 1489771528
            // }
            // We use a dynamic object to get rid of the 'send_at' property and simply return the DateTime value
            dynamic dynamicObject = JObject.Parse(responseContent);
            dynamic dynamicValue = dynamicObject.send_at;

            var unixTime = (long)dynamicValue;
            if (unixTime == 0) return null;
            else return unixTime.FromUnixTime();
        }

        public async Task UnscheduleAsync(long campaignId, CancellationToken cancellationToken = default(CancellationToken))
        {
            var response = await _client.Delete(string.Format("{0}/{1}/schedules", _endpoint, campaignId), cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        public async Task SendTestAsync(long campaignId, IEnumerable<string> emailAddresses, CancellationToken cancellationToken = default(CancellationToken))
        {
            emailAddresses = (emailAddresses ?? Enumerable.Empty<string>());
            if (!emailAddresses.Any()) throw new ArgumentException("You must specify at least one email address");

            var data = new JObject();
            if (emailAddresses.Count() == 1) data.Add("to", emailAddresses.First());
            else data.Add("to", JArray.FromObject(emailAddresses.ToArray()));

            var response = await _client.Post(string.Format("{0}/{1}/schedules/test", _endpoint, campaignId), data, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccess();
        }

        private static JObject CreateJObjectForCampaign(string title = null, string subject = null, long? senderId = null, string htmlContent = null, string textContent = null, IEnumerable<long> listIds = null, IEnumerable<long> segmentIds = null, IEnumerable<string> categories = null, long? suppressionGroupId = null, string customUnsubscribeUrl = null, string ipPool = null)
        {
            var result = new JObject();
            if (!string.IsNullOrEmpty(title)) result.Add("title", title);
            if (!string.IsNullOrEmpty(subject)) result.Add("subject", subject);
            if (senderId.HasValue) result.Add("sender_id", senderId.Value);
            if (!string.IsNullOrEmpty(htmlContent)) result.Add("html_content", htmlContent);
            if (!string.IsNullOrEmpty(textContent)) result.Add("plain_content", textContent);
            if (listIds.Any()) result.Add("list_ids", JArray.FromObject(listIds.ToArray()));
            if (segmentIds.Any()) result.Add("segment_ids", JArray.FromObject(segmentIds.ToArray()));
            if (categories.Any()) result.Add("categories", JArray.FromObject(categories.ToArray()));
            if (suppressionGroupId.HasValue) result.Add("suppression_group_id", suppressionGroupId.Value);
            if (!string.IsNullOrEmpty(ipPool)) result.Add("ip_pool", ipPool);
            return result;
        }
    }
}