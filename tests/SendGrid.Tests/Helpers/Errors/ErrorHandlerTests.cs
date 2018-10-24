using SendGrid.Helpers.Errors.Model;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SendGrid.Tests.Helpers.Errors
{
    public class ErrorHandlerTests
    {
        private string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");

        [Fact]
        public async void TestUnauthorizedRequestException()
        {
            var client = new SendGridClient("", httpErrorAsException: true);

            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<UnauthorizedException>(ex);

                var jsonErrorReponse = ex.Message;
                
                ErrorResponse errorResponseExpected = new ErrorResponse
                {
                    DefaultErrorData = "401 - Unauthorized",
                    SendGriErrorMessage = "Permission denied, wrong credentials",
                    FieldWithError = null,
                    HelpLink = null
                };

                var jsonErrorReponseExpected = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponseExpected);

                Assert.Equal(jsonErrorReponse, jsonErrorReponseExpected);
            }
        }

        [Fact]
        public async void TestBadRequestExceptionFromEmailNull()
        {            
            var client = new SendGridClient(_apiKey, httpErrorAsException: true);

            var from = new EmailAddress("", "Example User");
            var subject = "Hello World from the SendGrid CSharp Library Helper!";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            
            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch(Exception ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<BadRequestException>(ex);

                var jsonErrorReponse = ex.Message;

                ErrorResponse errorResponseExpected = new ErrorResponse
                {
                    DefaultErrorData = "400 - Bad Request",
                    SendGriErrorMessage = "The from email does not contain a valid address.",
                    FieldWithError = "from.email",
                    HelpLink = "http://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html#message.from"
                };

                var jsonErrorReponseExpected = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponseExpected);
                
                Assert.Equal(jsonErrorReponse, jsonErrorReponseExpected);
            }
        }

        [Fact]
        public async void TestBadRequestExceptionSubjectError()
        {
            var client = new SendGridClient(_apiKey, httpErrorAsException: true);

            var from = new EmailAddress("test@example.com", "Example User");
            var subject = "";
            var to = new EmailAddress("test@example.com", "Example User");
            var plainTextContent = "Hello, Email from the helper [SendSingleEmailAsync]!";
            var htmlContent = "<strong>Hello, Email from the helper! [SendSingleEmailAsync]</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);

            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<BadRequestException>(ex);

                var jsonErrorReponse = ex.Message;

                ErrorResponse errorResponseExpected = new ErrorResponse
                {
                    DefaultErrorData = "400 - Bad Request",
                    SendGriErrorMessage = "The subject is required. You can get around this requirement if you use a template with a subject defined or if every personalization has a subject defined.",
                    FieldWithError = "subject",
                    HelpLink = "http://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html#message.subject"
                };

                var jsonErrorReponseExpected = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponseExpected);

                Assert.Equal(jsonErrorReponse, jsonErrorReponseExpected);
            }
        }

        [Fact]
        public async void TestBadRequestAttachmentError()
        {
            var client = new SendGridClient(_apiKey, httpErrorAsException: true);

            var from = new EmailAddress("test@example.com");
            var subject = "Subject";
            var to = new EmailAddress("test@example.com");
            var body = "Email Body";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, body, "");

            var attachment = new Attachment
            {
                Filename = "file.txt",
                Content = null,
                Type = null,
                Disposition = null,
                ContentId = null
            };

            msg.AddAttachment(attachment);

            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<BadRequestException>(ex);

                var jsonErrorReponse = ex.Message;

                ErrorResponse errorResponseExpected = new ErrorResponse
                {
                    DefaultErrorData = "400 - Bad Request",
                    SendGriErrorMessage = "The attachment content is required.",
                    FieldWithError = "attachments.0.content",
                    HelpLink = "http://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html#message.attachments.content"
                };

                var jsonErrorReponseExpected = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponseExpected);

                Assert.Equal(jsonErrorReponse, jsonErrorReponseExpected);
            }
        }

        [Fact]
        public async void TestBadRequestTemplateError()
        {
            var client = new SendGridClient(_apiKey, httpErrorAsException: true);

            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com", "Example User"));
            msg.AddTo(new EmailAddress("test@example.com", "Example User"));
            msg.SetTemplateId("");

            try
            {
                var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                Assert.NotNull(ex);
                Assert.IsType<BadRequestException>(ex);

                var jsonErrorReponse = ex.Message;
                
                ErrorResponse errorResponseExpected = new ErrorResponse
                {
                    DefaultErrorData = "400 - Bad Request",
                    SendGriErrorMessage = "The template_id must be a valid GUID, you provided ''.",
                    FieldWithError = "template_id",
                    HelpLink = "http://sendgrid.com/docs/API_Reference/Web_API_v3/Mail/errors.html#message.template_id"
                };

                var jsonErrorReponseExpected = Newtonsoft.Json.JsonConvert.SerializeObject(errorResponseExpected);

                Assert.Equal(jsonErrorReponse, jsonErrorReponseExpected);
            }
        }
    }
}
