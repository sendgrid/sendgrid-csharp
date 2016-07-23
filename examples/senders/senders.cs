using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create a Sender Identity
// POST /senders

string data = @"{
  'address': '123 Elm St.', 
  'address_2': 'Apt. 456', 
  'city': 'Denver', 
  'country': 'United States', 
  'from': {
    'email': 'from@example.com', 
    'name': 'Example INC'
  }, 
  'nickname': 'My Sender ID', 
  'reply_to': {
    'email': 'replyto@example.com', 
    'name': 'Example INC'
  }, 
  'state': 'Colorado', 
  'zip': '80202'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.senders.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get all Sender Identities
// GET /senders

dynamic response = await sg.client.senders.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a Sender Identity
// PATCH /senders/{sender_id}

string data = @"{
  'address': '123 Elm St.', 
  'address_2': 'Apt. 456', 
  'city': 'Denver', 
  'country': 'United States', 
  'from': {
    'email': 'from@example.com', 
    'name': 'Example INC'
  }, 
  'nickname': 'My Sender ID', 
  'reply_to': {
    'email': 'replyto@example.com', 
    'name': 'Example INC'
  }, 
  'state': 'Colorado', 
  'zip': '80202'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var sender_id = "test_url_param";
dynamic response = await sg.client.senders._(sender_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// View a Sender Identity
// GET /senders/{sender_id}

var sender_id = "test_url_param";
dynamic response = await sg.client.senders._(sender_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Sender Identity
// DELETE /senders/{sender_id}

var sender_id = "test_url_param";
dynamic response = await sg.client.senders._(sender_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Resend Sender Identity Verification
// POST /senders/{sender_id}/resend_verification

var sender_id = "test_url_param";
dynamic response = await sg.client.senders._(sender_id).resend_verification.post();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

