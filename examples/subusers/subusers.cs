using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Create Subuser
// POST /subusers

string data = @"{
  'email': 'John@example.com', 
  'ips': [
    '1.1.1.1', 
    '2.2.2.2'
  ], 
  'password': 'johns_password', 
  'username': 'John@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "subusers", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// List all Subusers
// GET /subusers

string queryParams = @"{
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Subuser Reputations
// GET /subusers/reputations

string queryParams = @"{
  'usernames': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/reputations", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve email statistics for your subusers.
// GET /subusers/stats

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01', 
  'subusers': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve monthly stats for all subusers
// GET /subusers/stats/monthly

string queryParams = @"{
  'date': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'subuser': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats/monthly", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
//  Retrieve the totals for each email statistic metric for all subusers.
// GET /subusers/stats/sums

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'start_date': '2016-01-01'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats/sums", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Enable/disable a subuser
// PATCH /subusers/{subuser_name}

string data = @"{
  'disabled': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "subusers/" + subuser_name, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a subuser
// DELETE /subusers/{subuser_name}

var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update IPs assigned to a subuser
// PUT /subusers/{subuser_name}/ips

string data = @"[
  '127.0.0.1'
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "subusers/" + subuser_name + "/ips", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Monitor Settings for a subuser
// PUT /subusers/{subuser_name}/monitor

string data = @"{
  'email': 'example@example.com', 
  'frequency': 500
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "subusers/" + subuser_name + "/monitor", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create monitor settings
// POST /subusers/{subuser_name}/monitor

string data = @"{
  'email': 'example@example.com', 
  'frequency': 50000
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "subusers/" + subuser_name + "/monitor", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve monitor settings for a subuser
// GET /subusers/{subuser_name}/monitor

var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/" + subuser_name + "/monitor");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete monitor settings
// DELETE /subusers/{subuser_name}/monitor

var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name + "/monitor");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve the monthly email statistics for a single subuser
// GET /subusers/{subuser_name}/stats/monthly

string queryParams = @"{
  'date': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string'
}";
var subuser_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/" + subuser_name + "/stats/monthly", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

