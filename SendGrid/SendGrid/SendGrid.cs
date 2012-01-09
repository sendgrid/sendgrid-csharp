using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace SendGrid
{
    public class SendGrid : ISendGrid
    {
        private IHeader header;

        private Dictionary<String, String> _filters;

        //apps list and settings
        private const String ReText = @"<\%\s*\%>";
        private const String ReHtml = @"<\%\s*[^\s]+\s*\%>";

        public SendGrid(IHeader header)
        {
            this.header = header;
            
            //initialize the filters, for use within the library
            this.InitializeFilters();
        }

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
            this.header.Disable(this._filters["Gravatar"]);
        }

        public void DisableOpenTracking()
        {
            this.header.Disable(this._filters["OpenTracking"]);
        }

        public void DisableClickTracking()
        {
            this.header.Disable(this._filters["ClickTracking"]);
        }

        public void DisableSpamCheck()
        {
            this.header.Disable(this._filters["SpamCheck"]);
        }

        public void DisableUnsubscribe()
        {
            this.header.Disable(this._filters["Unsubscribe"]);
        }

        public void DisableFooter()
        {
            this.header.Disable(this._filters["Footer"]);
        }

        public void DisableGoogleAnalytics()
        {
            this.header.Disable(this._filters["GoogleAnalytics"]);
        }

        public void DisableTemplate()
        {
            this.header.Disable(this._filters["Template"]);
        }

        public void DisableBcc()
        {
            this.header.Disable(this._filters["Bcc"]);
        }

        public void DisableBypassListManagement()
        {
            this.header.Disable(this._filters["BypassListManagement"]);
        }

        public void EnableGravatar()
        {
            this.header.Enable(this._filters["Gravatar"]);
        }

        public void EnableOpenTracking()
        {
            this.header.Enable(this._filters["OpenTracking"]);
        }

        public void EnableClickTracking(string text = null)
        {
            var filter = this._filters["ClickTracking"];
                
            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
        }

        public void EnableSpamCheck(int score = 5, string url = null)
        {
            var filter = this._filters["SpamCheck"];

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "score" }, score.ToString(CultureInfo.InvariantCulture));
            this.header.AddFilterSetting(filter, new List<string>(){ "url" }, url);
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

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
            this.header.AddFilterSetting(filter, new List<string>(){ "replace"}, replace);
            this.header.AddFilterSetting(filter, new List<string>(){ "landing" }, landing);
        }

        public void EnableFooter(string text = null, string html = null)
        {
            var filter = this._filters["Footer"];

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "text" }, text);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableGoogleAnalytics(string source, string medium, string term, string content = null, string campaign = null)
        {
            var filter = this._filters["GoogleAnalytics"];

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "source " }, source);
            this.header.AddFilterSetting(filter, new List<string>(){ "medium" }, medium);
            this.header.AddFilterSetting(filter, new List<string>(){ "term" }, term);
            this.header.AddFilterSetting(filter, new List<string>(){ "content" }, content);
            this.header.AddFilterSetting(filter, new List<string>(){ "compaign" }, campaign);
        }

        public void EnableTemplate(string html)
        {
            var filter = this._filters["Template"];

            if (!System.Text.RegularExpressions.Regex.IsMatch(html, SendGrid.ReHtml))
            {
                throw new Exception("Missing substitution tag in html");
            }

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "html" }, html);
        }

        public void EnableBcc(string email)
        {
            var filter = this._filters["Bcc"];

            this.header.Enable(filter);
            this.header.AddFilterSetting(filter, new List<string>(){ "email" }, email);
        }

        public void EnableBypassListManagement()
        {
            this.header.Enable(this._filters["BypassListManagement"]);
        }

        public void Mail()
        {
            throw new NotImplementedException();
        }
    }
}
