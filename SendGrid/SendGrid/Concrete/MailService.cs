using SendGrid.Interfaces;


namespace SendGrid.Concrete
{
    internal class MailService : IMailService
    {
        private Client _client;

        public MailService(Client client)
        {
            _client = client;
        }
    }
}
