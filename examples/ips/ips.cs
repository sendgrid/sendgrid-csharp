using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Retrieve all IP addresses
// GET /ips

string queryParams = @"{
  'exclude_whitelabels': 'true', 
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'subuser': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all assigned IPs
// GET /ips/assigned

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/assigned");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create an IP pool.
// POST /ips/pools

string data = @"{
  'name': 'marketing'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/pools", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IP pools.
// GET /ips/pools

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update an IP pools name.
// PUT /ips/pools/{pool_name}

string data = @"{
  'name': 'new_pool_name'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var pool_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "ips/pools/" + pool_name, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IPs in a specified pool.
// GET /ips/pools/{pool_name}

var pool_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools/" + pool_name);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete an IP pool.
// DELETE /ips/pools/{pool_name}

var pool_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add an IP address to a pool
// POST /ips/pools/{pool_name}/ips

string data = @"{
  'ip': '0.0.0.0'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var pool_name = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/pools/" + pool_name + "/ips", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP address from a pool.
// DELETE /ips/pools/{pool_name}/ips/{ip}

var pool_name = "test_url_param";
var ip = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name + "/ips/" + ip);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add an IP to warmup
// POST /ips/warmup

string data = @"{
  'ip': '0.0.0.0'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/warmup", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IPs currently in warmup
// GET /ips/warmup

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve warmup status for a specific IP address
// GET /ips/warmup/{ip_address}

var ip_address = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup/" + ip_address);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP from warmup
// DELETE /ips/warmup/{ip_address}

var ip_address = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/warmup/" + ip_address);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IP pools an IP address belongs to
// GET /ips/{ip_address}

var ip_address = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/" + ip_address);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

