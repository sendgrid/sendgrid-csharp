using System;
using System.Collections.Generic;
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
            var from = "cj.buchmann@sendgrid.com";
            var to = new List<String>
                         {
                             "cj.buchmann@sendgrid.com"
                         };

            //initialize the SMTPAPI example class
            var smtpapi = new SMTPAPI(username, password, from, to);
            var restapi = new RESTAPI(username, password, from, to);

            //use this section to test out our REST and SMTP examples!
            restapi.EnableTemplateEmail();

            Console.WriteLine("Done!");
            Console.ReadLine();
        }
    }
}
