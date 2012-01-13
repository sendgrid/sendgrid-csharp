using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using SendGridMail;
using SendGridMail.Transport;

namespace Example
{
    class RESTAPI
    {
        private String _username;
        private String _password;
        private String _from;
        private IEnumerable<String> _to;
        private IEnumerable<String> _bcc;  
        private IEnumerable<String> _cc;

        public RESTAPI(String username, String password, String from, IEnumerable<String> recipients, IEnumerable<String> bcc, IEnumerable<String> cc)
        {
            _username = username;
            _password = password;
            _from = from;
            _to = recipients;
            _bcc = bcc;
            _cc = cc;
        }

        public void SimpleHTMLEmail()
        {
            //create a new message object
            var message = new SendGrid(new Header());

            //set the message recipients

            if(_to != null)
            {
                foreach (String recipient in _to)
                {
                    message.AddTo(recipient);
                }
            }


            if(_bcc != null)
            {
                foreach (String blindcc in _bcc)
                {
                    message.AddBcc(blindcc);
                }
            }
            
            if(_cc != null)
            {
                foreach (String cc in _cc)
                {
                    message.AddCc(cc);
                }
            }
            

            //set the sender
            message.From = new MailAddress(_from);

            //set the message body
            message.Html = "<html><p>Hello</p><p>World</p></html>";

            //set the message subject
            message.Subject = "Hello World Simple Test";

            //create an instance of the SMTP transport mechanism
            var restInstance = new REST(new NetworkCredential(_username, _password));

            //send the mail
            restInstance.Deliver(message);
        }
    }
}
