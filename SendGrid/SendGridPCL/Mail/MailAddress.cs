using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SendGrid {
    public class MailAddress {

        public MailAddress(string address, string displayName) {
            Address = address;
            DisplayName = displayName;
        }

        public MailAddress(string address) {
            Address = address;
        }

        public string Address { get; set; }
        public string DisplayName { get; set; }
    }
}
