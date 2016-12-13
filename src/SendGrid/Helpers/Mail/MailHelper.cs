using System.Collections.Generic;

namespace SendGrid.Helpers.Mail
{

    public class MailHelper
    {
        public static SendGridMessage CreateSingleEmail(EmailAddress from,
                                                        EmailAddress to,
                                                        string subject,
                                                        string contentText,
                                                        string contentHtml)
        {
            var msg = new SendGridMessage()
            {
                From = from,
                Personalization = new List<Personalization>() {
                    new Personalization() {
                        Tos = new List<EmailAddress>() {
                            to
                        }
                    }
                },
                Subject = subject,
                Contents = new List<Content>() {
                    new PlainTextContent(contentText),
                    new HtmlContent(contentHtml)
                }
            };
            return msg;
        }
    }
}