using System;
using SendGrid.Interfaces;
using SendGrid.Models;
using System.Threading.Tasks;

namespace SendGrid.Concrete
{
    internal class MailService : IMailService
    {
        private const string Resource = @"/mail/send";

        private SendGridHttpClient _client;
        private Mail _mailRequestBody;

        public MailService(SendGridHttpClient client)
        {
            _client = client;
        }

        public Mail MailRequest
        {
            get
            {
                return _mailRequestBody;
            }

            set
            {
                _mailRequestBody = value;
            }
        }

        public async Task<MailServiceResponse> SendAsync()
        {
            var result = new MailServiceResponse();

            try
            {
                var response = await _client.PostAsync<Mail>(Resource, _mailRequestBody);
                result.Success = response.StatusCode == System.Net.HttpStatusCode.OK;

            } catch (Exception e)
            {
                result.Success = false;
                result.Message = e.Message;
            }
            
            return result;
        }
    }
}
