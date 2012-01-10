using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace SendGrid
{
    public class SendGrid : ISendGrid
    {
        private IHeader header;

        private enum Filters {
                                    Gravatar,
                                    OpenTracking,
                                    ClickTracking,
                                    SpamCheck,
                                    Unsubscribe,
                                    Footer,
                                    GoogleAnalytics,
                                    DomainKeys,
                                    Template,
                                    Twitter,
                                    Bcc,
                                    BypassListManagement
                               };

        public SendGrid(IHeader header)
        {
            this.header = header;
        }

        private MailMessage message;

        // TODO find appropriate types for these
        const string encoding = "quoted-printable";
        const string charset = "utf-8";

    /*
            if (Html != null )
            {
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(html, null, "text/html");
                message.AlternateViews.Add(htmlView);
            }

            if (Text != null )
            {
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(Text, null, "text/plain");
                message.AlternateViews.Add(plainView);
            }

            message.BodyEncoding = Encoding.GetEncoding(charset);                
     */

        public SendGrid(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc, 
            String subject, String html, String text, TransportType transport, IHeader header = null )
        {
            message = new MailMessage();
            Header = header;

            From = from;
            To = to;

            _subs = new Dictionary<string, string>();

            message.Subject = subject;
            message.SubjectEncoding = Encoding.GetEncoding(charset);

            Text = text;
            Html = html;
        }

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

        public IHeader Header
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public String Html { get; set; }
        public String Text { get; set; }

        public TransportType Transport { get; set; }

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

        private Dictionary<string, string> _subs;

        public void AddSubVal(string tag, string value)
        {
            //let the system complain if they do something bad, since the function returns null
            _subs[tag] = value;
        }

        public void AddAttachment(string filePath)
        {
            var data = new Attachment(filePath, MediaTypeNames.Application.Octet);
            message.Attachments.Add(data);
        }

        public void AddAttachment(Attachment attachment)
        {
            message.Attachments.Add(attachment);
        }

        public void AddAttachment(Stream attachment, ContentType type)
        {
            var data = new Attachment(attachment, type);
        }

        public IEnumerable<string> GetRecipients()
        {
            List<MailAddress> tos = message.To.ToList();
            List<MailAddress> ccs = message.CC.ToList();
            List<MailAddress> bccs = message.Bcc.ToList();

            var rcpts = tos.Union(ccs.Union(bccs)).Select(address => address.Address);
            return rcpts;
        }

        private string Get(string field)
        {
            throw new NotImplementedException();
        }

        private void Set(string field, string value)
        {
            throw new NotImplementedException();
        }

        public void DisableGravatar()
        {
            this.header.Disable(Filters.Gravatar.ToString());
        }

        public void DisableOpenTracking()
        {
            this.header.Disable(Filters.OpenTracking.ToString());
        }

        public void DisableClickTracking()
        {
            this.header.Disable(Filters.ClickTracking.ToString());
        }

        public void DisableSpamCheck()
        {
            this.header.Disable(Filters.SpamCheck.ToString());
        }

        public void DisableUnsubscribe()
        {
            this.header.Disable(Filters.Unsubscribe.ToString());
        }

        public void DisableFooter()
        {
            this.header.Disable(Filters.Footer.ToString());
        }

        public void DisableGoogleAnalytics()
        {
            this.header.Disable(Filters.GoogleAnalytics.ToString());
        }

        public void DisableTemplate()
        {
            this.header.Disable(Filters.Template.ToString());
        }

        public void DisableBcc()
        {
            this.header.Disable(Filters.Bcc.ToString());
        }

        public void DisableBypassListManagement()
        {
            this.header.Disable(Filters.BypassListManagement.ToString());
        }

        public void EnableGravatar()
        {
            this.header.Enable(Filters.Gravatar.ToString());
        }

        public void EnableOpenTracking()
        {
            this.header.Enable(Filters.Gravatar.ToString());
        }

        public void EnableClickTracking(string text = null)
        {
            throw new NotImplementedException();
        }

        public void EnableSpamCheck(int score = 5, string url = null)
        {
            throw new NotImplementedException();
        }

        public void EnableUnsubscribe(string text, string html, string replace, string url, string landing)
        {
            throw new NotImplementedException();
        }

        public void EnableFooter(string text = null, string html = null)
        {
            throw new NotImplementedException();
        }

        public void EnableGoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null)
        {
            throw new NotImplementedException();
        }

        public void EnableTemplate(string html = null)
        {
            throw new NotImplementedException();
        }

        public void EnableBcc(string email = null)
        {
            throw new NotImplementedException();
        }

        public void EnableBypassListManagement()
        {
            throw new NotImplementedException();
        }

        public MailMessage CreateMimeMessage()
        {
            String smtpapi = Header.AsJson();

            if (!String.IsNullOrEmpty(smtpapi))
                this.message.Headers.Add("X-SmtpApi", "{" + smtpapi + "}");

            return this.message;
        }

        public void Mail()
        {
            throw new NotImplementedException();
        }
    }
}
