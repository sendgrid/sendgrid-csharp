using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using SendGrid.Transport;

namespace SendGrid
{

    /// <summary>
    /// 
    /// </summary>
    public class Mail : IMail
    {

        #region Private Members

        private static readonly Dictionary<string, string> Filters = InitializeFilters();
        private readonly MailMessage _message;

        // TODO find appropriate types for these
        const string Encoding = "quoted-printable";
        const string Charset = "utf-8";
        private const string ReText = @"<\%\s*\%>";
        private const string ReHtml = @"<\%\s*[^\s]+\s*\%>";
        private List<string> _attachments = new List<string>();
        private Dictionary<string, MemoryStream> _streamedAttachments = new Dictionary<string, MemoryStream>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets the attachment collection used to store data attached to this e-mail message. 
        /// </summary>
        public string[] Attachments
        {
            get { return _attachments.ToArray(); }
            set { _attachments = value.ToList(); }
        }

        /// <summary>
        /// Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message. 
        /// </summary>
        public MailAddress[] Bcc
        {
            get
            {
                return _message.Bcc.ToArray();
            }
            set
            {
                _message.Bcc.Clear();
                foreach (var mailAddress in value)
                {
                    _message.Bcc.Add(mailAddress);
                }
            }
        }

        /// <summary>
        /// Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message. 
        /// </summary>
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

        /// <summary>
        /// Gets or sets the from address for this e-mail message. 
        /// </summary>
        public MailAddress From
        {
            get { return _message.From; }
            set { if (value != null) _message.From = value; }
        }

        /// <summary>
        /// Gets the IHeader that is used to build the Headers that are transmitted with this e-mail message. 
        /// </summary>
        public IHeader Header { get; set; }

        /// <summary>
        /// Gets the e-mail headers that are transmitted with this e-mail message. 
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// The HTML Body for the message.
        /// </summary>
        public string Html { get; set; }

        /// <summary>
        /// Gets or sets the list of addresses to reply to for the mail message. 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, MemoryStream> StreamedAttachments
        {
            get { return _streamedAttachments; }
            set { _streamedAttachments = value; }
        }

        /// <summary>
        /// Gets or sets the subject line for this e-mail message. 
        /// </summary>
        public string Subject
        {
            get { return _message.Subject; }
            set { if (value != null) _message.Subject = value; }
        }

        /// <summary>
        /// The text body for the message.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  Gets the address collection that contains the recipients of this e-mail message.  
        /// </summary>
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

        #endregion

        #region Initialization and Constructors
        /// <summary>
        /// Creates an instance of SendGrid's custom message object
        /// </summary>
        /// <returns></returns>
        public static Mail GetInstance()
        {
            var header = new Header();
            return new Mail(header);
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
        public static Mail GetInstance(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc,
                                                string subject, string html, string text)
        {
            var header = new Header();
            return new Mail(from, to, cc, bcc, subject, html, text, header);
        }

        internal Mail(MailAddress from, MailAddress[] to, MailAddress[] cc, MailAddress[] bcc, 
            string subject, string html, string text, IHeader header = null ) : this(header)
        {
            From = from;
            To = to;
            Cc = cc;
            Bcc = bcc;

            _message.Subject = subject;

            Text = text;
            Html = html;
        }

        internal Mail(IHeader header)
        {
            _message = new MailMessage();
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

        #region Public Methods

        /// <summary>
        /// Add to the 'To' address.
        /// </summary>
        /// <param name="address">single string eg. 'you@company.com'</param>

        public void AddTo(string address)
        {
            var mailAddress = new MailAddress(address);
            _message.To.Add(mailAddress);
        }

        /// <summary>
        /// Add to the 'To' address.
        /// </summary>
        /// <param name="addresses">list of email addresses as strings</param>
        public void AddTo(IEnumerable<string> addresses)
        {
            if (addresses == null) return;
            foreach (var address in addresses.Where(address => address != null))
            {
                AddTo(address);
            }
        }

        /// <summary>
        /// Add to the 'To' address.
        /// </summary>
        /// <param name="addresssInfo"> the dictionary keys are the email addresses, which points to a dictionary of 
        /// key substitutionValues pairs mapping to other address codes, such as { foo@bar.com => { 'DisplayName' => 'Mr Foo' } } </param>
        public void AddTo(IDictionary<string, IDictionary<string, string>> addresssInfo)
        {
            foreach (var mailAddress in from address in addresssInfo.Keys let table = addresssInfo[address] select new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null))
            {
                _message.To.Add(mailAddress);
            }
        }

        /// <summary>
        /// Add to the 'CC' address.
        /// </summary>
        /// <param name="address">a single email address eg "you@company.com"</param>
        public void AddCc(string address)
        {
            var mailAddress = new MailAddress(address);
            _message.CC.Add(mailAddress);
        }

        /// <summary>
        /// Add to the 'CC' address.
        /// </summary>
        /// <param name="addresses">a list of email addresses as strings</param>
        public void AddCc(IEnumerable<string> addresses)
        {
            if (addresses == null) return;
            foreach (var address in addresses.Where(address => address != null))
            {
                AddCc(address);
            }
        }

        /// <summary>
        /// Add to the 'CC' address.
        /// </summary>
        /// <param name="addresssInfo">the dictionary keys are the email addresses, which points to a dictionary of 
        /// key substitutionValues pairs mapping to other address codes, such as { foo@bar.com => { 'DisplayName' => 'Mr Foo' } } </param>
        public void AddCc(IDictionary<string, IDictionary<string, string>> addresssInfo)
        {
            foreach (var mailAddress in from address in addresssInfo.Keys let table = addresssInfo[address] select new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null))
            {
                _message.CC.Add(mailAddress);
            }
        }

