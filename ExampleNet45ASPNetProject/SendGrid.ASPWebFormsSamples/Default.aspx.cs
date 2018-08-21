using System;
using System.Threading.Tasks;
using System.Web.UI;
using SendGrid.ASPWebFormsSamples.Models;
using SendGrid.ASPWebFormsSamples.Services;

namespace SendGrid.ASPWebFormsSamples
{
    public partial class Default : Page
    {
        private readonly SendGridService _sendGridService;

        protected Default()
        {
            _sendGridService = new SendGridService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            responseInfo.Visible = false;
        }

        protected async void sendButton_Click(object sender, EventArgs e)
        {
            // Prepare the email message info model
            var messageInfo = new EmailMessageInfo
            {
                FromEmailAddress = fromInput.Value,
                ToEmailAddress = toInput.Value,
                CcEmailAddress = ccInput.Value,
                BccEmailAddress = bccInput.Value,
                EmailSubject = subjectInput.Value,
                EmailBody = bodyInput.Value
            };

            // Make an API call, and save the response
            var apiResponse = await _sendGridService.Send(messageInfo);

            await SetResponseInfoContainers(apiResponse);
        }

        private async Task SetResponseInfoContainers(Response apiResponse)
        {
            responseInfo.Visible = true;
            responseStatus.InnerText = $"Statuscode {(int)apiResponse.StatusCode}: {apiResponse.StatusCode}";
            responseBody.InnerText = await apiResponse.Body.ReadAsStringAsync();
        }
    }
}