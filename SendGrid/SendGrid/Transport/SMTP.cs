using System;
using System.Net;
using System.Net.Mail;

namespace SendGrid.Transport
{
    /// <summary>
    /// Transport class for delivering messages via SMTP
    /// </summary>
    public class SMTP : ITransport
    {
        /// <summary>
        /// SendGrid's host name
        /// </summary>
        public const String SmtpServer = "smtp.sendgrid.net";

        /// <summary>
        /// Port for Simple Mail Transfer Protocol
        /// </summary>
        public const Int32 Port = 25;

        /// <summary>
        /// Port for Secure SMTP
        /// </summary>
        public const Int32 SslPort = 465;

        /// <summary>
        /// Port for TLS (currently not supported)
        /// </summary>
        public const Int32 TlsPort = 571;

        /// <summary>
        /// Client used to deliver SMTP message
        /// </summary>
        private readonly SmtpClient _client;

        /// <summary>
        /// Transport created to deliver messages to SendGrid using SMTP
        /// </summary>
        /// <param name="credentials">Sendgrid user credentials</param>
        /// <param name="host">MTA recieving this message.  By default, sent through SendGrid.</param>
        /// <param name="port">SMTP port 25 is the default.  Port 465 can be used for Secure SMTP.</param>
        public SMTP(NetworkCredential credentials, String host = SmtpServer, Int32 port = Port)
        {
            _client = new SmtpClient(host, port) {Credentials = credentials, DeliveryMethod = SmtpDeliveryMethod.Network};

            switch (port)
            {
                case Port:
                    break;
                case SslPort:
                    _client.EnableSsl = true;
                    break;
                case TlsPort:
                    throw new NotSupportedException("TLS not supported");
            }
        }

        /// <summary>
        /// Deliver an email using SMTP protocol
        /// </summary>
        /// <param name="message"></param>
        public void Deliver(ISendGrid message)
        {
            var mime = message.CreateMimeMessage();
            _client.Send(mime);
        }
    }
}