        /// <summary>
        /// Add to the 'Bcc' address.
        /// </summary>
        /// <param name="address">a single email as the input eg "you@company.com"</param>
        public void AddBcc(string address)
        {
            var mailAddress = new MailAddress(address);
            _message.Bcc.Add(mailAddress);
        }

        /// <summary>
        /// Add to the 'Bcc' address.
        /// </summary>
        /// <param name="addresses">a list of emails as an array of strings.</param>
        public void AddBcc(IEnumerable<string> addresses)
        {
            if (addresses == null) return;
            foreach (var address in addresses.Where(address => address != null))
            {
                AddBcc(address);
            }
        }

        /// <summary>
        /// Add to the 'Bcc' address.
        /// </summary>
        /// <param name="addresssInfo">the dictionary keys are the email addresses, which points to a dictionary of 
        /// key substitutionValues pairs mapping to other address codes, such as { foo@bar.com => { 'DisplayName' => 'Mr Foo' } }</param>
        public void AddBcc(IDictionary<string, IDictionary<string, string>> addresssInfo)
        {
            foreach (var address in addresssInfo.Keys)
            {
                var table = addresssInfo[address];
                //DisplayName is the only option that this implementation of MailAddress implements.
                var mailAddress = new MailAddress(address, table.ContainsKey("DisplayName") ? table["DisplayName"] : null);
                _message.Bcc.Add(mailAddress);
            }
        }

        /// <summary>
        /// Defines a mapping between a replacement string in the text of the message to a list of 
        /// substitution values to be used, one per each recipient, in the same order as the recipients were added. 
        /// </summary>
        /// <param name="replacementTag">the string in the email that you'll replace eg. '-name-'</param>
        /// <param name="substitutionValues">a list of values that will be substituted in for the replacementTag, one for each recipient</param>
        public void AddSubVal(string replacementTag, List<string> substitutionValues)
        {
            //let the system complain if they do something bad, since the function returns null
            Header.AddSubVal(replacementTag, substitutionValues);
        }

        /// <summary>
        /// This adds parameters and values that will be passed back through SendGrid's
        /// Event API if an event notification is triggered by this email.
        /// </summary>
        /// <param name="identifiers">parameter substitutionValues pairs to be passed back on event notification</param>
        public void AddUniqueIdentifiers(IDictionary<string, string> identifiers)
        {
            Header.AddUniqueIdentifier(identifiers);
        }

        /// <summary>
        /// This sets the category for this email.  Statistics are stored on a per category
        /// basis, so this can be useful for tracking on a per group basis.
        /// </summary>
        /// <param name="category">categories applied to the message</param>
        public void SetCategory(string category)
        {
            Header.SetCategory(category);
        }

        /// <summary>
        /// Add a stream as an attachment to the message
        /// </summary>
        /// <param name="stream">Stream of file to be attached</param>
        /// <param name="name">Name of file to be attached</param>
        public void AddAttachment(Stream stream, string name)
        {
            var ms = new MemoryStream();
            stream.CopyTo(ms);
            StreamedAttachments[name] = ms;
        }

