using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SendGridMail
{
    /// <summary>
    /// Internal object to represent the way in which email may be sent.
    /// The library supports sending through either SMTP or REST interfaces.
    /// </summary>
    public enum TransportType
    {
        SMTP,
        REST
    };

    /// <summary>
    /// Represents the basic set of functions that will be called by the user
    /// includes basic message data manipulation and filter settings
    /// </summary>
    public interface ISendGrid
    {
        #region Properties
        MailAddress From { get; set; }
        MailAddress[] To { get; set; }
        MailAddress[] Cc { get; }
        MailAddress[] Bcc { get; }
        MailAddress[] ReplyTo { get; set; }
        Attachment[] Attachments { get; set; }
        String Subject { get; set; }
        Dictionary<String, String> Headers { get; set; }
        IHeader Header { get; set; }
        String Html { get; set; }
        String Text { get; set; }
        TransportType Transport { get; set; }
        #endregion

        #region Interface for ITransport
        MailMessage CreateMimeMessage();
        #endregion

        #region Methods for setting data
        /// <summary>
        /// Set the To address.  In this case the input is a single string eg. 'you@company.com'
        /// </summary>
        /// <param name="address"></param>
        void AddTo(String address);
        /// <summary>
        /// Set the To address.  In this case the expected input is a list of addresses as strings
        /// </summary>
        /// <param name="addresses"></param>
        void AddTo(IEnumerable<String> addresses);
        /// <summary>
        /// Set the To address.  In this case the expected input is a list of addresses with paramaters.
        /// The first parameter is a string for the email address. 
        /// The second paramater is a dictionary containing the key, value pairs for a property's name and its value.
        /// Currently the only value that you can set with the MailAddress data object is the 'DisplayName' field. 
        /// </summary>
        /// <param name="addresssInfo"></param>
        void AddTo(IDictionary<String, IDictionary<String, String>> addresssInfo);

        /// <summary>
        /// Add the CC address.  In this case the expected input is a single email address eg "you@company.com"
        /// </summary>
        /// <param name="address"></param>
        void AddCc(String address);
        /// <summary>
        /// Set the CC address.  In this case the expected input is a list of addresses as strings
        /// </summary>
        /// <param name="addresses"></param>
        void AddCc(IEnumerable<String> addresses);
        /// <summary>
        /// Set the CC address.  In this case the expected input is a list of addresses with paramaters.
        /// The first parameter is a string for the email address.
        /// The second paramater is a dictionary containing the key, value pairs for a property's name and its value.
        /// Currently the only value that you can set with the MailAddress data object is the 'DisplayName' field.
        /// </summary>
        /// <param name="addresssInfo"></param>
        void AddCc(IDictionary<String, IDictionary<String, String>> addresssInfo);

        /// <summary>
        /// Set the Bcc address.  Expects a single email as the input eg "you@company.com"
        /// </summary>
        /// <param name="address"></param>
        void AddBcc(String address);
        /// <summary>
        /// Set the Bcc address.  Expects a list of emails as an array of strings.
        /// </summary>
        /// <param name="addresses"></param>
        void AddBcc(IEnumerable<String> addresses);
        /// <summary>
        /// Set the Bcc address.  In this case the expected input is a list of addresses with paramaters.
        /// The first parameter is a string for the email address.
        /// The second paramater is a dictionary containing the key, value pairs for a property's name and its value.
        /// Currently the only value that you can set with the MailAddress data object is the 'DisplayName' field.
        /// </summary>
        /// <param name="addresssInfo"></param>
        void AddBcc(IDictionary<String, IDictionary<String, String>> addresssInfo);

        /// <summary>
        /// Sets a list of substitution values for the email.
        /// the 'tag' parameter is the value in the email that you'll replace eg. '-name-'
        /// the 'value' parameter is a list of values that will be substituted in for the tag.
        /// In our example above the list would be something like ['eric', 'tyler', 'cj'].
        /// The 'value' parameter expects a 1:1 mapping from email addresses to values.  
        /// If you are left with more email addresses then values to substitute then the substitution tag will be left blank.
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="value"></param>
        void AddSubVal(String tag, params String[] value);

        /// <summary>
        /// Add an attachment to the message.  
        /// The 'filePath' paramater expects a fully qualified file path as a string
        /// </summary>
        /// <param name="filePath"></param>
        void AddAttachment(String filePath);
        /// <summary>
        /// Add an attachment to the message
        /// The 'attachment' paramater expects an argument of the Attachment type.
        /// </summary>
        /// <param name="attachment"></param>
        void AddAttachment(Attachment attachment);
        /// <summary>
        /// Add an attachment to the message
        /// The 'attachment' paramater expects an argument of the Attachment type.
        /// The 'type' parameter expects an argument of the ContentType type.
        /// </summary>
        /// <param name="attachment"></param>
        /// <param name="type"></param>
        void AddAttachment(Stream attachment, ContentType type);

        /// <summary>
        /// GetRecipients returns a list of all the recepients by retrieving the to, cc, and bcc lists.
        /// </summary>
        /// <returns></returns>
        IEnumerable<String> GetRecipients();

        /// <summary>
        /// Add custom headers to the message
        /// </summary>
        /// <param name="headers">key value pairs</param>
        void AddHeaders(IDictionary<String, String> headers);
        #endregion

        #region SMTP API Functions
        void DisableGravatar();
        void DisableOpenTracking();
        void DisableClickTracking();
        void DisableSpamCheck();
        void DisableUnsubscribe();
        void DisableFooter();
        void DisableGoogleAnalytics();
        void DisableTemplate();
        void DisableBcc();
        void DisableBypassListManagement();

        void EnableGravatar();
        void EnableOpenTracking();
        void EnableClickTracking(String text = null);
        void EnableSpamCheck(int score = 5, String url = null);
        void EnableUnsubscribe(String text, String html, String replace, String url, String landing);
        void EnableFooter(String text = null, String html = null);
        void EnableGoogleAnalytics(String source, String medium, String term, String content = null, String campaign = null);
        void EnableTemplate(String html = null);
        void EnableBcc(String email = null);
        void EnableBypassListManagement();
        #endregion

        void Mail();
    }
}
