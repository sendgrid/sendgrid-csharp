using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{

    /// <summary>
    /// Simplified email sending for common use cases
    /// </summary>
    public class MailHelper
    {
        /// <summary>
        /// Send a single simple email
        /// </summary>
        /// <param name="from">An email object that may contain the recipient’s name, but must always contain the sender’s email.</param>
        /// <param name="to">An email object that may contain the recipient’s name, but must always contain the recipient’s email.</param>
        /// <param name="subject">The subject of your email. This may be overridden by SetGlobalSubject().</param>
        /// <param name="plainTextContent">The text/plain content of the email body.</param>
        /// <param name="htmlContent">The text/html content of the email body.</param>
        /// <returns>A SendGridMessage object.</returns>
        public static SendGridMessage CreateSingleEmail(EmailAddress from,
                                                        EmailAddress to,
                                                        string subject,
                                                        string plainTextContent,
                                                        string htmlContent)
        {
            var msg = new SendGridMessage()
            {
                From = from,
                Personalizations = new List<Personalization>() {
                    new Personalization() {
                        Tos = new List<EmailAddress>() {
                            to
                        }
                    }
                },
                Subject = subject,
                PlainTextContent = plainTextContent,
                HtmlContent = htmlContent
            };
            return msg;
        }
    }
}