using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SendGridMail
{
    public enum TransportType
    {
        SMTP,
        REST
    };

    public interface ISendGrid
    {
        #region Properties
        MailAddress From { get; set; }
        MailAddress[] To { get; set; }
        MailAddress[] Cc { get; }
        MailAddress[] Bcc { get; }
        String Subject { get; set; }
        IHeader Header { get; set; }
        String Html { get; set; }
        String Text { get; set; }
        TransportType Transport { get; set; }
        #endregion

        #region Interface for ITransport
        MailMessage CreateMimeMessage();
        #endregion

        #region Methods for setting data
        void AddTo(String address);
        void AddTo(IEnumerable<String> addresses);
        void AddTo(IDictionary<String, IDictionary<String, String>> addresssInfo);

        void AddCc(String address);
        void AddCc(IEnumerable<String> addresses);
        void AddCc(IDictionary<String, IDictionary<String, String>> addresssInfo);

        void AddBcc(String address);
        void AddBcc(IEnumerable<String> addresses);
        void AddBcc(IDictionary<String, IDictionary<String, String>> addresssInfo);

        void AddSubVal(String tag, String value);

        void AddAttachment(String filePath);
        void AddAttachment(Attachment attachment);
        void AddAttachment(Stream attachment, ContentType type);

        IEnumerable<String> GetRecipients();
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
