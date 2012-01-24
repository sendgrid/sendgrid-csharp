using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using SendGridMail.Transport;

namespace SendGridMail
{
    public class SendGrid : ISendGrid
    {
        #region constants/vars
        //private/constant vars:
        private static readonly Dictionary<String, String> Filters = InitializeFilters();
        private MailMessage message;

        // TODO find appropriate types for these
        const string encoding = "quoted-printable";
        const string charset = "utf-8";

        //apps list and settings
        private const String ReText = @"<\%\s*\%>";
        private const String ReHtml = @"<\%\s*[^\s]+\s*\%>";
        #endregion

        #region Initialization and Constructors
        /// <summary>
        /// Creates an instance of SendGrid's custom message object
        /// </summary>
        /// <returns></returns>
        public static SendGrid GetInstance()
        {
            var header = new Header();
            return new SendGrid(header);
        }

        /// <summary>
        /// Creates an instance of SendGrid's custom message object with mail parameters
        /// </summary>
        /// <param name="from">The email address of the sender</param>
        /// <param name="to">An array of the recipients</param>
        /// <param name="cc">Supported over SMTP, with future plans for support in the Web transport</param>
        /// <param name="bcc">Blind recipients</param>
        /// <param name="subject">The subject of the message</param>
        /// <param name="html">the html content for the message</param>
        /// <param name="text">the plain text part of the message</param>
        /// <param name="transport">Transport class to use for sending the message</param>
        /// <returns></returns>
        public static SendGrid GetInstance(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc,
                                                String subject, String html, String text)
        {
            var header = new Header();
            return new SendGrid(from, to, cc, bcc, subject, html, text, header);
        }

        internal SendGrid(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc, 
            String subject, String html, String text, IHeader header = null ) : this(header)
        {
            From = from;
            To = to;
            Cc = cc;
            Bcc = bcc;

            message.Subject = subject;

            Text = text;
            Html = html;
        }

        internal SendGrid(IHeader header)
        {
            message = new MailMessage();
            Header = header;
            Headers = new Dictionary<string, string>();
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
                {"Bcc", "bcc"},
                {"BypassListManagement", "bypass_list_management"}
            };
        }
        #endregion

        #region Properties
        public MailAddress From
        {
            get
            {
                return message.From;
            }
            set
            {
                if (value != null) message.From = value;
            }
        }

        public MailAddress[] ReplyTo
        {
            get
            {
                return message.ReplyToList.ToArray();
            }
            set
            {
                message.ReplyToList.Clear();
                foreach (var replyTo in value)
                {
                    message.ReplyToList.Add(replyTo);
                }
            }
        }

        public MailAddress[] To
        {
            get
            {
                return message.To.ToArray();
            }
            set
            {
                message.To.Clear();
                foreach (var mailAddress in value)
                {
                    message.To.Add(mailAddress);
                }
            }
        }

        public MailAddress[] Cc
        {
            get
            {
                return message.CC.ToArray();
            }
            set
            {
                message.CC.Clear();
                foreach (var mailAddress in value)
                {
                    message.CC.Add(mailAddress);
                }
            }
        }

        public MailAddress[] Bcc
        {
            get
            {
                return message.Bcc.ToArray();
            }
            set
            {
                message.Bcc.Clear();
                foreach (var mailAddress in value)
                {
                    message.Bcc.Add(mailAddress);
                }
            }
        }

        public String Subject
        {
            get
            {
                return message.Subject;
            }
            set
            {
                if (value != null) message.Subject = value;
            }
        }

        public Dictionary<String, String> Headers { get; set; }
        public IHeader Header { get; set; }
        public String Html { get; set; }
        public String Text { get; set; }
        #endregion

        #region Methods for setting data
        public void AddTo(String address)
        {
            var mailAddress = new MailAddress(address);
            message.To.Add(mailAddress);
        }

