using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Retrieve a list of scopes for which this user has access.
// GET /scopes

dynamic response = sg.client.scopes.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

