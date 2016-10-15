

using SendGrid.Helpers.Mail;
using SendGrid.Models;
using System.Threading.Tasks;

namespace SendGrid.Interfaces
{
    public interface IMailService
    {
        Mail MailRequest { get; set; }
        Task<MailServiceResponse> SendAsync();
    }
}
