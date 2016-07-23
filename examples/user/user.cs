using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Get a user's account information.
// GET /user/account

dynamic response = await sg.client.user.account.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your credit balance
// GET /user/credits

dynamic response = await sg.client.user.credits.get();
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
dynamic response = await sg.client.user.email.put(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your account email address
// GET /user/email

dynamic response = await sg.client.user.email.get();
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
dynamic response = await sg.client.user.password.put(requestBody: data);
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
dynamic response = await sg.client.user.profile.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get a user's profile
// GET /user/profile

dynamic response = await sg.client.user.profile.get();
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
dynamic response = await sg.client.user.scheduled_sends.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all scheduled sends
// GET /user/scheduled_sends

dynamic response = await sg.client.user.scheduled_sends.get();
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
dynamic response = await sg.client.user.scheduled_sends._(batch_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve scheduled send
// GET /user/scheduled_sends/{batch_id}

var batch_id = "test_url_param";
dynamic response = await sg.client.user.scheduled_sends._(batch_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a cancellation or pause of a scheduled send
// DELETE /user/scheduled_sends/{batch_id}

var batch_id = "test_url_param";
dynamic response = await sg.client.user.scheduled_sends._(batch_id).delete();
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
dynamic response = await sg.client.user.settings.enforced_tls.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve current Enforced TLS settings.
// GET /user/settings/enforced_tls

dynamic response = await sg.client.user.settings.enforced_tls.get();
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
dynamic response = await sg.client.user.username.put(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve your username
// GET /user/username

dynamic response = await sg.client.user.username.get();
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
dynamic response = await sg.client.user.webhooks._("_("event")").settings.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Event Webhook settings
// GET /user/webhooks/event/settings

dynamic response = await sg.client.user.webhooks._("_("event")").settings.get();
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
dynamic response = await sg.client.user.webhooks._("_("event")").test.post(requestBody: data);
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
dynamic response = await sg.client.user.webhooks.parse.settings.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all parse settings
// GET /user/webhooks/parse/settings

dynamic response = await sg.client.user.webhooks.parse.settings.get();
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
dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific parse setting
// GET /user/webhooks/parse/settings/{hostname}

var hostname = "test_url_param";
dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a parse setting
// DELETE /user/webhooks/parse/settings/{hostname}

var hostname = "test_url_param";
dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).delete();
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
dynamic response = await sg.client.user.webhooks.parse.stats.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

