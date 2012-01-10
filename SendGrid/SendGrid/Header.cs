using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace SendGrid
{
    public class Header : IHeader
    {
        public void AddTo(IEnumerable<string> recipients)
        {
            throw new NotImplementedException();
        }

        public void AddSubVal(string tag, IEnumerable<string> substitutions)
        {
            throw new NotImplementedException();
        }

        public void AddUniqueIdentifier(IDictionary<string, string> identifiers)
        {
            throw new NotImplementedException();
        }

        public void SetCategory(string category)
        {
            throw new NotImplementedException();
        }

        public void Enable(string filter)
        {
            throw new NotImplementedException();
        }

        public void Disable(string filter)
        {
            throw new NotImplementedException();
        }

        public void AddFilterSetting(string filter, IEnumerable<string> settings, string value)
        {
            throw new NotImplementedException();
        }

        public void AddHeader(MailMessage mime)
        {
            throw new NotImplementedException();
        }

        public String AsJson()
        {
            throw new NotImplementedException();
        }
    }
}
