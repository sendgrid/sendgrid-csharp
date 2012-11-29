using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace SendGridMail.WebApi
{
    /// <summary>
    /// Allows you to retrieve and delete entries in the Blocks list with the Web API.
    /// </summary>
    public class WebBlockApi : IBlockApi
    {
        public const String Endpoint = "https://sendgrid.com/api/blocks";
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
        public WebBlockApi(NetworkCredential credentials, String url = Endpoint)
        {
            this._credentials = credentials;

            this._format = XmlFormat;
            this._restEndpoint = url;
        }
        
        /// <summary>
        /// Retrieve a list of Blocks with addresses and response codes, optionally with dates.
        /// </summary>
        /// <param name="includeDate">Determines if the date of the bounce should be included.</param>
        /// <param name="days">Number of days in the past for which to retrieve bounces (includes today).</param>
        /// <param name="startDate">The start of the date range for which to retrieve bounces.</param>
        /// <param name="endDate">The end of the date range for which to retrieve bounces.</param>
        /// <returns></returns>
        public List<Block> GetBlocks(Boolean includeDate, Int32? days, DateTime? startDate, DateTime? endDate)
        {
            List<Block> items = new List<Block>();
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

            using (var client = new System.Net.WebClient()/*{ Credentials = _credentials }*/)
            {
                byte[] responseBytes = client.UploadValues(String.Format("{0}.get.{1}", _restEndpoint, this._format), "POST", reqParams);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(responseBytes);
                System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
                doc.ThrowOnSendGridError();

                foreach (var node in doc.Descendants("block"))
                {
                    Block item = new Block()
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
        /// Delete an address from the Block list.
        /// </summary>
        /// <param name="email">Email block address to remove.  Must be a valid user account email</param>
        public void DeleteBlocks(String email)
        {
            System.Collections.Specialized.NameValueCollection reqParams = new System.Collections.Specialized.NameValueCollection();
            reqParams.Add("api_user", this._credentials.UserName);
            reqParams.Add("api_key", this._credentials.Password);
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