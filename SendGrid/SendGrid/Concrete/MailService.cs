using System;
using SendGrid.Helpers.Mail;
using SendGrid.Interfaces;
using SendGrid.Models;
using System.Threading.Tasks;

namespace SendGrid.Concrete
{
    internal class MailService : IMailService
    {
        private Client _client;
        private Mail _mailRequest;

        public MailService(Client client)
        {
            _client = client;
        }

        public Mail MailRequest
        {
            get
            {
                return _mailRequest;
            }

            set
            {
                _mailRequest = value;
            }
        }

        public async Task<MailServiceResponse> SendAsync()
        {
            var result = new MailServiceResponse();

            var response = await _client.MakeRequest(null, null);

            //TODO: Handle the response and return right data

            return result;
        }
    }
}
