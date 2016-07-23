using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

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
dynamic response = await sg.client.ips.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all assigned IPs
// GET /ips/assigned

dynamic response = await sg.client.ips.assigned.get();
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
dynamic response = await sg.client.ips.pools.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IP pools.
// GET /ips/pools

dynamic response = await sg.client.ips.pools.get();
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
dynamic response = await sg.client.ips.pools._(pool_name).put(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IPs in a specified pool.
// GET /ips/pools/{pool_name}

var pool_name = "test_url_param";
dynamic response = await sg.client.ips.pools._(pool_name).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete an IP pool.
// DELETE /ips/pools/{pool_name}

var pool_name = "test_url_param";
dynamic response = await sg.client.ips.pools._(pool_name).delete();
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
dynamic response = await sg.client.ips.pools._(pool_name).ips.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP address from a pool.
// DELETE /ips/pools/{pool_name}/ips/{ip}

var pool_name = "test_url_param";
var ip = "test_url_param";
dynamic response = await sg.client.ips.pools._(pool_name).ips._(ip).delete();
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
dynamic response = await sg.client.ips.warmup.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IPs currently in warmup
// GET /ips/warmup

dynamic response = await sg.client.ips.warmup.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve warmup status for a specific IP address
// GET /ips/warmup/{ip_address}

var ip_address = "test_url_param";
dynamic response = await sg.client.ips.warmup._(ip_address).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Remove an IP from warmup
// DELETE /ips/warmup/{ip_address}

var ip_address = "test_url_param";
dynamic response = await sg.client.ips.warmup._(ip_address).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all IP pools an IP address belongs to
// GET /ips/{ip_address}

var ip_address = "test_url_param";
dynamic response = await sg.client.ips._(ip_address).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

