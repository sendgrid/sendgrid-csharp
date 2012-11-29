using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Bounces list with the Web API.
    /// </summary>
    public class WebBounceApi : IBounceApi
    {
        public const String Endpoint = "https://sendgrid.com/api/bounces";
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
        public WebBounceApi(NetworkCredential credentials, String url = Endpoint)
        {
            this._credentials = credentials;

            this._format = XmlFormat;
            this._restEndpoint = url;
        }
        
        /// <summary>
        /// Retrieve a list of bounces with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the bounce should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve bounces (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="type">The type(s) of bounces to include.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        public List<Bounce> GetBounces(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, BounceType type, string email)
        {
            List<Bounce> items = new List<Bounce>();
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
            if (type == BounceType.Hard)
            {
                reqParams.Add("type", "hard");
            }
            else if (type == BounceType.Soft)
            {
                reqParams.Add("type", "soft");
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

                foreach (var node in doc.Descendants("bounce"))
                {
                    Bounce item = new Bounce()
                    {
                        Email = node.Element("email").Value,
                        Reason = node.Element("reason").Value
                    };
                    var nodeStatus = node.Element("status");
                    if (nodeStatus != null)
                        item.Status = nodeStatus.Value;
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
        /// Delete an address from the Bounce list. Please note that if no parameters are specified the ENTIRE list will be deleted.
        /// </summary>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <param name="type">The type(s) of bounces to include.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        public void DeleteBounces(DateTime? startDate, DateTime? endDate, BounceType type, string email)
        {
            System.Collections.Specialized.NameValueCollection reqParams = new System.Collections.Specialized.NameValueCollection();
            reqParams.Add("api_user", this._credentials.UserName);
            reqParams.Add("api_key", this._credentials.Password);
            if (startDate.HasValue)
            {
                reqParams.Add("start_date", startDate.Value.ToString("yyyy-MM-dd"));
            }
            if (endDate.HasValue)
            {
                reqParams.Add("end_date", endDate.Value.ToString("yyyy-MM-dd"));
            }
            if (type == BounceType.Hard)
            {
                reqParams.Add("type", "hard");
            }
            else if (type == BounceType.Soft)
            {
                reqParams.Add("type", "soft");
            }
            if (!String.IsNullOrWhiteSpace(email))
            {
                reqParams.Add("email", email);
            }

            using (var client = new System.Net.WebClient()/*{ Credentials = _credentials }*/)
            {
                byte[] responseBytes = client.UploadValues(String.Format("{0}.delete.{1}", _restEndpoint, this._format), "POST", reqParams);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(responseBytes);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
                doc.ThrowOnSendGridError();
            }
        }
    }
}