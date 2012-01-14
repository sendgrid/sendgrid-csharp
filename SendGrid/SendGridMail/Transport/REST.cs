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
using CodeScales.Http;
using CodeScales.Http.Entity;
using CodeScales.Http.Entity.Mime;
using CodeScales.Http.Methods;
using HttpResponse = System.Web.HttpResponse;

namespace SendGridMail.Transport
{
    public class REST : ITransport
    {
        // REST delivery settings
        public const String Endpoint = "http://sendgrid.com/api/mail.send";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly List<KeyValuePair<String, String>> _query;
        private readonly NetworkCredential _credentials;
        private readonly NameValueCollection _queryParameters;
        private readonly String _restEndpoint;
        private readonly String _format;

        private WebFileUpload _fileUpload;

        public static REST GetInstance(NetworkCredential credentials, String url = Endpoint)
        {
            return new REST(credentials, url);
        }

        internal REST(NetworkCredential credentials, String url = Endpoint)
        {
            _credentials = credentials;

            _format = XmlFormat;
            _restEndpoint = url + "." + _format;
        }

        private List<KeyValuePair<String, String>> FetchFormParams(ISendGrid message)
        {
            var result = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<String, String>("api_user", _credentials.UserName),
                new KeyValuePair<String, String>("api_key", _credentials.Password),
                new KeyValuePair<String, String>("headers", message.Headers.Count == 0 ? null :  Utils.SerializeDictionary(message.Headers)),
                new KeyValuePair<String, String>("replyto", message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
                new KeyValuePair<String, String>("from", message.From.Address),
                new KeyValuePair<String, String>("fromname", message.From.DisplayName),
                new KeyValuePair<String, String>("subject", message.Subject),
                new KeyValuePair<String, String>("text", message.Text),
                new KeyValuePair<String, String>("html", message.Html),
                new KeyValuePair<String, String>("x-smtpapi", message.Header.AsJson())
            };
            if(message.To != null)
            {
                result = result.Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("to[]", a.Address)))
                    .Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("toname[]", a.DisplayName)))
                    .ToList();
            }
            if(message.Bcc != null)
            {
                result = result.Concat(message.Bcc.ToList().Select(a => new KeyValuePair<String, String>("bcc[]", a.Address)))
                        .ToList();
            }
            if(message.Cc != null)
            {
                result = result.Concat(message.Cc.ToList().Select(a => new KeyValuePair<String, String>("cc[]", a.Address)))
                    .ToList();
            }
            return result.Where(r => !String.IsNullOrEmpty(r.Value)).ToList();
        }

        private List<KeyValuePair<String, FileInfo>> FetchFileBodies(ISendGrid message)
        {
            if(message.Attachments == null)
                return new List<KeyValuePair<string, FileInfo>>();
            return message.Attachments.Select(name => new KeyValuePair<String, FileInfo>(name, new FileInfo(name))).ToList();
        }

        public void Deliver(ISendGrid message)
        {
            var client = new HttpClient();
            var postMethod = new HttpPost(new Uri(_restEndpoint));

            var multipartEntity = new MultipartEntity();
            postMethod.Entity = multipartEntity;
            
            var formParams = FetchFormParams(message);
        
            formParams.ForEach(kvp => multipartEntity.AddBody(new StringBody(Encoding.UTF8, kvp.Key, kvp.Value)));

            var files = FetchFileBodies(message);
            files.ForEach(kvp => multipartEntity.AddBody(new FileBody("files["+kvp.Key+"]", kvp.Key, kvp.Value)));

            CodeScales.Http.Methods.HttpResponse response = client.Execute(postMethod);

            Console.WriteLine("Response Code: " + response.ResponseCode);
            Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));

            Console.WriteLine("Res");

            var status = EntityUtils.ToString(response.Entity);
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(status));


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
