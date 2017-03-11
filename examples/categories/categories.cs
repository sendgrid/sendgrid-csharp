using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Retrieve all categories
// GET /categories

string queryParams = @"{
  'category': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Email Statistics for Categories
// GET /categories/stats

string queryParams = @"{
  'aggregated_by': 'day', 
  'categories': 'test_string', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories/stats", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve sums of email stats for each category [Needs: Stats object defined, has category ID?]
// GET /categories/stats/sums

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'start_date': '2016-01-01'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories/stats/sums", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

