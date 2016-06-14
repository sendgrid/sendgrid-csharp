using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Returns a list of all partner settings.
// GET /partner_settings

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
dynamic response = sg.client.partner_settings.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Updates New Relic partner settings.
// PATCH /partner_settings/new_relic

string data = @"{
  'enable_subuser_statistics': true, 
  'enabled': true, 
  'license_key': ''
}";
dynamic response = sg.client.partner_settings.new_relic.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Returns all New Relic partner settings.
// GET /partner_settings/new_relic

dynamic response = sg.client.partner_settings.new_relic.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

