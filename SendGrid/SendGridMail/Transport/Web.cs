﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml;
using Smtpapi;

// ReSharper disable MemberCanBePrivate.Global
namespace SendGridMail
{
	public class Web : ITransport
	{
		#region Properties

		//TODO: Make this configurable
		public const String BaseUrl = "api.sendgrid.com";
		public const String Endpoint = "/api/mail.send";

		private readonly NetworkCredential _credentials;

		#endregion

		/// <summary>
		///     Creates a new Web interface for sending mail.  Preference is using the Factory method.
		/// </summary>
		/// <param name="credentials">SendGrid user parameters</param>
		/// <param name="https">Use https?</param>
		internal Web(NetworkCredential credentials)
		{
			_credentials = credentials;
		}

		/// <summary>
		///     Delivers a message over SendGrid's Web interface
		/// </summary>
		/// <param name="message"></param>
		public void Deliver(ISendGrid message)
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri("https://" + BaseUrl)
			};

			var content = new MultipartFormDataContent();
			AttachFormParams(message, content);
			AttachFiles(message, content);
			var response = client.PostAsync(Endpoint + ".xml", content).Result;
			CheckForErrors(response);
		}

		/// <summary>
		///     Factory method for Web transport of sendgrid messages
		/// </summary>
		/// <param name="credentials">SendGrid credentials for sending mail messages</param>
		/// <param name="https">Use https?</param>
		/// <returns>New instance of the transport mechanism</returns>
		public static Web GetInstance(NetworkCredential credentials)
		{
			return new Web(credentials);
		}

		/// <summary>
		///     Asynchronously delivers a message over SendGrid's Web interface
		/// </summary>
		/// <param name="message"></param>
		public async void DeliverAsync(ISendGrid message)
		{
			var client = new HttpClient
			{
				BaseAddress = new Uri("https://" + BaseUrl)
			};

			var content = new MultipartFormDataContent();
			AttachFormParams(message, content);
			AttachFiles(message, content);
			var response = await client.PostAsync(Endpoint + ".xml", content);
			await CheckForErrorsAsync(response);
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

		private void AttachFiles(ISendGrid message, MultipartFormDataContent content)
		{
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

		private static void CheckForErrors(HttpResponseMessage response)
		{
			//transport error
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception(response.ReasonPhrase);
			}

			var content = response.Content.ReadAsStreamAsync().Result;

			FindErrorsInResponse(content);
		}

		private static void FindErrorsInResponse(Stream content)
		{
			using (var reader = XmlReader.Create(content))
			{
				while (reader.Read())
				{
					if (!reader.IsStartElement()) continue;
					switch (reader.Name)
					{
						case "result":
							break;
						case "message": // success
							if (reader.ReadToNextSibling("errors"))
								throw new ProtocolViolationException();
							return;
						case "error": // failure
							throw new ProtocolViolationException();
						default:
							throw new ArgumentException("Unknown element: " + reader.Name);
					}
				}
			}
		}

		private static async Task CheckForErrorsAsync(HttpResponseMessage response)
		{
			//transport error
			if (response.StatusCode != HttpStatusCode.OK)
			{
				throw new Exception(response.ReasonPhrase);
			}

			var content = await response.Content.ReadAsStreamAsync();

			FindErrorsInResponse(content);
		}

		internal List<KeyValuePair<String, String>> FetchFormParams(ISendGrid message)
		{
			var result = new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<String, String>("api_user", _credentials.UserName),
				new KeyValuePair<String, String>("api_key", _credentials.Password),
				new KeyValuePair<String, String>("headers",
					message.Headers.Count == 0 ? null : Utils.SerializeDictionary(message.Headers)),
				new KeyValuePair<String, String>("replyto",
					message.ReplyTo.Length == 0 ? null : message.ReplyTo.ToList().First().Address),
				new KeyValuePair<String, String>("from", message.From.Address),
				new KeyValuePair<String, String>("fromname", message.From.DisplayName),
				new KeyValuePair<String, String>("subject", message.Subject),
				new KeyValuePair<String, String>("text", message.Text),
				new KeyValuePair<String, String>("html", message.Html),
				new KeyValuePair<String, String>("x-smtpapi", message.Header.JsonString())
			};
			if (message.To != null)
			{
				result = result.Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("to[]", a.Address)))
					.Concat(message.To.ToList().Select(a => new KeyValuePair<String, String>("toname[]", a.DisplayName)))
					.ToList();
			}
			if (message.Bcc != null)
			{
				result = result.Concat(message.Bcc.ToList().Select(a => new KeyValuePair<String, String>("bcc[]", a.Address)))
					.ToList();
			}
			if (message.Cc != null)
			{
				throw new NotSupportedException("CC is not supported by the SendGrid Web API");
			}
			return result.Where(r => !String.IsNullOrEmpty(r.Value)).ToList();
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
	}
}
