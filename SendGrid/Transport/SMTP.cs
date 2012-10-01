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
        public const string SmtpServer = "smtp.sendgrid.net";

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
        private readonly ISmtpClient _client;

        /// <summary>
        /// Transport created to deliver messages to SendGrid using SMTP
        /// </summary>
        /// <param name="client">SMTP client we are wrapping</param>
        /// <param name="credentials">Sendgrid user credentials</param>
        /// <param name="host">MTA recieving this message.  By default, sent through SendGrid.</param>
        /// <param name="port">SMTP port 25 is the default.  Port 465 can be used for Secure SMTP.</param>
        private SMTP(ISmtpClient client, NetworkCredential credentials, string host = SmtpServer, int port = Port)
        {
            _client = client;
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
        public void Deliver(IMail message)
        {
            var mime = message.CreateMimeMessage();
            _client.Send(mime);
        }

        /// <summary>
        /// Transport created to deliver messages to SendGrid using SMTP
        /// </summary>
        /// <param name="credentials">Sendgrid user credentials</param>
        /// <param name="host">MTA recieving this message.  By default, sent through SendGrid.</param>
        /// <param name="port">SMTP port 25 is the default.  Port 465 can be used for Secure SMTP.</param>
        public static SMTP GetInstance(NetworkCredential credentials, string host = SmtpServer, Int32 port = Port)
        {
            var client = new SmtpWrapper(host, port, credentials, SmtpDeliveryMethod.Network);
            return new SMTP(client, credentials, host, port);
        }

        /// <summary>
        /// For Unit Testing Only!
        /// </summary>
        /// <param name="client"></param>
        /// <param name="credentials"></param>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        internal static SMTP GetInstance(ISmtpClient client, NetworkCredential credentials, string host = SmtpServer, Int32 port = Port)
        {
            return new SMTP(client, credentials, host, port);
        }

        /// <summary>
        /// Interface to allow testing
        /// </summary>
        internal interface ISmtpClient
        {
            bool EnableSsl { get; set; }
            void Send(MailMessage mime);
        }

        /// <summary>
        /// Implementation of SmtpClient wrapper, separated to allow dependency injection
        /// </summary>
        internal class SmtpWrapper : ISmtpClient
        {
            private readonly SmtpClient _client;
            public bool EnableSsl
            {
                get
                {
                    return _client.EnableSsl;
                }
                set
                {
                    _client.EnableSsl = value;
                }
            }

            public SmtpWrapper(string host, Int32 port, NetworkCredential credentials, SmtpDeliveryMethod deliveryMethod)
            {
                _client = new SmtpClient(host, port) { Credentials = credentials, DeliveryMethod = deliveryMethod };
            }

            public void Send(MailMessage mime)
            {
                _client.Send(mime);
            }
        }
    }
}
