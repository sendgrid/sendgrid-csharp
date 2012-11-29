using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Retrieve and delete entries in the Spam Reports list with the Web API.
    /// </summary>
    public class WebSpamReportApi : ISpamReportApi
    {
        public const String Endpoint = "https://sendgrid.com/api/spamreports";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly NetworkCredential _credentials;
        private readonly String _restEndpoint;
        private readonly String _format;

        /// <summary>
        /// Creates a new instance of the web api
        /// </summary>
        /// <param name="credentials">SendGrid user parameters</param>
        /// <param name="url">The uri of the Web endpoint</param>
        public WebSpamReportApi(NetworkCredential credentials, String url = Endpoint)
        {
            this._credentials = credentials;

            this._format = XmlFormat;
            this._restEndpoint = url;
        }

        /// <summary>
        /// Retrieve a list of spam reports with addresses and ip address, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the spam report should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve spam reports (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve spam reports.</param>
        /// <param name="endDate">The end of the date range for which to retrieve spam reports.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        public List<SpamReport> GetSpamReports(bool includeDate, int? days, DateTime? startDate, DateTime? endDate, int? limit, int? offset, string email)
        {
            List<SpamReport> items = new List<SpamReport>();
            System.Collections.Specialized.NameValueCollection reqParams = new System.Collections.Specialized.NameValueCollection();
            reqParams.Add("api_user", this._credentials.UserName);
            reqParams.Add("api_key", this._credentials.Password);
            if (includeDate)
            {
                reqParams.Add("date", "1");
            }
            if (days.HasValue && days.Value > 0)
            {
                reqParams.Add("days", days.Value.ToString());
            }
            if (startDate.HasValue)
            {
                reqParams.Add("start_date", startDate.Value.ToString("yyyy-MM-dd"));
            }
            if (endDate.HasValue)
            {
                reqParams.Add("end_date", endDate.Value.ToString("yyyy-MM-dd"));
            }
            if (limit.HasValue && limit.Value > 0)
            {
                reqParams.Add("limit", limit.ToString());
            }
            if (offset.HasValue && offset.Value > 0)
            {
                reqParams.Add("offset", offset.ToString());
            }
            if (!String.IsNullOrWhiteSpace(email))
            {
                reqParams.Add("email", email);
            }

            using (var client = new System.Net.WebClient()/*{ Credentials = _credentials }*/)
            {
                byte[] responseBytes = client.UploadValues(String.Format("{0}.get.{1}", _restEndpoint, this._format), "POST", reqParams);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(responseBytes);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
                doc.ThrowOnSendGridError();

                foreach (var node in doc.Descendants("spamreport"))
                {
                    SpamReport item = new SpamReport()
                    {
                        IP = node.Element("ip").Value,
                        Email = node.Element("email").Value
                    };
                    var nodeCreated = node.Element("created");
                    DateTime created;
                    if (nodeCreated != null && DateTime.TryParseExact(nodeCreated.Value, "yyyy-MM-dd HH:mm:ss", null, System.Globalization.DateTimeStyles.AssumeUniversal, out created))
                    {
                        item.Created = created;
                    }
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// Delete an address from the Spam Reports list.
        /// </summary>
        /// <param name="email">Required email address to filter by.</param>
        /// <returns></returns>
        public void DeleteSpamReports(string email)
        {
            System.Collections.Specialized.NameValueCollection reqParams = new System.Collections.Specialized.NameValueCollection();
            reqParams.Add("api_user", this._credentials.UserName);
            reqParams.Add("api_key", this._credentials.Password);
            reqParams.Add("email", email);

            using (var client = new System.Net.WebClient()/*{ Credentials = _credentials }*/)
            {
                byte[] responseBytes = client.UploadValues(String.Format("{0}.delete.{1}", this._restEndpoint, this._format), "POST", reqParams);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(responseBytes);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
                doc.ThrowOnSendGridError();
            }
        }
    }
}