        public void AddTo(IEnumerable<String> addresses)
        {
            if (addresses != null)
            {
                foreach (var address in addresses)
                {
                    if (address != null) AddTo(address);
                }
            }
        }

        public void AddTo(IDictionary<String, IDictionary<String, String>> addresssInfo)
        {
            foreach (var address in addresssInfo.Keys)
            {
                var table = addresssInfo[address];
                //DisplayName is the only option that this implementation of MailAddress implements.
                var mailAddress = new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null);
                message.To.Add(mailAddress);
            }
        }

        public void AddCc(String address)
        {
            var mailAddress = new MailAddress(address);
            message.CC.Add(mailAddress);
        }

        public void AddCc(IEnumerable<String> addresses)
        {
            if (addresses != null)
            {
                foreach (var address in addresses)
                {
                    if (address != null) AddCc(address);
                }
            }
        }

        public void AddCc(IDictionary<String, IDictionary<String, String>> addresssInfo)
        {
            foreach (var address in addresssInfo.Keys)
            {
                var table = addresssInfo[address];
                //DisplayName is the only option that this implementation of MailAddress implements.
                var mailAddress = new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null);
                message.CC.Add(mailAddress);
            }
        }

        public void AddBcc(String address)
        {
            var mailAddress = new MailAddress(address);
            message.Bcc.Add(mailAddress);
        }

        public void AddBcc(IEnumerable<String> addresses)
        {
            if (addresses != null)
            {
                foreach (var address in addresses)
                {
                    if (address != null) AddBcc(address);
                }
            }
        }

        public void AddBcc(IDictionary<String, IDictionary<String, String>> addresssInfo)
        {
            foreach (var address in addresssInfo.Keys)
            {
                var table = addresssInfo[address];
                //DisplayName is the only option that this implementation of MailAddress implements.
                var mailAddress = new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null);
                message.Bcc.Add(mailAddress);
            }
        }

        private List<String> _attachments = new List<String>(); 
        public String[] Attachments
        {
            get { return _attachments.ToArray(); }
            set { _attachments = value.ToList(); }
        }

        public void AddSubVal(String replacementTag, List<String> substitutionValues)
        {
            //let the system complain if they do something bad, since the function returns null
            Header.AddSubVal(replacementTag, substitutionValues);
        }

        public void AddUniqueIdentifier(IDictionary<String, String> identifiers)
        {
            Header.AddUniqueIdentifier(identifiers);
        }

        public void SetCategory(String category)
        {
            Header.SetCategory(category);
        }


        public void AddAttachment(String filePath)
        {
            _attachments.Add(filePath);
        }

        public IEnumerable<String> GetRecipients()
        {
            List<MailAddress> tos = message.To.ToList();
            List<MailAddress> ccs = message.CC.ToList();
            List<MailAddress> bccs = message.Bcc.ToList();

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
            Header.Disable(Filters["Gravatar"]);
        }

        public void DisableOpenTracking()
        {
            Header.Disable(Filters["OpenTracking"]);
        }

        public void DisableClickTracking()
        {
            Header.Disable(Filters["ClickTracking"]);
        }

        public void DisableSpamCheck()
        {
            Header.Disable(Filters["SpamCheck"]);
        }

        public void DisableUnsubscribe()
        {
            Header.Disable(Filters["Unsubscribe"]);
        }

        public void DisableFooter()
        {
            Header.Disable(Filters["Footer"]);
        }

        public void DisableGoogleAnalytics()
        {
            Header.Disable(Filters["GoogleAnalytics"]);
        }

        public void DisableTemplate()
        {
            Header.Disable(Filters["Template"]);
        }

        public void DisableBcc()
        {
            Header.Disable(Filters["Bcc"]);
        }

        public void DisableBypassListManagement()
        {
            Header.Disable(Filters["BypassListManagement"]);
        }

        public void EnableGravatar()
        {
            Header.Enable(Filters["Gravatar"]);
        }

        public void EnableOpenTracking()
        {
            Header.Enable(Filters["OpenTracking"]);
        }

        public void EnableClickTracking(bool includePlainText = false)
        {
            var filter = Filters["ClickTracking"];
                
            Header.Enable(filter);
            if (includePlainText)
            {
                Header.AddFilterSetting(filter, new List<string> { "enable_text" }, "1");
            }
        }

        public void EnableSpamCheck(int score = 5, string url = null)
        {
            var filter = Filters["SpamCheck"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "maxscore" }, score.ToString(CultureInfo.InvariantCulture));
            Header.AddFilterSetting(filter, new List<string> { "url" }, url);
        }

        public void EnableUnsubscribe(string text, string html)
        {
            var filter = Filters["Unsubscribe"];

            if(!System.Text.RegularExpressions.Regex.IsMatch(text, ReText))
            {
                throw new Exception("Missing substitution replacementTag in text");
            }

            if(!System.Text.RegularExpressions.Regex.IsMatch(html, ReHtml))
            {
                throw new Exception("Missing substitution replacementTag in html");
            }

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "text/plain" }, text);
            Header.AddFilterSetting(filter, new List<string> {"text/html"}, html);
        }

        public void EnableUnsubscribe(string replace)
        {
            var filter = Filters["Unsubscribe"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "replace" }, replace);
        }

        public void EnableFooter(string text = null, string html = null)
        {
            var filter = Filters["Footer"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "text/plain" }, text);
            Header.AddFilterSetting(filter, new List<string> { "text/html" }, html);
        }

        public void EnableGoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null)
        {
            var filter = Filters["GoogleAnalytics"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "utm_source" }, source);
            Header.AddFilterSetting(filter, new List<string> { "utm_medium" }, medium);
            Header.AddFilterSetting(filter, new List<string> { "utm_term" }, term);
            Header.AddFilterSetting(filter, new List<string> { "utm_content" }, content);
            Header.AddFilterSetting(filter, new List<string> { "utm_campaign" }, campaign);
        }

        public void EnableTemplate(string html)
        {
            var filter = Filters["Template"];

            if (!System.Text.RegularExpressions.Regex.IsMatch(html, ReHtml))
            {
                throw new Exception("Missing substitution replacementTag in html");
            }

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "text/html" }, html);
        }

        public void EnableBcc(string email)
        {
            var filter = Filters["Bcc"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "email" }, email);
        }

        public void EnableBypassListManagement()
        {
            Header.Enable(Filters["BypassListManagement"]);
        }
        #endregion

        public MailMessage CreateMimeMessage()
        {
            String smtpapi = Header.AsJson();

            if (!String.IsNullOrEmpty(smtpapi))
                message.Headers.Add("X-Smtpapi", smtpapi);

            Headers.Keys.ToList().ForEach(k => message.Headers.Add(k, Headers[k]));

            message.Attachments.Clear();
            message.AlternateViews.Clear();

            if(Attachments != null)
            {
                foreach (var attachment in Attachments)
                {
                    message.Attachments.Add(new Attachment(attachment, MediaTypeNames.Application.Octet));
                }                
            }

            if (Text != null)
            {
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(Text, null, "text/plain");
                message.AlternateViews.Add(plainView);
            }

            if (Html != null)
            {
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Html, null, "text/html");
                message.AlternateViews.Add(htmlView);
            }
            
            //message.SubjectEncoding = Encoding.GetEncoding(charset);
            //message.BodyEncoding = Encoding.GetEncoding(charset);

            return message;
        }

        /// <summary>
        /// Helper function lets us look at the mime before it is sent
        /// </summary>
        /// <param name="directory">directory in which we store this mime message</param>
        public void SaveMessage(String directory)
        {
            var client = new SmtpClient("localhost")
                             {
                                 DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                                 PickupDirectoryLocation = @"C:\temp"
                             };
            var msg = CreateMimeMessage();
            client.Send(msg);
        }
    }
}
