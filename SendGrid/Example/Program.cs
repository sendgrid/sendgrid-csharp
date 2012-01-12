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
        static void Main(string[] args)
        {
            var username = "cjbuchmann";
            var password = "Gadget_15";
            var from = "cj.buchmann@sendgrid.com";
            var to = new List<String>
                         {
                             "cj.buchmann@gmail.com"
                         };

            //initialize the SMTPAPI example class
            var smtpapi = new SMTPAPI(username, password, from, to);

            //send a simple HTML encoded email.
            //smtpapi.SimpleHTMLEmail();

            //send a simple Plaintext email.
            //smtpapi.SimplePlaintextEmail();

            //send a gravatar enabled email.
            smtpapi.EnableGravatarEmail();
        }
    }
}
