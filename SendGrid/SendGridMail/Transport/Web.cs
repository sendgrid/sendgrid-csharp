using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection;
using System.Threading.Tasks;
using SendGrid.SmtpApi;

// ReSharper disable MemberCanBePrivate.Global
namespace SendGrid
{
	public class Web : ITransport, IDisposable
	{
		#region Properties

		//TODO: Make this configurable
        public const String Endpoint = "https://api.sendgrid.com/api/mail.send.xml";//"https://ddrst15a2d5q.runscope.net";//
	    
		private readonly NetworkCredential _credentials;
	    private readonly HttpClient _client;
	    private readonly string _apiKey;

		#endregion

	    /// <summary>
	    ///     Creates a new Web interface for sending mail
	    /// </summary>
	    /// <param name="apiKey">The API Key with which to send</param>
	    public Web(string apiKey)
	        : this(apiKey, null, TimeSpan.FromSeconds(100)) { }

        ///     Creates a new Web interface for sending mail
        /// </summary>
        /// <param name="apiKey">The API Key with which to send</param>
        /// <param name="httpTimeout">HTTP request timeout</param>
        public Web(string apiKey, TimeSpan httpTimeout)
            : this(apiKey, null, httpTimeout) { }

		/// <summary>
		///     Creates a new Web interface for sending mail
		/// </summary>
		/// <param name="credentials">SendGridMessage user parameters</param>
        public Web(NetworkCredential credentials)
            : this(null, credentials, TimeSpan.FromSeconds(100)) { }

        /// <summary>
        ///     Creates a new Web interface for sending mail.
        /// </summary>
        /// <param name="apiKey">The API Key with which to send</param>
        /// <param name="credentials">SendGridMessage user parameters</param>
        /// <param name="httpTimeout">HTTP request timeout</param>
	    public Web(string apiKey, NetworkCredential credentials, TimeSpan httpTimeout)
	    {
        	_credentials = credentials;
            _client = new HttpClient();
            _apiKey = apiKey;

            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            if (credentials == null)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            }
            _client.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", "sendgrid/" + version + ";csharp");
            _client.Timeout = httpTimeout;
	    }

		/// <summary>
		///     Asynchronously delivers a message over SendGrid's Web interface
		/// </summary>
		/// <param name="message"></param>
		public async Task DeliverAsync(ISendGrid message)
		{
			var content = new MultipartFormDataContent();
			AttachFormParams(message, content);
			AttachFiles(message, content);
			var response = await _client.PostAsync(Endpoint, content);
            		await ErrorChecker.CheckForErrorsAsync(response);
		}

	    #region Support Methods

		private void AttachFormParams(ISendGrid message, MultipartFormDataContent content)
		{
			var formParams = FetchFormParams(message);
			foreach (var keyValuePair in formParams)
			{
				content.Add(new StringContent(keyValuePair.Value), keyValuePair.Key);
			}
		}

        //TODO: Refactor this method
		private void AttachFiles(ISendGrid message, MultipartFormDataContent content)
		{
		    var embeds = FetchEmbeddedImages(message);
		    foreach (var embed in embeds)
		    {
                var fs = new FileStream(embed.Key, FileMode.Open, FileAccess.Read);
                var fileContent = new StreamContent(fs);

                fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = "files[" + Path.GetFileName(embed.Key) + "]",
                    FileName = Path.GetFileName(embed.Key)
                };

                fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
                content.Add(fileContent);		        
		    }

			var files = FetchFileBodies(message);
			foreach (var file in files)
			{
				var fs = new FileStream(file.Key, FileMode.Open, FileAccess.Read);
				var fileContent = new StreamContent(fs);

				fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
				{
					Name = "files[" + Path.GetFileName(file.Key) + "]",
					FileName = Path.GetFileName(file.Key)
				};

				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
				content.Add(fileContent);
			}

			var streamingFiles = FetchStreamingFileBodies(message);
			foreach (var file in streamingFiles)
			{
				var stream = file.Value;
				var fileContent = new StreamContent(stream);

				fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
				{
					Name = "files[" + Path.GetFileName(file.Key) + "]",
					FileName = Path.GetFileName(file.Key)
				};

				fileContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/octet-stream");
				content.Add(fileContent);
			}
		}
        
		internal List<KeyValuePair<String, String>> FetchFormParams(ISendGrid message)
		{
			var result = new List<KeyValuePair<string, string>>
			{
				
				new KeyValuePair<String, String>("headers",
					message.Headers.Count == 0 ? null : Utils.SerializeDictionary(message.Headers)),
				new KeyValuePair<String, String>("replyto",
					message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
				new KeyValuePair<String, String>("from", message.From.Address),
				new KeyValuePair<String, String>("fromname", message.From.DisplayName),
				new KeyValuePair<String, String>("subject", message.Subject),
				new KeyValuePair<String, String>("text", message.Text),
				new KeyValuePair<String, String>("html", message.Html),
				new KeyValuePair<String, String>("x-smtpapi", message.Header.JsonString() ?? "")
			};

            //if the api key is not specified, use the username and password
		    if (_credentials != null)
		    {
		        var creds = new List<KeyValuePair<string, string>>
		        {
		            new KeyValuePair<String, String>("api_user", _credentials.UserName),
		            new KeyValuePair<String, String>("api_key", _credentials.Password)
		        };
		        result.AddRange(creds);
		    }

			if (message.To != null)
			{
				result = result.Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("to[]", a.Address)))
					.Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("toname[]", a.DisplayName)))
					.ToList();
			}

		    if (message.Cc != null)
		    {
		        result.AddRange(message.Cc.Select(c => new KeyValuePair<string, string>("cc[]", c.Address)));
		    }

		    if (message.Bcc != null)
		    {
		        result.AddRange(message.Bcc.Select(c => new KeyValuePair<string, string>("bcc[]", c.Address)));
		    }
            
			if (message.GetEmbeddedImages().Count > 0)
			{
			    var embeds =
			        message.GetEmbeddedImages()
			            .ToList()
			            .Select(
			                x =>
			                    new KeyValuePair<String, String>(string.Format("content[{0}]", Path.GetFileName(x.Key)), x.Value)).ToList();
			    result = result.Concat(embeds).ToList();
			}
			return result.Where(r => !String.IsNullOrEmpty(r.Value)).ToList();
		}

	    internal IDictionary<string, string> FetchEmbeddedImages(ISendGrid message)
	    {
	        return message.GetEmbeddedImages();
	    }

		internal IEnumerable<KeyValuePair<string, MemoryStream>> FetchStreamingFileBodies(ISendGrid message)
		{
			return message.StreamedAttachments.Select(kvp => kvp).ToList();
		}

		internal List<KeyValuePair<String, FileInfo>> FetchFileBodies(ISendGrid message)
		{
			return message.Attachments == null
				? new List<KeyValuePair<string, FileInfo>>()
				: message.Attachments.Select(name => new KeyValuePair<String, FileInfo>(name, new FileInfo(name))).ToList();
		}

		#endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
            }
            _client.Dispose();
        }
	}
}
