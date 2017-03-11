using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Retrieve global email statistics
// GET /stats

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "stats", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

