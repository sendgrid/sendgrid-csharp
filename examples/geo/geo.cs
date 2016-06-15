using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Retrieve email statistics by country and state/province.
// GET /geo/stats

string queryParams = @"{
  'aggregated_by': 'day', 
  'country': 'US', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
dynamic response = sg.client.geo.stats.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

