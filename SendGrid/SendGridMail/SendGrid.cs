using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using SendGrid.SmtpApi;

namespace SendGrid
{
	public class SendGridMessage : ISendGrid
	{
		#region constants/vars
		
		//apps list and settings
		private static readonly Dictionary<String, String> Filters = InitializeFilters();
		private readonly MailMessage _message;
#if !PCL
        private static RegexOptions _defaultRegexOptions = RegexOptions.Compiled;
#else
        private static RegexOptions _defaultRegexOptions = RegexOptions.None;

#endif
        private static readonly Regex TemplateTest = new Regex(@"<%\s*body\s*%>", _defaultRegexOptions | RegexOptions.IgnoreCase);
        private static readonly Regex TextUnsubscribeTest = new Regex(@"<%\s*%>", _defaultRegexOptions);
        private static readonly Regex HtmlUnsubscribeTest = new Regex(@"<%\s*([^\s%]+\s?)+\s*%>", _defaultRegexOptions);

		#endregion

		#region Initialization and Constructors
        
        /// <summary>
        ///     Creates an instance of SendGrid's custom message object
        /// </summary>
        /// <returns></returns>
	    public SendGridMessage() : this(new Header())
	    {
	        
	    }

        public SendGridMessage(IHeader header)
        {
            _message = new MailMessage();
            
            Header = header;
            Headers = new Dictionary<string, string>();
        }

		public SendGridMessage(MailAddress from, MailAddress[] to,
			String subject, String html, String text, IHeader header = null) : this()
		{
			From = from;
			To = to;

			_message.Subject = subject;

			Text = text;
			Html = html;
		}

		private static Dictionary<string, string> InitializeFilters()
		{
			return
				new Dictionary<string, string>
				{
					{"Gravatar", "gravatar"},
					{"OpenTracking", "opentrack"},
					{"ClickTracking", "clicktrack"},
					{"SpamCheck", "spamcheck"},
					{"Unsubscribe", "subscriptiontrack"},
					{"Footer", "footer"},
					{"GoogleAnalytics", "ganalytics"},
					{"Template", "template"},
                    {"Templates","templates"},
					{"Bcc", "bcc"},
					{"BypassListManagement", "bypass_list_management"}
				};
		}

		#endregion

		#region Properties

		public MailAddress From
		{
			get { return _message.From; }
			set { if (value != null) _message.From = value; }
		}

		public MailAddress[] ReplyTo
		{
			get { return _message.ReplyToList.ToArray(); }
			set
			{
				_message.ReplyToList.Clear();
				foreach (var replyTo in value)
				{
					_message.ReplyToList.Add(replyTo);
				}
			}
		}

		public MailAddress[] To
		{
			get { return _message.To.ToArray(); }
			set
			{
				_message.To.Clear();
				foreach (var mailAddress in value)
				{
					_message.To.Add(mailAddress);
				}
			}
		}

        public MailAddress[] Cc
        {
            get { return _message.CC.ToArray(); }
            set
            {
                _message.CC.Clear();
                foreach (var mailAddress in value)
                {
                    _message.CC.Add(mailAddress);
                }
            }
        }

        public MailAddress[] Bcc
        {
            get { return _message.Bcc.ToArray(); }
            set
            {
                _message.Bcc.Clear();
                foreach (var mailAddress in value)
                {
                    _message.Bcc.Add(mailAddress);
                }
            }
        }

		public String Subject
		{
			get { return _message.Subject; }
			set { if (value != null) _message.Subject = value; }
		}

		public Dictionary<String, String> Headers { get; set; }
		public IHeader Header { get; set; }
		public String Html { get; set; }
		public String Text { get; set; }
		
		#endregion

		#region Methods for setting data

#if !PCL
		private List<String> _attachments = new List<String>();
#endif

		private Dictionary<String, MemoryStream> _streamedAttachments = new Dictionary<string, MemoryStream>();
		private Dictionary<String, String> _contentImages = new Dictionary<string, string>();

		public void AddTo(String address)
		{
			var mailAddress = new MailAddress(address);
			_message.To.Add(mailAddress);
		}

		public void AddTo(IEnumerable<String> addresses)
		{
			if (addresses == null) return;

			foreach (var address in addresses.Where(address => address != null))
				AddTo(address);
		}

