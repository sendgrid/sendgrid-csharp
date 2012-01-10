using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace SendGrid.Transport
{
    public class REST : ITransport
    {
        // REST delivery settings
        public const String Endpoint = "https://sendgrid.com/api/mail.send";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly NameValueCollection _queryParameters;
        private readonly String _restEndpoint;
        private readonly String _format;

        public REST(NetworkCredential credentials, String url = Endpoint)
        {
            _queryParameters = HttpUtility.ParseQueryString(String.Empty);
            _queryParameters["api_user"] = credentials.UserName;
            _queryParameters["api_key"] = credentials.Password;

            _format = XmlFormat;
            _restEndpoint = url + "." + this._format;
        }

        public void Deliver(ISendGrid message)
        {
            // TODO Fix this to include all recipients
            _queryParameters["to"] = message.To.First().ToString();
            _queryParameters["from"] = message.From.ToString();
            _queryParameters["subject"] = message.Subject;
            _queryParameters["text"] = message.Text;
            _queryParameters["html"] = message.Html;

            String smtpapi = message.Header.AsJson();

            if (!String.IsNullOrEmpty(smtpapi))
                _queryParameters["x-smtpapi"] = smtpapi;

            var restCommand = new Uri(_restEndpoint + "?" + _queryParameters);

            var request = (HttpWebRequest)WebRequest.Create(restCommand.AbsoluteUri);
            var response = (HttpWebResponse)request.GetResponse();

            // Basically, read the entire message out before we parse the XML.
            // That way, if we detect an error, we can give the whole response to the client.
            var r = new StreamReader(response.GetResponseStream());
            var status = r.ReadToEnd();
            var bytes = Encoding.ASCII.GetBytes(status);
            var stream = new MemoryStream(bytes);

            using (var reader = XmlReader.Create(stream))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.Name)
                        {
                            case "result":
                                break;
                            case "message": // success
                                return;
                            case "error":   // failure
                                throw new ProtocolViolationException(status);
                            default:
                                throw new ArgumentException("Unknown element: " + reader.Name);
                        }
                    }
                }
            }
        }
    }
}
