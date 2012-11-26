using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Invalid Emails list with the Web API.
    /// </summary>
    public class WebInvalidEmailApi : IInvalidEmailApi
    {
        public const String Endpoint = "https://sendgrid.com/api/invalidemails";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly NetworkCredential _credentials;
        private readonly String _restEndpoint;
        private readonly String _format;

        /// <summary>
        /// Creates a new Web interface for sending mail.  Preference is using the Factory method.
        /// </summary>
        /// <param name="credentials">SendGrid user parameters</param>
        /// <param name="url">The uri of the Web endpoint</param>
        public WebInvalidEmailApi(NetworkCredential credentials, String url = Endpoint)
        {
            this._credentials = credentials;

            this._format = XmlFormat;
            this._restEndpoint = url;
        }

        /// <summary>
        /// Retrieve a list of invalid emails with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the  invalid email should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve  invalid emails (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve  invalid emails.</param>
        /// <param name="endDate">The end of the date range for which to retrieve  invalid emails.</param>
        /// <param name="limit">Limit the number of results returned.</param>
        /// <param name="offset">Beginning point in the list to retrieve from.</param>
        /// <param name="email">Optional email address to filter by.</param>
        /// <returns></returns>
        public List<InvalidEmail> GetInvalidEmails(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate, Int32? limit, Int32? offset, String email)
        {
            List<InvalidEmail> items = new List<InvalidEmail>();
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
                byte[] responseBytes = client.UploadValues(String.Format("{0}.get.{1}", this._restEndpoint, this._format), "POST", reqParams);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(responseBytes);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
                doc.ThrowOnSendGridError();

                foreach (var node in doc.Descendants("invalidemail"))
                {
                    InvalidEmail item = new InvalidEmail()
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
        /// Delete an address from the Invalid Email list.
        /// </summary>
        /// <param name="email">Email Invalid Email address to remove.  Must be a valid user account email.</param>
        public void DeleteInvalidEmails(String email)
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