		public void AddTo(IDictionary<String, IDictionary<String, String>> addresssInfo)
		{
			foreach (var mailAddress in from address in addresssInfo.Keys let table = addresssInfo[address] select new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null))
			{
				_message.To.Add(mailAddress);
			}
		}

	    public void AddCc(string address)
	    {
	        var mailAddress = new MailAddress(address);
	        _message.CC.Add(mailAddress);
	    }

	    public void AddCc(MailAddress address)
	    {
	        _message.CC.Add(address);
	    }

	    public void AddBcc(string address)
	    {
	        var mailAddress = new MailAddress(address);
            _message.Bcc.Add(mailAddress);
	    }

	    public void AddBcc(MailAddress address)
	    {
	        _message.Bcc.Add(address);
	    }

	    public Dictionary<String, MemoryStream> StreamedAttachments
		{
			get { return _streamedAttachments; }
			set { _streamedAttachments = value; }
		}
#if !PCL
		public String[] Attachments
		{
			get { return _attachments.ToArray(); }
			set { _attachments = value.ToList(); }
		}
#else
        public String[] Attachments {get; set;}
#endif

		public void EmbedImage(String filename, String cid) {
			_contentImages[filename] = cid;
		}

		public IDictionary<string, string> GetEmbeddedImages() {
			return new Dictionary<string, string>(_contentImages);
		}

		public void AddSubstitution(String replacementTag, List<String> substitutionValues)
		{
			//let the system complain if they do something bad, since the function returns null
			Header.AddSubstitution(replacementTag, substitutionValues);
		}

		public void AddUniqueArgs(IDictionary<String, String> identifiers)
		{
			Header.AddUniqueArgs(identifiers);
		}

		public void SetCategory(String category)
		{
			Header.SetCategory(category);
		}

		public void SetCategories(IEnumerable<string> categories)
		{
			Header.SetCategories(categories);
		}

		public void AddAttachment(Stream stream, String name)
		{
			var ms = new MemoryStream();
			stream.CopyTo(ms);
			ms.Seek(0, SeekOrigin.Begin);
			StreamedAttachments[name] = ms;
		}
		public void AddAttachment(String filePath) {
#if !PCL
            _attachments.Add(filePath);
#else
            throw new NotImplementedException("loading attachments from filesystem is not supported in PCL");
#endif
		}
		public IEnumerable<String> GetRecipients()
		{
			var tos = _message.To.ToList();
			var ccs = _message.CC.ToList();
			var bccs = _message.Bcc.ToList();

			var rcpts = tos.Union(ccs.Union(bccs)).Select(address => address.Address);
			return rcpts;
		}

		public void AddHeaders(IDictionary<string, string> headers)
		{
			headers.Keys.ToList().ForEach(key => Headers[key] = headers[key]);
		}

		#endregion

		#region SMTP API Functions

		public void DisableGravatar()
		{
			Header.DisableFilter(Filters["Gravatar"]);
		}

		public void DisableOpenTracking()
		{
			Header.DisableFilter(Filters["OpenTracking"]);
		}

		public void DisableClickTracking()
		{
			Header.DisableFilter(Filters["ClickTracking"]);
		}

		public void DisableSpamCheck()
		{
			Header.DisableFilter(Filters["SpamCheck"]);
		}

		public void DisableUnsubscribe()
		{
			Header.DisableFilter(Filters["Unsubscribe"]);
		}

		public void DisableFooter()
		{
			Header.DisableFilter(Filters["Footer"]);
		}

		public void DisableGoogleAnalytics()
		{
			Header.DisableFilter(Filters["GoogleAnalytics"]);
		}

		public void DisableTemplate()
		{
			Header.DisableFilter(Filters["Template"]);
		}

		public void DisableBcc()
		{
			Header.DisableFilter(Filters["Bcc"]);
		}

		public void DisableBypassListManagement()
		{
			Header.DisableFilter(Filters["BypassListManagement"]);
		}

		public void EnableGravatar()
		{
			Header.EnableFilter(Filters["Gravatar"]);
		}

		public void EnableOpenTracking()
		{
			Header.EnableFilter(Filters["OpenTracking"]);
		}

		public void EnableClickTracking(bool includePlainText = false)
		{
			var filter = Filters["ClickTracking"];

			Header.EnableFilter(filter);
			if (includePlainText)
			{
				Header.AddFilterSetting(filter, new List<string> {"enable_text"}, "1");
			}
		}

