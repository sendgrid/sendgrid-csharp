using System;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
Client client = new Client(apiKey);

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
Response response = await client.RequestAsync(method: Client.Methods.POST, urlPath: "alerts", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all alerts
// GET /alerts

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "alerts");
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
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "alerts/" + alert_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific alert
// GET /alerts/{alert_id}

var alert_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "alerts/" + alert_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete an alert
// DELETE /alerts/{alert_id}

var alert_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.DELETE, urlPath: "alerts/" + alert_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

