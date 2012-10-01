using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using CodeScales.Http;
using CodeScales.Http.Entity;
using CodeScales.Http.Entity.Mime;
using CodeScales.Http.Methods;

namespace SendGrid.Transport
{
    public class Web : ITransport
    {
        #region Properties
        public const string Endpoint = "http://sendgrid.com/api/mail.send";
        public const string JsonFormat = "json";
        public const string XmlFormat = "xml";

        private readonly NetworkCredential _credentials;
        private readonly string _restEndpoint;
        private readonly string _format;
        #endregion

        /// <summary>
        /// Factory method for Web transport of sendgrid messages
        /// </summary>
        /// <param name="credentials">SendGrid credentials for sending mail messages</param>
        /// <param name="url">The uri of the Web endpoint</param>
        /// <returns>New instance of the transport mechanism</returns>
        public static Web GetInstance(NetworkCredential credentials, string url = Endpoint)
        {
            return new Web(credentials, url);
        }

        /// <summary>
        /// Creates a new Web interface for sending mail.  Preference is using the Factory method.
        /// </summary>
        /// <param name="credentials">SendGrid user parameters</param>
        /// <param name="url">The uri of the Web endpoint</param>
        internal Web(NetworkCredential credentials, string url = Endpoint)
        {
            _credentials = credentials;

            _format = XmlFormat;
            _restEndpoint = url + "." + _format;
        }

        /// <summary>
        /// Delivers a message over SendGrid's Web interface
        /// </summary>
        /// <param name="message"></param>
        public void Deliver(IMail message)
        {
            MultipartEntity multipartEntity;
            HttpPost postMethod;

            var client = InitializeTransport(out multipartEntity, out postMethod);
            AttachFormParams(message, multipartEntity);
            AttachFiles(message, multipartEntity);
            var response = client.Execute(postMethod);
            CheckForErrors(response);
        }

        #region Support Methods

        internal HttpClient InitializeTransport(out MultipartEntity multipartEntity, out HttpPost postMethod)
        {
            var client = new HttpClient();
            postMethod = new HttpPost(new Uri(_restEndpoint));

            multipartEntity = new MultipartEntity();
            postMethod.Entity = multipartEntity;
            return client;
        }

        private void AttachFormParams(IMail message, MultipartEntity multipartEntity)
        {
            var formParams = FetchFormParams(message);
            formParams.ForEach(kvp => multipartEntity.AddBody(new StringBody(Encoding.UTF8, kvp.Key, kvp.Value)));
        }

        private void AttachFiles(IMail message, MultipartEntity multipartEntity)
        {
            var files = FetchFileBodies(message);
            files.ForEach(kvp => multipartEntity.AddBody(new FileBody("files[" + Path.GetFileName(kvp.Key) + "]", Path.GetFileName(kvp.Key), kvp.Value)));

            var streamingFiles = FetchStreamingFileBodies(message);
            streamingFiles.ForEach(kvp => multipartEntity.AddBody(new StreamedFileBody(kvp.Value, kvp.Key)));
        }

        private void CheckForErrors(CodeScales.Http.Methods.HttpResponse response)
        {
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
                            case "error": // failure
                                throw new ProtocolViolationException(status);
                            default:
                                throw new ArgumentException("Unknown element: " + reader.Name);
                        }
                    }
                }
            }
        }

        internal List<KeyValuePair<string, string>> FetchFormParams(IMail message)
        {
            var result = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("api_user", _credentials.UserName),
                new KeyValuePair<string, string>("api_key", _credentials.Password),
                new KeyValuePair<string, string>("headers", message.Headers.Count == 0 ? null :  Utils.SerializeDictionary(message.Headers)),
                new KeyValuePair<string, string>("replyto", message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
                new KeyValuePair<string, string>("from", message.From.Address),
                new KeyValuePair<string, string>("fromname", message.From.DisplayName),
                new KeyValuePair<string, string>("subject", message.Subject),
                new KeyValuePair<string, string>("text", message.Text),
                new KeyValuePair<string, string>("html", message.Html),
                new KeyValuePair<string, string>("x-smtpapi", message.Header.AsJson())
            };
            if(message.To != null)
            {
                result = result.Concat(message.To.ToList().Select(a => new KeyValuePair<string, string>("to[]", a.Address)))
                    .Concat(message.To.ToList().Select(a => new KeyValuePair<string, string>("toname[]", a.DisplayName)))
                    .ToList();
            }
            if(message.Bcc != null)
            {
                result = result.Concat(message.Bcc.ToList().Select(a => new KeyValuePair<string, string>("bcc[]", a.Address)))
                        .ToList();
            }
            if(message.Cc != null)
            {
                result = result.Concat(message.Cc.ToList().Select(a => new KeyValuePair<string, string>("cc[]", a.Address)))
                    .ToList();
            }
            return result.Where(r => !string.IsNullOrEmpty(r.Value)).ToList();
        }
        
        internal List<KeyValuePair<string, MemoryStream>> FetchStreamingFileBodies(IMail message)
        {
            return message.StreamedAttachments.Select(kvp => kvp).ToList();
        }

        internal List<KeyValuePair<string, FileInfo>> FetchFileBodies(IMail message)
        {
            if(message.Attachments == null)
                return new List<KeyValuePair<string, FileInfo>>();
            return message.Attachments.Select(name => new KeyValuePair<string, FileInfo>(name, new FileInfo(name))).ToList();
        }

        #endregion
    }
}
