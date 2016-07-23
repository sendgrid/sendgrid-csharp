using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create a new Alert
// POST /alerts

string data = @"{
  'email_to': 'example@example.com', 
  'frequency': 'daily', 
  'type': 'stats_notification'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.alerts.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all alerts
// GET /alerts

dynamic response = await sg.client.alerts.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update an alert
// PATCH /alerts/{alert_id}

string data = @"{
  'email_to': 'example@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var alert_id = "test_url_param";
dynamic response = await sg.client.alerts._(alert_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific alert
// GET /alerts/{alert_id}

var alert_id = "test_url_param";
dynamic response = await sg.client.alerts._(alert_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete an alert
// DELETE /alerts/{alert_id}

var alert_id = "test_url_param";
dynamic response = await sg.client.alerts._(alert_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