		public void EnableSpamCheck(int score = 5, string url = null)
		{
			var filter = Filters["SpamCheck"];

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"maxscore"}, score.ToString(CultureInfo.InvariantCulture));
			Header.AddFilterSetting(filter, new List<string> {"url"}, url);
		}

		public void EnableUnsubscribe(string text, string html)
		{
			var filter = Filters["Unsubscribe"];

            if (!TextUnsubscribeTest.IsMatch(text))
			{
				throw new Exception("Missing substitution replacementTag in text");
			}

            if (!HtmlUnsubscribeTest.IsMatch(html))
			{
				throw new Exception("Missing substitution replacementTag in html");
			}

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"text/plain"}, text);
			Header.AddFilterSetting(filter, new List<string> {"text/html"}, html);
		}

		public void EnableUnsubscribe(string replace)
		{
			var filter = Filters["Unsubscribe"];

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"replace"}, replace);
		}

		public void EnableFooter(string text = null, string html = null)
		{
			var filter = Filters["Footer"];

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"text/plain"}, text);
			Header.AddFilterSetting(filter, new List<string> {"text/html"}, html);
		}

		public void EnableGoogleAnalytics(string source, string medium, string term, string content = null,
			string campaign = null)
		{
			var filter = Filters["GoogleAnalytics"];

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"utm_source"}, source);
			Header.AddFilterSetting(filter, new List<string> {"utm_medium"}, medium);
			Header.AddFilterSetting(filter, new List<string> {"utm_term"}, term);
			Header.AddFilterSetting(filter, new List<string> {"utm_content"}, content);
			Header.AddFilterSetting(filter, new List<string> {"utm_campaign"}, campaign);
		}

		public void EnableTemplate(string html)
		{
			var filter = Filters["Template"];

            if (!TemplateTest.IsMatch(html))
			{
				throw new Exception("Missing <% body %> tag in template HTML");
			}

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"text/html"}, html);
		}

        public void EnableTemplateEngine(string templateId)
        {
            var filter = Filters["Templates"];

            Header.EnableFilter(filter);
            Header.AddFilterSetting(filter, new List<string> { "template_id" }, templateId);
        }

		public void EnableBcc(string email)
		{
			var filter = Filters["Bcc"];

			Header.EnableFilter(filter);
			Header.AddFilterSetting(filter, new List<string> {"email"}, email);
		}

		public void EnableBypassListManagement()
		{
			Header.EnableFilter(Filters["BypassListManagement"]);
		}

		#endregion

		public MailMessage CreateMimeMessage()
		{
			var smtpapi = Header.JsonString();

			if (!String.IsNullOrEmpty(smtpapi))
				_message.Headers.Add("X-Smtpapi", smtpapi);

			Headers.Keys.ToList().ForEach(k => _message.Headers.Add(k, Headers[k]));

			_message.Attachments.Clear();
			_message.AlternateViews.Clear();

#if !PCL
			if (Attachments != null)
			{
				foreach (var attachment in Attachments)
				{
					_message.Attachments.Add(new Attachment(attachment, MediaTypeNames.Application.Octet));
				}
			}
#endif
			if (StreamedAttachments != null)
			{
				foreach (var attachment in StreamedAttachments)
				{
					attachment.Value.Position = 0;
					_message.Attachments.Add(new Attachment(attachment.Value, attachment.Key));
				}
			}

			if (Text != null)
			{
				var plainView = AlternateView.CreateAlternateViewFromString(Text, null, "text/plain");
				_message.AlternateViews.Add(plainView);
			}

			if (Html == null) return _message;

			var htmlView = AlternateView.CreateAlternateViewFromString(Html, null, "text/html");
			_message.AlternateViews.Add(htmlView);

			//message.SubjectEncoding = Encoding.GetEncoding(charset);
			//message.BodyEncoding = Encoding.GetEncoding(charset);

			return _message;
		}

#if !PCL
		/// <summary>
		///     Helper function lets us look at the mime before it is sent
		/// </summary>
		/// <param name="directory">directory in which we store this mime message</param>
		internal void SaveMessage(String directory)
		{
			var client = new SmtpClient("localhost")
			{
				DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
				PickupDirectoryLocation = @"C:\temp"
			};
			var msg = CreateMimeMessage();
			client.Send(msg);
		}
#endif
	}
}