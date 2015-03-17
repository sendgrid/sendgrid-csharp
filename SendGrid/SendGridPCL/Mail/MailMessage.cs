using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid {
    public class MailMessage {
        public MailAddress From { get; set; }
        public MailAddress ReplyTo { get; set; }
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
