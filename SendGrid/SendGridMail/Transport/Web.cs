using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using RestSharp;

namespace SendGridMail.Transport
{
    public class Web : ITransport
    {
        #region Properties
		//TODO: Make this configurable
		public const String BaseURl = "https://sendgrid.com/api/";
        public const String Endpoint = "mail.send";
        public const String JsonFormat = "json";
        public const String XmlFormat = "xml";

        private readonly NetworkCredential _credentials;
        private readonly String _restEndpoint;
        private readonly String _format;
        #endregion

        /// <summary>
        /// Factory method for Web transport of sendgrid messages
        /// </summary>
        /// <param name="credentials">SendGrid credentials for sending mail messages</param>
        /// <param name="url">The uri of the Web endpoint</param>
        /// <returns>New instance of the transport mechanism</returns>
        public static Web GetInstance(NetworkCredential credentials, String url = Endpoint)
        {
            return new Web(credentials, url);
        }

        /// <summary>
        /// Creates a new Web interface for sending mail.  Preference is using the Factory method.
        /// </summary>
        /// <param name="credentials">SendGrid user parameters</param>
        /// <param name="url">The uri of the Web endpoint</param>
        internal Web(NetworkCredential credentials, String url = Endpoint)
        {
            _credentials = credentials;

            _format = XmlFormat;
            _restEndpoint = url + "." + _format;
        }

        /// <summary>
        /// Delivers a message over SendGrid's Web interface
        /// </summary>
        /// <param name="message"></param>
        public void Deliver(ISendGrid message)
        {
			var client = new RestClient(BaseURl);
			var request = new RestRequest(Endpoint + ".xml", Method.POST);
            AttachFormParams(message, request);
            AttachFiles(message, request);
            var response = client.Execute(request);
            CheckForErrors(response);
        }

        #region Support Methods
        private void AttachFormParams(ISendGrid message, RestRequest request)
        {
            var formParams = FetchFormParams(message);
			formParams.ForEach(kvp => request.AddParameter(kvp.Key, kvp.Value));
        }

		private void AttachFiles(ISendGrid message, RestRequest request)
        {
			//TODO: think the files are being sent in the POST data... but we need to add them as params as well

            var files = FetchFileBodies(message);
			files.ForEach(kvp => request.AddFile(Path.GetFileName(kvp.Key), kvp.Key));

            var streamingFiles = FetchStreamingFileBodies(message);
			foreach (KeyValuePair<string, MemoryStream> file in streamingFiles) {
				var name = file.Key;
				var stream = file.Value;
				var writer = new Action<Stream>(
					delegate(Stream s)
					{
						stream.CopyTo(s);
					}
				);

				request.AddFile(name, writer, name);
			}
        }

        private void CheckForErrors (IRestResponse response)
		{
			//transport error
			if (response.ResponseStatus == ResponseStatus.Error) {
				throw new Exception(response.ErrorMessage);
			}

			//TODO: check for HTTP errors... don't throw exceptions just pass info along?
			var content = response.Content;
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));

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
							    bool errors = reader.ReadToNextSibling("errors");
								if (errors) 
									throw new ProtocolViolationException(content);
								else
								    return;
                            case "error": // failure
                                throw new ProtocolViolationException(content);
                            default:
                                throw new ArgumentException("Unknown element: " + reader.Name);
                        }
                    }
                }
            }
        }

        internal List<KeyValuePair<String, String>> FetchFormParams(ISendGrid message)
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
        
        internal List<KeyValuePair<String, MemoryStream>> FetchStreamingFileBodies(ISendGrid message)
        {
            return message.StreamedAttachments.Select(kvp => kvp).ToList();
        }

        internal List<KeyValuePair<String, FileInfo>> FetchFileBodies(ISendGrid message)
        {
            if(message.Attachments == null)
                return new List<KeyValuePair<string, FileInfo>>();
            return message.Attachments.Select(name => new KeyValuePair<String, FileInfo>(name, new FileInfo(name))).ToList();
        }

        #endregion
    }
}
