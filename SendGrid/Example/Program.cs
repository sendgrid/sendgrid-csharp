using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using SendGridMail;
using SendGridMail.Transport;

namespace Example
{
    class Program
    {
        // this code is used for the SMTPAPI examples
        static void Main(string[] args)
        {
            var username = "sgrid_username";
            var password = "sgrid_password";
            var from = "bar@domain.com";
            var to = new List<String>
                         {
                             "foo@domain.com",
                             "raz@domain.com"
                         };

            //initialize the SMTPAPI example class
            var smtpapi = new SMTPAPI(username, password, from, to);
            var restapi = new WEBAPI(username, password, from, to);

            //use this section to test out our Web and SMTP examples!
            smtpapi.SimpleHTMLEmail();
			restapi.SimpleHTMLEmail();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }

    }
}
