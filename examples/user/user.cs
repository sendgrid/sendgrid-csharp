using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Get a user's account information.
// GET /user/account

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/account");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your credit balance
// GET /user/credits

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/credits");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update your account email address
// PUT /user/email

string data = @"{
  'email': 'example@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/email", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your account email address
// GET /user/email

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/email");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update your password
// PUT /user/password

string data = @"{
  'new_password': 'new_password', 
  'old_password': 'old_password'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/password", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a user's profile
// PATCH /user/profile

string data = @"{
  'city': 'Orange', 
  'first_name': 'Example', 
  'last_name': 'User'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/profile", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get a user's profile
// GET /user/profile

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/profile");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Cancel or pause a scheduled send
// POST /user/scheduled_sends

string data = @"{
  'batch_id': 'YOUR_BATCH_ID', 
  'status': 'pause'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/scheduled_sends", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all scheduled sends
// GET /user/scheduled_sends

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update user scheduled send information
// PATCH /user/scheduled_sends/{batch_id}

string data = @"{
  'status': 'pause'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var batch_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/scheduled_sends/" + batch_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve scheduled send
// GET /user/scheduled_sends/{batch_id}

var batch_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends/" + batch_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a cancellation or pause of a scheduled send
// DELETE /user/scheduled_sends/{batch_id}

var batch_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/scheduled_sends/" + batch_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Enforced TLS settings
// PATCH /user/settings/enforced_tls

string data = @"{
  'require_tls': true, 
  'require_valid_cert': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/settings/enforced_tls", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve current Enforced TLS settings.
// GET /user/settings/enforced_tls

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/settings/enforced_tls");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update your username
// PUT /user/username

string data = @"{
  'username': 'test_username'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/username", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your username
// GET /user/username

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/username");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Event Notification Settings
// PATCH /user/webhooks/event/settings

string data = @"{
  'bounce': true, 
  'click': true, 
  'deferred': true, 
  'delivered': true, 
  'dropped': true, 
  'enabled': true, 
  'group_resubscribe': true, 
  'group_unsubscribe': true, 
  'open': true, 
  'processed': true, 
  'spam_report': true, 
  'unsubscribe': true, 
  'url': 'url'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/webhooks/_("event")/settings", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Event Webhook settings
// GET /user/webhooks/event/settings

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/_("event")/settings");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Test Event Notification Settings 
// POST /user/webhooks/event/test

string data = @"{
  'url': 'url'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/webhooks/_("event")/test", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a parse setting
// POST /user/webhooks/parse/settings

string data = @"{
  'hostname': 'myhostname.com', 
  'send_raw': false, 
  'spam_check': true, 
  'url': 'http://email.myhosthame.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/webhooks/parse/settings", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all parse settings
// GET /user/webhooks/parse/settings

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a parse setting
// PATCH /user/webhooks/parse/settings/{hostname}

string data = @"{
  'send_raw': true, 
  'spam_check': false, 
  'url': 'http://newdomain.com/parse'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var hostname = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/webhooks/parse/settings/" + hostname, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific parse setting
// GET /user/webhooks/parse/settings/{hostname}

var hostname = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings/" + hostname);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a parse setting
// DELETE /user/webhooks/parse/settings/{hostname}

var hostname = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/webhooks/parse/settings/" + hostname);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieves Inbound Parse Webhook statistics.
// GET /user/webhooks/parse/stats

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 'test_string', 
  'offset': 'test_string', 
  'start_date': '2016-01-01'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/stats", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

