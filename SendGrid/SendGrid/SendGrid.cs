using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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

        public string From
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string To
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Cc
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Bcc
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Subject
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IHeader Header
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Html
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Text
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Transport
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string Date
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public MailMessage CreateMimeMessage()
        {
            throw new NotImplementedException();
        }

        public void AddTo(string address)
        {
            throw new NotImplementedException();
        }

        public void AddTo(IEnumerable<string> addresses)
        {
            throw new NotImplementedException();
        }

        public void AddTo(IDictionary<string, string> addresssInfo)
        {
            throw new NotImplementedException();
        }

        public void AddTo(IEnumerable<IDictionary<string, string>> addressesInfo)
        {
            throw new NotImplementedException();
        }

        public void AddCc(string address)
        {
            throw new NotImplementedException();
        }

        public void AddCc(IEnumerable<string> addresses)
        {
            throw new NotImplementedException();
        }

        public void AddCc(IDictionary<string, string> addresssInfo)
        {
            throw new NotImplementedException();
        }

        public void AddCc(IEnumerable<IDictionary<string, string>> addressesInfo)
        {
            throw new NotImplementedException();
        }

        public void AddBcc(string address)
        {
            throw new NotImplementedException();
        }

        public void AddBcc(IEnumerable<string> addresses)
        {
            throw new NotImplementedException();
        }

        public void AddBcc(IDictionary<string, string> addresssInfo)
        {
            throw new NotImplementedException();
        }

        public void AddBcc(IEnumerable<IDictionary<string, string>> addressesInfo)
        {
            throw new NotImplementedException();
        }

        public void AddRcpts(string address)
        {
            throw new NotImplementedException();
        }

        public void AddRcpts(IEnumerable<string> addresses)
        {
            throw new NotImplementedException();
        }

        public void AddRcpts(IDictionary<string, string> addresssInfo)
        {
            throw new NotImplementedException();
        }

        public void AddRcpts(IEnumerable<IDictionary<string, string>> addressesInfo)
        {
            throw new NotImplementedException();
        }

        public void AddSubVal(string tag, string value)
        {
            throw new NotImplementedException();
        }

        public void AddAttachment(string filePath)
        {
            throw new NotImplementedException();
        }

        public void AddAttachment(Attachment attachment)
        {
            throw new NotImplementedException();
        }

        public string GetMailFrom()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetRecipients()
        {
            throw new NotImplementedException();
        }

        public string Get(string field)
        {
            throw new NotImplementedException();
        }

        public void Set(string field, string value)
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
            var filter = Filters.ClickTracking.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
        }

        public void EnableSpamCheck(int score = 5, string url = null)
        {
            var filter = Filters.SpamCheck.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "score" }, score.ToString(CultureInfo.InvariantCulture));
            this.header.AddFilterSetting(filter, new List<string>(){ "url" }, url);
        }

        public void EnableUnsubscribe(string text, string html, string replace, string url, string landing)
        {
            var filter = Filters.Unsubscribe.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
            this.header.AddFilterSetting(filter, new List<string>(){ "replace"}, replace);
            this.header.AddFilterSetting(filter, new List<string>(){ "landing" }, landing);
        }

        public void EnableFooter(string text = null, string html = null)
        {
            var filter = Filters.Footer.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableGoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null)
        {
            var filter = Filters.GoogleAnalytics.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "source " }, source);
            this.header.AddFilterSetting(filter, new List<string>(){ "medium" }, medium);
            this.header.AddFilterSetting(filter, new List<string>(){ "term" }, term);
            this.header.AddFilterSetting(filter, new List<string>(){ "content" }, content);
            this.header.AddFilterSetting(filter, new List<string>(){ "compaign" }, campaign);
        }

        public void EnableTemplate(string html = null)
        {
            var filter = Filters.GoogleAnalytics.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableBcc(string email = null)
        {
            var filter = Filters.Bcc.ToString();

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "email" }, email);
        }

        public void EnableBypassListManagement()
        {
            this.header.Enable(Filters.BypassListManagement.ToString());
        }

        public void Mail()
        {
            throw new NotImplementedException();
        }
    }
}
