using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Create a domain authentication.
// POST /whitelabel/domains

string data = @"{
  'automatic_security': false,
  'custom_spf': true,
  'default': true,
  'domain': 'example.com',
  'ips': [
    '192.168.1.1',
    '192.168.1.2'
  ],
  'subdomain': 'news',
  'username': 'john@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// List all domain authentication.
// GET /whitelabel/domains

string queryParams = @"{
  'domain': 'test_string',
  'exclude_subusers': 'true',
  'limit': 1,
  'offset': 1,
  'username': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get the default domain authentication.
// GET /whitelabel/domains/default

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/_("default")");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// List the domain authentication associated with the given user.
// GET /whitelabel/domains/subuser

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/subuser");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Disassociate a domain authentication from a given user.
// DELETE /whitelabel/domains/subuser

var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/subuser");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a domain authentication.
// PATCH /whitelabel/domains/{domain_id}

string data = @"{
  'custom_spf': true,
  'default': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var domain_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "whitelabel/domains/" + domain_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a domain authentication.
// GET /whitelabel/domains/{domain_id}

var domain_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/" + domain_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a domain authentication.
// DELETE /whitelabel/domains/{domain_id}

var domain_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + domain_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Associate a domain authentication with a given user.
// POST /whitelabel/domains/{domain_id}/subuser

string data = @"{
  'username': 'jane@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var domain_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + domain_id + "/subuser", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add an IP to a domain authentication.
// POST /whitelabel/domains/{id}/ips

string data = @"{
  'ip': '192.168.0.1'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + id + "/ips", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP from a domain authentication.
// DELETE /whitelabel/domains/{id}/ips/{ip}

var id = "test_url_param";
var ip = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + id + "/ips/" + ip);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate a domain authentication.
// POST /whitelabel/domains/{id}/validate

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + id + "/validate");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a reverse DNS record
// POST /whitelabel/ips

string data = @"{
  'domain': 'example.com',
  'ip': '192.168.1.1',
  'subdomain': 'email'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/ips", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a reverse DNS record
// GET /whitelabel/ips

string queryParams = @"{
  'ip': 'test_string',
  'limit': 1,
  'offset': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a reverse DNS record
// GET /whitelabel/ips/{id}

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips/" + id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a reverse DNS record
// DELETE /whitelabel/ips/{id}

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/ips/" + id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate a reverse DNS record
// POST /whitelabel/ips/{id}/validate

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/ips/" + id + "/validate");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a Link Branding
// POST /whitelabel/links

string data = @"{
  'default': true,
  'domain': 'example.com',
  'subdomain': 'mail'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
string queryParams = @"{
  'limit': 1,
  'offset': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links", requestBody: data, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all link brandings
// GET /whitelabel/links

string queryParams = @"{
  'limit': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Default Link Branding
// GET /whitelabel/links/default

string queryParams = @"{
  'domain': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/_("default")", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Associated Link Branding
// GET /whitelabel/links/subuser

string queryParams = @"{
  'username': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/subuser", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Disassociate a Link Branding
// DELETE /whitelabel/links/subuser

string queryParams = @"{
  'username': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/subuser", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a Link Branding
// PATCH /whitelabel/links/{id}

string data = @"{
  'default': true
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "whitelabel/links/" + id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Link Branding
// GET /whitelabel/links/{id}

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/" + id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Link Branding
// DELETE /whitelabel/links/{id}

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/" + id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate a Link Branding
// POST /whitelabel/links/{id}/validate

var id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links/" + id + "/validate");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Associate a Link Branding
// POST /whitelabel/links/{link_id}/subuser

string data = @"{
  'username': 'jane@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var link_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links/" + link_id + "/subuser", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

