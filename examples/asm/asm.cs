using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

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
dynamic response = await sg.client.asm.groups.post(requestBody: data);
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
dynamic response = await sg.client.asm.groups.get(queryParams: queryParams);
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
dynamic response = await sg.client.asm.groups._(group_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get information on a single suppression group.
// GET /asm/groups/{group_id}

var group_id = "test_url_param";
dynamic response = await sg.client.asm.groups._(group_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a suppression group.
// DELETE /asm/groups/{group_id}

var group_id = "test_url_param";
dynamic response = await sg.client.asm.groups._(group_id).delete();
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
dynamic response = await sg.client.asm.groups._(group_id).suppressions.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppressions for a suppression group
// GET /asm/groups/{group_id}/suppressions

var group_id = "test_url_param";
dynamic response = await sg.client.asm.groups._(group_id).suppressions.get();
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
dynamic response = await sg.client.asm.groups._(group_id).suppressions.search.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a suppression from a suppression group
// DELETE /asm/groups/{group_id}/suppressions/{email}

var group_id = "test_url_param";
var email = "test_url_param";
dynamic response = await sg.client.asm.groups._(group_id).suppressions._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppressions
// GET /asm/suppressions

dynamic response = await sg.client.asm.suppressions.get();
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
dynamic response = await sg.client.asm.suppressions.global.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Global Suppression
// GET /asm/suppressions/global/{email}

var email = "test_url_param";
dynamic response = await sg.client.asm.suppressions.global._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Global Suppression
// DELETE /asm/suppressions/global/{email}

var email = "test_url_param";
dynamic response = await sg.client.asm.suppressions.global._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all suppression groups for an email address
// GET /asm/suppressions/{email}

var email = "test_url_param";
dynamic response = await sg.client.asm.suppressions._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

