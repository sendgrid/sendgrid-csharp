using System;
using System.Threading.Tasks;
using ExampleAspNetCoreProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ExampleAspNetCoreProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> logger;
        private readonly ISendGridClient client;

        public EmailController(ILogger<EmailController> logger, ISendGridClient client)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var message = new SendGridMessage
            {
                HtmlContent = model.Content,
                Subject = model.Subject,
                From = new EmailAddress(model.From)
            };

            message.AddTo(model.Recipient);

            logger.LogInformation($"Sending email to {model.Recipient} with subject {model.Subject}.");

            var response = await client.SendEmailAsync(message);

            logger.LogInformation($"SendGrid responded with status code: {response.StatusCode}");

            return StatusCode((int) response.StatusCode);
        }
    }
}
