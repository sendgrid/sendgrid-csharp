using System.Collections.Generic;
using System.Text;

namespace Inbound.Models
{
    /// <summary>
    /// The parsed information about an email sent by SendGrid via a webhook.
    /// </summary>
    public class InboundEmail
    {
        /// <summary>
        /// Gets or sets the headers of the email.
        /// </summary>
        /// <value>
        /// The headers.
        /// </value>
        public KeyValuePair<string, string>[] Headers { get; set; }

        /// <summary>
        /// Gets or sets a string containing the verification results of any DKIM and domain keys signatures in the message.
        /// </summary>
        /// <value>
        /// The dkim.
        /// </value>
        public string Dkim { get; set; }

        /// <summary>
        /// Gets or sets the email recipient field, as taken from the message headers.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public InboundEmailAddress[] To { get; set; }

        /// <summary>
        /// Gets or sets the carbon copy recipient field, as taken from the message headers.
        /// </summary>
        /// <value>
        /// To.
        /// </value>
        public InboundEmailAddress[] Cc { get; set; }

        /// <summary>
        /// Gets or sets the HTML body of email. If not set, email did not have a HTML body.
        /// </summary>
        /// <value>
        /// The HTML.
        /// </value>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the TEXT body of email .If not set, email did not have a TEXT body.
        /// </summary>
        /// <value>
        /// The TEXT.
        /// </value>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets from the email sender, as taken from the message headers.
        /// </summary>
        /// <value>
        /// From.
        /// </value>
        public InboundEmailAddress From { get; set; }

        /// <summary>
        /// Gets or sets the sender's ip address.
        /// </summary>
        /// <value>
        /// The sender ip.
        /// </value>
        public string SenderIp { get; set; }

        /// <summary>
        /// Gets or sets the Spam Assassin's spam report.
        /// </summary>
        /// <value>
        /// The spam report.
        /// </value>
        public string SpamReport { get; set; }

        /// <summary>
        /// Gets or sets the SMTP envelope.
        /// This will have two variables:
        /// - to, which is a single-element array containing the address that we received the email to,
        /// - from, which is the return path for the message.
        /// </summary>
        /// <value>
        /// The envelope.
        /// </value>
        public InboundEmailEnvelope Envelope { get; set; }

        /// <summary>
        /// Gets or sets the email subject.
        /// </summary>
        /// <value>
        /// The subject.
        /// </value>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Spam Assassin’s rating for whether or not this is spam.
        /// </summary>
        /// <value>
        /// The spam score.
        /// </value>
        public string SpamScore { get; set; }

        /// <summary>
        /// Gets or sets the attachment.
        /// </summary>
        /// <value>
        /// The attachment.
        /// </value>
        public InboundEmailAttachment[] Attachments { get; set; }

        /// <summary>
        /// Gets or sets the character sets of the fields extracted from the message.
        /// </summary>
        /// <value>
        /// The charsets.
        /// </value>
        public KeyValuePair<string, Encoding>[] Charsets { get; set; }

        /// <summary>
        /// Gets or sets the results of the Sender Policy Framework verification of the message sender
        /// and receiving IP address.
        /// </summary>
        /// <value>
        /// The SPF.
        /// </value>
        public string Spf { get; set; }

        /// <summary>
        /// Get or sets the raw email received
        /// </summary>
        /// <value>
        /// RAW EMAIL
        /// </value>
        public string RawEmail { get; set; }
    }


}
