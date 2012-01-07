using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendGrid
{
    public interface ISendGrid
    {
        #region Properties
        String From { get; set; }
        String To { get; set; }
        String Cc { get; set; }
        String Bcc { get; set; }
        String Subject { get; set; }
        IHeader Header { get; set; }
        String Html { get; set; }
        String Text { get; set; }
        String Transport { get; set; }
        String Date { get; set; }
        #endregion

        #region Interface for ITransport
        MailMessage CreateMimeMessage();
        #endregion

        #region Methods for setting data
        void AddTo(String address);
        void AddTo(IEnumerable<String> addresses);
        void AddTo(IDictionary<String, String> addresssInfo);
        void AddTo(IEnumerable<IDictionary<String, String>> addressesInfo);

        void AddCc(String address);
        void AddCc(IEnumerable<String> addresses);
        void AddCc(IDictionary<String, String> addresssInfo);
        void AddCc(IEnumerable<IDictionary<String, String>> addressesInfo);

        void AddBcc(String address);
        void AddBcc(IEnumerable<String> addresses);
        void AddBcc(IDictionary<String, String> addresssInfo);
        void AddBcc(IEnumerable<IDictionary<String, String>> addressesInfo);

        void AddRcpts(String address);
        void AddRcpts(IEnumerable<String> addresses);
        void AddRcpts(IDictionary<String, String> addresssInfo);
        void AddRcpts(IEnumerable<IDictionary<String, String>> addressesInfo);

        void AddSubVal(String tag, String value);

        void AddAttachment(String filePath);
        void AddAttachment(Attachment attachment);

        String GetMailFrom();
        IEnumerable<String> GetRecipients();

        String Get(String field);
        void Set(String field, String value);
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
        void DisableBipassListManaement();

        void EnableGravatar();
        void EnableOpenTracking();
        void EnableClickTracking(String text = null);
        void EnableSpamCheck(int score = 5, String url = null);
        void EnableUnsubscribe(String text, String html, String replace, String url, String landing);
        void EnableFooter(String text = null, String html = null);
        void EnableGoogleAnalytics(String source, String medium, String term, String content = null, String campaign = null);
        void EnableTemplate(String html = null);
        void EnableBcc(String email = null);
        void EnableBipassListManaement();
        #endregion

        void Mail();
    }
}
