using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create API keys
// POST /api_keys

string data = @"{
  'name': 'My API Key', 
  'sample': 'data', 
  'scopes': [
    'mail.send', 
    'alerts.create', 
    'alerts.read'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.api_keys.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all API Keys belonging to the authenticated user
// GET /api_keys

string queryParams = @"{
  'limit': 1
}";
dynamic response = await sg.client.api_keys.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update the name & scopes of an API Key
// PUT /api_keys/{api_key_id}

string data = @"{
  'name': 'A New Hope', 
  'scopes': [
    'user.profile.read', 
    'user.profile.update'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var api_key_id = "test_url_param";
dynamic response = await sg.client.api_keys._(api_key_id).put(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update API keys
// PATCH /api_keys/{api_key_id}

string data = @"{
  'name': 'A New Hope'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var api_key_id = "test_url_param";
dynamic response = await sg.client.api_keys._(api_key_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve an existing API Key
// GET /api_keys/{api_key_id}

var api_key_id = "test_url_param";
dynamic response = await sg.client.api_keys._(api_key_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete API keys
// DELETE /api_keys/{api_key_id}

var api_key_id = "test_url_param";
dynamic response = await sg.client.api_keys._(api_key_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

