using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Retrieve all blocks
// GET /suppression/blocks

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/blocks", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete blocks
// DELETE /suppression/blocks

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/blocks", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific block
// GET /suppression/blocks/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/blocks/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific block
// DELETE /suppression/blocks/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/blocks/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all bounces
// GET /suppression/bounces

string queryParams = @"{
  'end_time': 1, 
  'start_time': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete bounces
// DELETE /suppression/bounces

string data = @"{
  'delete_all': true, 
  'emails': [
    'example@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/bounces", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Bounce
// GET /suppression/bounces/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a bounce
// DELETE /suppression/bounces/{email}

string queryParams = @"{
  'email_address': 'example@example.com'
}";
var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/bounces/" + email, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all invalid emails
// GET /suppression/invalid_emails

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/invalid_emails", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete invalid emails
// DELETE /suppression/invalid_emails

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/invalid_emails", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific invalid email
// GET /suppression/invalid_emails/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/invalid_emails/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific invalid email
// DELETE /suppression/invalid_emails/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/invalid_emails/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific spam report
// GET /suppression/spam_report/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/spam_report/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific spam report
// DELETE /suppression/spam_report/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/spam_reports/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all spam reports
// GET /suppression/spam_reports

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/spam_reports", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete spam reports
// DELETE /suppression/spam_reports

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/spam_reports", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all global suppressions
// GET /suppression/unsubscribes

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/unsubscribes", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

