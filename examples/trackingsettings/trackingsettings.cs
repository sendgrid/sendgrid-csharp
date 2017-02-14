using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Retrieve Tracking Settings
// GET /tracking_settings

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Click Tracking Settings
// PATCH /tracking_settings/click

string data = @"{
  'enabled': true
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/click", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Click Track Settings
// GET /tracking_settings/click

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/click");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Google Analytics Settings
// PATCH /tracking_settings/google_analytics

string data = @"{
  'enabled': true, 
  'utm_campaign': 'website', 
  'utm_content': '', 
  'utm_medium': 'email', 
  'utm_source': 'sendgrid.com', 
  'utm_term': ''
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/google_analytics", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Google Analytics Settings
// GET /tracking_settings/google_analytics

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/google_analytics");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Open Tracking Settings
// PATCH /tracking_settings/open

string data = @"{
  'enabled': true
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/open", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get Open Tracking Settings
// GET /tracking_settings/open

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/open");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Subscription Tracking Settings
// PATCH /tracking_settings/subscription

string data = @"{
  'enabled': true, 
  'html_content': 'html content', 
  'landing': 'landing page html', 
  'plain_content': 'text content', 
  'replace': 'replacement tag', 
  'url': 'url'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/subscription", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Subscription Tracking Settings
// GET /tracking_settings/subscription

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/subscription");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

