using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

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
dynamic response = await sg.client.subusers.post(requestBody: data);
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
dynamic response = await sg.client.subusers.get(queryParams: queryParams);
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
dynamic response = await sg.client.subusers.reputations.get(queryParams: queryParams);
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
dynamic response = await sg.client.subusers.stats.get(queryParams: queryParams);
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
dynamic response = await sg.client.subusers.stats.monthly.get(queryParams: queryParams);
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
dynamic response = await sg.client.subusers.stats.sums.get(queryParams: queryParams);
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
dynamic response = await sg.client.subusers._(subuser_name).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a subuser
// DELETE /subusers/{subuser_name}

var subuser_name = "test_url_param";
dynamic response = await sg.client.subusers._(subuser_name).delete();
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
dynamic response = await sg.client.subusers._(subuser_name).ips.put(requestBody: data);
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
dynamic response = await sg.client.subusers._(subuser_name).monitor.put(requestBody: data);
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
dynamic response = await sg.client.subusers._(subuser_name).monitor.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve monitor settings for a subuser
// GET /subusers/{subuser_name}/monitor

var subuser_name = "test_url_param";
dynamic response = await sg.client.subusers._(subuser_name).monitor.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete monitor settings
// DELETE /subusers/{subuser_name}/monitor

var subuser_name = "test_url_param";
dynamic response = await sg.client.subusers._(subuser_name).monitor.delete();
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
dynamic response = await sg.client.subusers._(subuser_name).stats.monthly.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