        /// <summary>
        /// Add an attachment to the message.
        /// </summary>
        /// <param name="filePath">a fully qualified file path as a string</param>
        public void AddAttachment(string filePath)
        {
            _attachments.Add(filePath);
        }

        /// <summary>
        /// GetRecipients returns a list of all the recipients by retrieving the to, cc, and bcc lists.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetRecipients()
        {
            List<MailAddress> tos = _message.To.ToList();
            List<MailAddress> ccs = _message.CC.ToList();
            List<MailAddress> bccs = _message.Bcc.ToList();

            var rcpts = tos.Union(ccs.Union(bccs)).Select(address => address.Address);
            return rcpts;
        }

        /// <summary>
        /// Add custom headers to the message
        /// </summary>
        /// <param name="headers">key substitutionValues pairs</param>
        public void AddHeaders(IDictionary<string, string> headers)
        {
            headers.Keys.ToList().ForEach(key => Headers[key] = headers[key]);
        }

        /// <summary>
        /// Disable the gravatar app
        /// </summary>
        public void DisableGravatar()
        {
            Header.Disable(Filters["Gravatar"]);
        }

        /// <summary>
        /// Disable the open tracking app
        /// </summary>
        public void DisableOpenTracking()
        {
            Header.Disable(Filters["OpenTracking"]);
        }

        /// <summary>
        /// Disable the click tracking app
        /// </summary>
        public void DisableClickTracking()
        {
            Header.Disable(Filters["ClickTracking"]);
        }

        /// <summary>
        /// Disable the spam check
        /// </summary>
        public void DisableSpamCheck()
        {
            Header.Disable(Filters["SpamCheck"]);
        }

        /// <summary>
        /// Disable the unsubscribe app
        /// </summary>
        public void DisableUnsubscribe()
        {
            Header.Disable(Filters["Unsubscribe"]);
        }

        /// <summary>
        /// Disable the footer app
        /// </summary>
        public void DisableFooter()
        {
            Header.Disable(Filters["Footer"]);
        }

        /// <summary>
        /// Disable the Google Analytics app
        /// </summary>
        public void DisableGoogleAnalytics()
        {
            Header.Disable(Filters["GoogleAnalytics"]);
        }

        /// <summary>
        /// Disable the templates app
        /// </summary>
        public void DisableTemplate()
        {
            Header.Disable(Filters["Template"]);
        }

        /// <summary>
        /// Disable Bcc app
        /// </summary>
        public void DisableBcc()
        {
            Header.Disable(Filters["Bcc"]);
        }

        /// <summary>
        /// Disable the Bypass List Management app
        /// </summary>
        public void DisableBypassListManagement()
        {
            Header.Disable(Filters["BypassListManagement"]);
        }

        /// <summary>
        /// Inserts the gravatar image of the sender to the bottom of the message
        /// </summary>
        public void EnableGravatar()
        {
            Header.Enable(Filters["Gravatar"]);
        }

        /// <summary>
        /// Adds an invisible image to the end of the email which can track e-mail opens.
        /// </summary>
        public void EnableOpenTracking()
        {
            Header.Enable(Filters["OpenTracking"]);
        }

        /// <summary>
        /// Causes all links to be overwritten, shortened, and pointed to SendGrid's servers so clicks will be tracked.
        /// </summary>
        /// <param name="includePlainText">true if links found in plain text portions of the message are to be overwritten</param>
        public void EnableClickTracking(bool includePlainText = false)
        {
            var filter = Filters["ClickTracking"];
                
            Header.Enable(filter);
            if (includePlainText)
            {
                Header.AddFilterSetting(filter, new List<string> { "enable_text" }, "1");
            }
        }

        /// <summary>
        /// Provides notification when emails are detected that exceed a predefined spam threshold.
        /// </summary>
        /// <param name="score">Emails with a SpamAssassin score over this substitutionValues will be considered spam and not be delivered.</param>
        /// <param name="url">SendGrid will send an HTTP POST request to this url when a message is detected as spam</param>
        public void EnableSpamCheck(int score = 5, string url = null)
        {
            var filter = Filters["SpamCheck"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "maxscore" }, score.ToString(CultureInfo.InvariantCulture));
            Header.AddFilterSetting(filter, new List<string> { "url" }, url);
        }

