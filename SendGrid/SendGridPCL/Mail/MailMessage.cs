using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid {
    public class MailMessage {

        public MailMessage() {
            ReplyToList = new List<MailAddress>();
            To = new List<MailAddress>();
            CC = new List<MailAddress>();
            Bcc = new List<MailAddress>();
            Attachments = new List<Attachment>();
            AlternateViews = new List<AlternateView>();

            Headers = new SendGridPCL.Collections.NameValueCollection();
        }

        public MailAddress From { get; set; }
        public MailAddress ReplyTo {
            get {
                return ReplyToList.FirstOrDefault();
            }
            set {
                ReplyToList.Clear();
                ReplyToList.Add(value);
            }
        }
        public string Subject { get; set; }
        public List<MailAddress> ReplyToList { get; set; }

        public List<MailAddress> To { get; set; }
        public List<MailAddress> CC { get; set; }
        public List<MailAddress> Bcc { get; set; }

        public List<Attachment> Attachments { get; set; }
        public List<AlternateView> AlternateViews { get; set; }

        public SendGridPCL.Collections.NameValueCollection Headers { get; set; }

    }
}
