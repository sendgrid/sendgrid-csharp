using System.Web;
using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Threading.Tasks;
using Exceptions;
using System;

/// <summary>
///
/// Summary description for SendGridSendAsync
///
/// This will send the message Async, and will return a string after is completed.
///
/// If the delivery is ok, the string will be "OK" (from the HttpStatusCode)
///
/// If there is an error, the error will be catch and the string will show the error in this format:
///
/// Status Code: [HttpStatusCode]. Errors returned by the API: [error1; error2; etc;]
///
/// Example of error:
/// 
/// Status Code: BadRequest. Errors returned by the API: Missing subject;
/// Status Code: BadRequest. Errors returned by the API: invalid API Key;
///
/// </summary>
public class SendGridSendAsync
{
    string SendGridApiKey = "Your API Key";

    public async Task<string> sendMessageAsync(SendGridMessage sendGridMessage)
    {
        // Create a Web transport for sending email.
        var transportWeb = new Web(SendGridApiKey);

        try
        {
            string response = await transportWeb.DeliverAsyncReturn(sendGridMessage).ConfigureAwait(continueOnCapturedContext: false);
            return response;
        }
        catch (InvalidApiRequestException ex)
        {
            string error;
            string statusCode = "Status Code: " + ex.ResponseStatusCode;
            string label = ". Errors returned by the API: ";
            string errors = "";

            for (int i = 0; i <= ex.Errors.Length - 1; i++)
            {
                errors = errors + " " + ex.Errors[i] + ";";
            }

            error = statusCode + label + errors;
            return error;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}
