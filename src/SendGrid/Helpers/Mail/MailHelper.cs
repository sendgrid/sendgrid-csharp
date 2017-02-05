using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{

    public class MailHelper
    {
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