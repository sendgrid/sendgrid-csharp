using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SendGrid.Helpers.Mail
{

    public class Mail
    {
        private Client _client;

        public Mail(Client client)
        {
            _client = client;
        }

        public async Task<Response> SendEmailAsync(SendGridMessage msg)
        {
            return await _client.RequestAsync(Client.Method.POST,
                                              msg.Get(),
                                              urlPath: "mail/send").ConfigureAwait(false);
        }

        public async Task<Response> SendSingleEmailAsync(MailAddress from,
                                                         MailAddress to,
                                                         string subject,
                                                         string contentText,
                                                         string contentHTML,
                                                         SendGridMessage msg = null)
        {
            if(msg == null)
            {
                msg = new SendGridMessage();
            }
            msg.From = from;
            msg.Subject = subject;
            var text = new Content(MimeType.Text, contentText);
            msg.AddContent(text);
            var html = new Content(MimeType.HTML, contentHTML);
            msg.AddContent(html);
            var p = new Personalization();
            p.AddTo(to);
            msg.AddPersonalization(p);
            return await SendEmailAsync(msg).ConfigureAwait(false);
        }
    }
}
