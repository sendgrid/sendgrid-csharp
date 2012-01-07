using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendGrid
{
    public class SendGrid : ISendGrid
    {
        public SendGrid(IHeader header)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void DisableOpenTracking()
        {
            throw new NotImplementedException();
        }

        public void DisableClickTracking()
        {
            throw new NotImplementedException();
        }

        public void DisableSpamCheck()
        {
            throw new NotImplementedException();
        }

        public void DisableUnsubscribe()
        {
            throw new NotImplementedException();
        }

        public void DisableFooter()
        {
            throw new NotImplementedException();
        }

        public void DisableGoogleAnalytics()
        {
            throw new NotImplementedException();
        }

        public void DisableTemplate()
        {
            throw new NotImplementedException();
        }

        public void DisableBcc()
        {
            throw new NotImplementedException();
        }

        public void DisableBipassListManaement()
        {
            throw new NotImplementedException();
        }

        public void EnableGravatar()
        {
            throw new NotImplementedException();
        }

        public void EnableOpenTracking()
        {
            throw new NotImplementedException();
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

        public void EnableBipassListManaement()
        {
            throw new NotImplementedException();
        }

        public void Mail()
        {
            throw new NotImplementedException();
        }
    }
}
