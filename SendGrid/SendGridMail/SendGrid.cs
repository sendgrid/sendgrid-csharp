using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SendGridMail
{
    public class SendGrid : ISendGrid
    {
        #region constants/vars
        //private/constant vars:
        private Dictionary<String, String> _filters;
        private MailMessage message;

        // TODO find appropriate types for these
        const string encoding = "quoted-printable";
        const string charset = "utf-8";

        //apps list and settings
        private const String ReText = @"<\%\s*\%>";
        private const String ReHtml = @"<\%\s*[^\s]+\s*\%>";
        #endregion

        public void InitializeFilters()
        {
            this._filters = 
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

        public SendGrid(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc, 
            String subject, String html, String text, TransportType transport, IHeader header = null )
        {
            message = new MailMessage();
            Header = header;

            From = from;
            To = to;
            Cc = cc;
            Bcc = bcc;

            message.Subject = subject;

            Text = text;
            Html = html;
        }

        public SendGrid(IHeader header)
        {
            message = new MailMessage();
            Header = header;

            //initialize the filters, for use within the library
            this.InitializeFilters();
        }

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

        public IHeader Header { get; set; }
        public String Html { get; set; }
        public String Text { get; set; }
        public TransportType Transport { get; set; }
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

        private List<Attachment> _attachments = new List<Attachment>(); 
        public Attachment[] Attachments
        {
            get { return _attachments.ToArray(); }
            set { _attachments = value.ToList(); }
        }

        public void AddSubVal(String tag, params String[] value)
        {
            //let the system complain if they do something bad, since the function returns null
            Header.AddSubVal(tag, value);
        }

        public void AddAttachment(String filePath)
        {
            var data = new Attachment(filePath, MediaTypeNames.Application.Octet);
            _attachments.Add(data);
        }

        public void AddAttachment(Attachment attachment)
        {
            _attachments.Add(attachment);
        }

        public void AddAttachment(Stream attachment, ContentType type)
        {
            var data = new Attachment(attachment, type); 
            _attachments.Add(data);
        }

        public IEnumerable<String> GetRecipients()
        {
            List<MailAddress> tos = message.To.ToList();
            List<MailAddress> ccs = message.CC.ToList();
            List<MailAddress> bccs = message.Bcc.ToList();

            var rcpts = tos.Union(ccs.Union(bccs)).Select(address => address.Address);
            return rcpts;
        }
        #endregion

        #region SMTP API Functions
        public void DisableGravatar()
        {
            Header.Disable(this._filters["Gravatar"]);
        }

        public void DisableOpenTracking()
        {
            Header.Disable(this._filters["OpenTracking"]);
        }

        public void DisableClickTracking()
        {
            Header.Disable(this._filters["ClickTracking"]);
        }

        public void DisableSpamCheck()
        {
            Header.Disable(this._filters["SpamCheck"]);
        }

        public void DisableUnsubscribe()
        {
            Header.Disable(this._filters["Unsubscribe"]);
        }

        public void DisableFooter()
        {
            Header.Disable(this._filters["Footer"]);
        }

        public void DisableGoogleAnalytics()
        {
            Header.Disable(this._filters["GoogleAnalytics"]);
        }

        public void DisableTemplate()
        {
            Header.Disable(this._filters["Template"]);
        }

        public void DisableBcc()
        {
            Header.Disable(this._filters["Bcc"]);
        }

        public void DisableBypassListManagement()
        {
            Header.Disable(this._filters["BypassListManagement"]);
        }

        public void EnableGravatar()
        {
            Header.Enable(this._filters["Gravatar"]);
        }

        public void EnableOpenTracking()
        {
            Header.Enable(this._filters["OpenTracking"]);
        }

        public void EnableClickTracking(string text = null)
        {
            var filter = this._filters["ClickTracking"];
                
            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
        }

        public void EnableSpamCheck(int score = 5, string url = null)
        {
            var filter = this._filters["SpamCheck"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "score" }, score.ToString(CultureInfo.InvariantCulture));
            Header.AddFilterSetting(filter, new List<string>(){ "url" }, url);
        }

        public void EnableUnsubscribe(string text, string html, string replace, string url, string landing)
        {
            var filter = this._filters["Unsubscribe"];

            if(!System.Text.RegularExpressions.Regex.IsMatch(text, SendGrid.ReText))
            {
                throw new Exception("Missing substitution tag in text");
            }

            if(!System.Text.RegularExpressions.Regex.IsMatch(html, SendGrid.ReHtml))
            {
                throw new Exception("Missing substitution tag in html");
            }

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            Header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
            Header.AddFilterSetting(filter, new List<string>(){ "replace"}, replace);
            Header.AddFilterSetting(filter, new List<string>(){ "url"}, url);
            Header.AddFilterSetting(filter, new List<string>(){ "landing" }, landing);
        }

        public void EnableFooter(string text = null, string html = null)
        {
            var filter = this._filters["Footer"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            Header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableGoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null)
        {
            var filter = this._filters["GoogleAnalytics"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "source" }, source);
            Header.AddFilterSetting(filter, new List<string>(){ "medium" }, medium);
            Header.AddFilterSetting(filter, new List<string>(){ "term" }, term);
            Header.AddFilterSetting(filter, new List<string>(){ "content" }, content);
            Header.AddFilterSetting(filter, new List<string>(){ "campaign" }, campaign);
        }

        public void EnableTemplate(string html)
        {
            var filter = this._filters["Template"];

            if (!System.Text.RegularExpressions.Regex.IsMatch(html, SendGrid.ReHtml))
            {
                throw new Exception("Missing substitution tag in html");
            }

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableBcc(string email)
        {
            var filter = this._filters["Bcc"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string>(){ "email" }, email);
        }

        public void EnableBypassListManagement()
        {
            Header.Enable(this._filters["BypassListManagement"]);
        }
        #endregion

        public MailMessage CreateMimeMessage()
        {
            String smtpapi = Header.AsJson();

            if (!String.IsNullOrEmpty(smtpapi))
                message.Headers.Add("X-Smtpapi", smtpapi);

            if(Attachments != null)
            {
                foreach (Attachment attachment in Attachments)
                {
                    message.Attachments.Add(attachment);
                }                
            }

            if (Text != null)
            { // Encoding.GetEncoding(charset)
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

        public void Mail()
        {
            SmtpClient client = new SmtpClient("localhost");
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = @"C:\temp";
            client.Send(message);
        }
    }
}
