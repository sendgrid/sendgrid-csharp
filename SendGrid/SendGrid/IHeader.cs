using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendGrid
{
    public interface IHeader
    {
        void AddTo(IEnumerable<String> recipients);
        void AddSubVal(String tag, IEnumerable<String> substitutions);
        void AddUniqueIdentifier(IDictionary<String, String> identifiers);
        void SetCategory(String category);
        void Enable(String filter);
        void Disable(String filter);
        void AddFilterSetting(String filter, IEnumerable<String> settings, String value);
        void AddHeader(MailMessage mime);
        String AsJson();
    }
}
