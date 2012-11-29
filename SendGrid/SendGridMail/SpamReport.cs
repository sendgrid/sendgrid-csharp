using System;
using System.Linq;

namespace SendGridMail
{
    public class SpamReport
    {
        public String IP { get; set; }

        public String Email { get; set; }

        public DateTime Created { get; set; }
    }
}