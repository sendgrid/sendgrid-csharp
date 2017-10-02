using SendGrid.ASPSamples.Models;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;

namespace SendGrid.ASPSamples
{
    public class SendGridEmailService
    {
        private readonly SendGridClient _client;
        private string apiKey = ConfigurationManager.AppSettings["SendGridApiKey"];
        private static readonly string MessageId = "X-Message-Id";

        public SendGridEmailService()
        {
            _client = new SendGridClient(apiKey);
        }

        public EmailResponse Send(EmailContract contract)
        {

            var emailMessage = new SendGridMessage()
            {
                From = new EmailAddress(contract.FromEmailAddress, contract.Alias),
                Subject = contract.Subject,
                HtmlContent = contract.Body
            };

            emailMessage.AddTo(new EmailAddress(contract.ToEmailAddress));
            if (!string.IsNullOrWhiteSpace(contract.BccEmailAddress))
            {
                emailMessage.AddBcc(new EmailAddress(contract.BccEmailAddress));
            }

            if (!string.IsNullOrWhiteSpace(contract.CcEmailAddress))
            {
                emailMessage.AddBcc(new EmailAddress(contract.CcEmailAddress));
            }

            return ProcessResponse(_client.SendEmailAsync(emailMessage).Result);
        }

        private EmailResponse ProcessResponse(Response response)
        {
            if (response.StatusCode.Equals(System.Net.HttpStatusCode.Accepted)
                || response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
            {
                return ToMailResponse(response);
            }

            //TODO check for null
            var errorResponse = response.Body.ReadAsStringAsync().Result;

            throw new EmailServiceException(response.StatusCode.ToString(), errorResponse);
        }

        private static EmailResponse ToMailResponse(Response response)
        {
            if (response == null)
                return null;

            var headers = (HttpHeaders)response.Headers;
            var messageId = headers.GetValues(MessageId).FirstOrDefault();
            return new EmailResponse()
            {
                UniqueMessageId = messageId,
                DateSent = DateTime.UtcNow,
            };
        }
    }
}