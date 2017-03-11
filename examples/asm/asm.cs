using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Create a new suppression group
// POST /asm/groups

string data = @"{
  'description': 'Suggestions for products our users might like.', 
  'is_default': true, 
  'name': 'Product Suggestions'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve information about multiple suppression groups
// GET /asm/groups

string queryParams = @"{
  'id': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a suppression group.
// PATCH /asm/groups/{group_id}

string data = @"{
  'description': 'Suggestions for items our users might like.', 
  'id': 103, 
  'name': 'Item Suggestions'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "asm/groups/" + group_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get information on a single suppression group.
// GET /asm/groups/{group_id}

var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a suppression group.
// DELETE /asm/groups/{group_id}

var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add suppressions to a suppression group
// POST /asm/groups/{group_id}/suppressions

string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups/" + group_id + "/suppressions", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppressions for a suppression group
// GET /asm/groups/{group_id}/suppressions

var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id + "/suppressions");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Search for suppressions within a group
// POST /asm/groups/{group_id}/suppressions/search

string data = @"{
  'recipient_emails': [
    'exists1@example.com', 
    'exists2@example.com', 
    'doesnotexists@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var group_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups/" + group_id + "/suppressions/search", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a suppression from a suppression group
// DELETE /asm/groups/{group_id}/suppressions/{email}

var group_id = "test_url_param";
var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id + "/suppressions/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppressions
// GET /asm/suppressions

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add recipient addresses to the global suppression group.
// POST /asm/suppressions/global

string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/suppressions/global", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Global Suppression
// GET /asm/suppressions/global/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/global/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Global Suppression
// DELETE /asm/suppressions/global/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/suppressions/global/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppression groups for an email address
// GET /asm/suppressions/{email}

var email = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/" + email);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

