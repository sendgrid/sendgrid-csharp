using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create a domain whitelabel.
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
dynamic response = await sg.client.whitelabel.domains.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// List all domain whitelabels.
// GET /whitelabel/domains

string queryParams = @"{
  'domain': 'test_string', 
  'exclude_subusers': 'true', 
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
dynamic response = await sg.client.whitelabel.domains.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Get the default domain whitelabel.
// GET /whitelabel/domains/default

dynamic response = await sg.client.whitelabel.domains._("_("default")").get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// List the domain whitelabel associated with the given user.
// GET /whitelabel/domains/subuser

dynamic response = await sg.client.whitelabel.domains.subuser.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Disassociate a domain whitelabel from a given user.
// DELETE /whitelabel/domains/subuser

dynamic response = await sg.client.whitelabel.domains.subuser.delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a domain whitelabel.
// PATCH /whitelabel/domains/{domain_id}

string data = @"{
  'custom_spf': true, 
  'default': false
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var domain_id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(domain_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a domain whitelabel.
// GET /whitelabel/domains/{domain_id}

var domain_id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(domain_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a domain whitelabel.
// DELETE /whitelabel/domains/{domain_id}

var domain_id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(domain_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Associate a domain whitelabel with a given user.
// POST /whitelabel/domains/{domain_id}/subuser

string data = @"{
  'username': 'jane@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var domain_id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(domain_id).subuser.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add an IP to a domain whitelabel.
// POST /whitelabel/domains/{id}/ips

string data = @"{
  'ip': '192.168.0.1'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(id).ips.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP from a domain whitelabel.
// DELETE /whitelabel/domains/{id}/ips/{ip}

var id = "test_url_param";
var ip = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(id).ips._(ip).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate a domain whitelabel.
// POST /whitelabel/domains/{id}/validate

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.domains._(id).validate.post();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create an IP whitelabel
// POST /whitelabel/ips

string data = @"{
  'domain': 'example.com', 
  'ip': '192.168.1.1', 
  'subdomain': 'email'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.whitelabel.ips.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IP whitelabels
// GET /whitelabel/ips

string queryParams = @"{
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
dynamic response = await sg.client.whitelabel.ips.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve an IP whitelabel
// GET /whitelabel/ips/{id}

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.ips._(id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete an IP whitelabel
// DELETE /whitelabel/ips/{id}

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.ips._(id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate an IP whitelabel
// POST /whitelabel/ips/{id}/validate

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.ips._(id).validate.post();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a Link Whitelabel
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
dynamic response = await sg.client.whitelabel.links.post(requestBody: data, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all link whitelabels
// GET /whitelabel/links

string queryParams = @"{
  'limit': 1
}";
dynamic response = await sg.client.whitelabel.links.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Default Link Whitelabel
// GET /whitelabel/links/default

string queryParams = @"{
  'domain': 'test_string'
}";
dynamic response = await sg.client.whitelabel.links._("_("default")").get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve Associated Link Whitelabel
// GET /whitelabel/links/subuser

string queryParams = @"{
  'username': 'test_string'
}";
dynamic response = await sg.client.whitelabel.links.subuser.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Disassociate a Link Whitelabel
// DELETE /whitelabel/links/subuser

string queryParams = @"{
  'username': 'test_string'
}";
dynamic response = await sg.client.whitelabel.links.subuser.delete(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a Link Whitelabel
// PATCH /whitelabel/links/{id}

string data = @"{
  'default': true
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var id = "test_url_param";
dynamic response = await sg.client.whitelabel.links._(id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Link Whitelabel
// GET /whitelabel/links/{id}

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.links._(id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Link Whitelabel
// DELETE /whitelabel/links/{id}

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.links._(id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate a Link Whitelabel
// POST /whitelabel/links/{id}/validate

var id = "test_url_param";
dynamic response = await sg.client.whitelabel.links._(id).validate.post();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Associate a Link Whitelabel
// POST /whitelabel/links/{link_id}/subuser

string data = @"{
  'username': 'jane@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var link_id = "test_url_param";
dynamic response = await sg.client.whitelabel.links._(link_id).subuser.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