        /// <summary>
        /// Allows SendGrid to manage unsubscribes and ensure these users don't get future emails from the sender
        /// </summary>
        /// <param name="text">string for the plain text email body showing what you want the message to look like.</param>
        /// <param name="html">string for the HTML email body showing what you want the message to look like.</param>
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

        /// <summary>
        /// Allows SendGrid to manage unsubscribes and ensure these users don't get future emails from the sender
        /// </summary>
        /// <param name="replace">Tag in the message body to be replaced with the unsubscribe link and message</param>
        public void EnableUnsubscribe(string replace)
        {
            var filter = Filters["Unsubscribe"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "replace" }, replace);
        }

        /// <summary>
        /// Attaches a message at the footer of the email
        /// </summary>
        /// <param name="text">Message for the plain text body of the email</param>
        /// <param name="html">Message for the HTML body of the email</param>
        public void EnableFooter(string text = null, string html = null)
        {
            var filter = Filters["Footer"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "text/plain" }, text);
            Header.AddFilterSetting(filter, new List<string> { "text/html" }, html);
        }

        /// <summary>
        /// Re-writes links to integrate with Google Analytics
        /// </summary>
        /// <param name="source">Name of the referrer source (e.g. Google, SomeDomain.com, NewsletterA)</param>
        /// <param name="medium">Name of the marketing medium (e.g. Email)</param>
        /// <param name="term">Identify paid keywords</param>
        /// <param name="content">Use to differentiate ads</param>
        /// <param name="campaign">Name of the campaign</param>
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

        /// <summary>
        /// Wraps an HTML template around your email content.
        /// </summary>
        /// <param name="html">HTML that your emails will be wrapped in, containing a body replacementTag.</param>
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

        /// <summary>
        /// Automatically sends a blind carbon copy to an address for every e-mail sent, without 
        /// adding that address to the header.
        /// </summary>
        /// <param name="email">A single email recipient</param>
        public void EnableBcc(string email)
        {
            var filter = Filters["Bcc"];

            Header.Enable(filter);
            Header.AddFilterSetting(filter, new List<string> { "email" }, email);
        }

        /// <summary>
        /// Enabing this app will bypass the normal unsubscribe / bounce / spam report checks 
        /// and queue the e-mail for delivery.
        /// </summary>
        public void EnableBypassListManagement()
        {
            Header.Enable(Filters["BypassListManagement"]);
        }

        /// <summary>
        /// Used by the Transport object to create a MIME for SMTP
        /// </summary>
        /// <returns>MIME to be sent.</returns>
        public MailMessage CreateMimeMessage()
        {
            string smtpapi = Header.AsJson();

            if (!string.IsNullOrEmpty(smtpapi))
                _message.Headers.Add("X-Smtpapi", smtpapi);

            Headers.Keys.ToList().ForEach(k => _message.Headers.Add(k, Headers[k]));

            _message.Attachments.Clear();
            _message.AlternateViews.Clear();

            if(Attachments != null)
            {
                foreach (var attachment in Attachments)
                {
                    _message.Attachments.Add(new Attachment(attachment, MediaTypeNames.Application.Octet));
                }                
            }

            if(StreamedAttachments != null)
            {
                foreach (var attachment in StreamedAttachments)
                {
                    attachment.Value.Position = 0;
                    _message.Attachments.Add(new Attachment(attachment.Value, attachment.Key));
                }
            }

            if (Text != null)
            {
                AlternateView plainView = AlternateView.CreateAlternateViewFromString(Text, null, "text/plain");
                _message.AlternateViews.Add(plainView);
            }

            if (Html != null)
            {
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(Html, null, "text/html");
                _message.AlternateViews.Add(htmlView);
            }
            
            //message.SubjectEncoding = Encoding.GetEncoding(charset);
            //message.BodyEncoding = Encoding.GetEncoding(charset);

            return _message;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Helper function lets us look at the mime before it is sent
        /// </summary>
        /// <param name="directory">directory in which we store this mime message</param>
        internal void SaveMessage(string directory)
        {
            var client = new SmtpClient("localhost")
                             {
                                 DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                                 PickupDirectoryLocation = @"C:\temp"
                             };
            var msg = CreateMimeMessage();
            client.Send(msg);
        }

        #endregion

    }
}
