using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var options = new SendGridClientOptions();


////////////////////////////////////////////////////////
// Available Sendgrid Client Options to set region as "eu"

var client_option_region = "eu";
options.SetDataResidency(client_option_region);
var client = new SendGridClient(options);
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "clients/stats");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Available Sendgrid Client Options to set region as "global"

var client_option_region = "global";
options.SetDataResidency(client_option_region);
var client = new SendGridClient(options);
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "clients/stats");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

