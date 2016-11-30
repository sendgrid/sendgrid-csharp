using System;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
Client client = new Client(apiKey);

////////////////////////////////////////////////////////
// Retrieve all mail settings
// GET /mail_settings

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update address whitelist mail settings
// PATCH /mail_settings/address_whitelist

string data = @"{
  'enabled': true, 
  'list': [
    'email1@example.com', 
    'example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/address_whitelist", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve address whitelist mail settings
// GET /mail_settings/address_whitelist

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/address_whitelist");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update BCC mail settings
// PATCH /mail_settings/bcc

string data = @"{
  'email': 'email@example.com', 
  'enabled': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/bcc", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all BCC mail settings
// GET /mail_settings/bcc

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/bcc");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update bounce purge mail settings
// PATCH /mail_settings/bounce_purge

string data = @"{
  'enabled': true, 
  'hard_bounces': 5, 
  'soft_bounces': 5
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/bounce_purge", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve bounce purge mail settings
// GET /mail_settings/bounce_purge

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/bounce_purge");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update footer mail settings
// PATCH /mail_settings/footer

string data = @"{
  'enabled': true, 
  'html_content': '...', 
  'plain_content': '...'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/footer", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve footer mail settings
// GET /mail_settings/footer

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/footer");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update forward bounce mail settings
// PATCH /mail_settings/forward_bounce

string data = @"{
  'email': 'example@example.com', 
  'enabled': true
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/forward_bounce", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve forward bounce mail settings
// GET /mail_settings/forward_bounce

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/forward_bounce");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update forward spam mail settings
// PATCH /mail_settings/forward_spam

string data = @"{
  'email': '', 
  'enabled': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/forward_spam", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve forward spam mail settings
// GET /mail_settings/forward_spam

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/forward_spam");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update plain content mail settings
// PATCH /mail_settings/plain_content

string data = @"{
  'enabled': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/plain_content", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve plain content mail settings
// GET /mail_settings/plain_content

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/plain_content");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update spam check mail settings
// PATCH /mail_settings/spam_check

string data = @"{
  'enabled': true, 
  'max_score': 5, 
  'url': 'url'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/spam_check", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve spam check mail settings
// GET /mail_settings/spam_check

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/spam_check");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update template mail settings
// PATCH /mail_settings/template

string data = @"{
  'enabled': true, 
  'html_content': '<% body %>'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "mail_settings/template", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve legacy template mail settings
// GET /mail_settings/template

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "mail_settings/template");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

