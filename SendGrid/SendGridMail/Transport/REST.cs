using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Xml;

namespace SendGridMail.Transport
{
    public class REST : ITransport
    {
        // REST delivery settings
        public const String Endpoint = "https://sendgrid.com/api/mail.send";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly List<KeyValuePair<String, String>> _query;
        private readonly NameValueCollection _queryParameters;
        private readonly String _restEndpoint;
        private readonly String _format;

        public static REST GetInstance(NetworkCredential credentials, String url = Endpoint)
        {
            return new REST(credentials, url);
        }

        internal REST(NetworkCredential credentials, String url = Endpoint)
        {
            _query = new List<KeyValuePair<string, string>>();
            _queryParameters = HttpUtility.ParseQueryString(String.Empty);

            addQueryParam("api_user", credentials.UserName);
            addQueryParam("api_key", credentials.Password);

            _format = XmlFormat;
            _restEndpoint = url + "." + _format;
        }

        private void addQueryParam(String key, String value)
        {
            _query.Add(new KeyValuePair<string, string>(key, value));
        }

        public void Deliver(ISendGrid message)
        {
            // TODO Fix this to include all recipients
            message.To.ToList().ForEach(a => addQueryParam("to[]", a.Address));
            message.Bcc.ToList().ForEach(a => addQueryParam("bcc[]", a.Address));
            message.Cc.ToList().ForEach(a => addQueryParam("cc[]", a.Address));

            message.To.ToList().ForEach(a => addQueryParam("toname[]", a.DisplayName));

            addQueryParam("headers", Utils.SerializeDictionary(message.Headers));

            message.ReplyTo.ToList().ForEach(a => addQueryParam("replyto", a.Address));
            //addQueryParam("", message.From.Address);

            addQueryParam("from", message.From.Address);
            addQueryParam("fromname", message.From.DisplayName);

            addQueryParam("subject", message.Subject);
            addQueryParam("text", message.Text);
            addQueryParam("html", message.Html);

            String smtpapi = message.Header.AsJson();

            if (!String.IsNullOrEmpty(smtpapi))
                addQueryParam("x-smtpapi", smtpapi);

            var queryString = FetchQueryString();

            var restCommand = new Uri(_restEndpoint + "?" + queryString);

            Console.WriteLine(restCommand.AbsoluteUri);
            Console.WriteLine("DONE!");

            var request = (HttpWebRequest)WebRequest.Create(restCommand.AbsoluteUri);
            request.Method = "POST";
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

        private string FetchQueryString()
        {
            return String.Join("&", _query.Where(kvp => !String.IsNullOrEmpty(kvp.Value)).Select(kvp => kvp.Key + "=" + kvp.Value));
        }
    }
}
