using System;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
Client client = new Client(apiKey);

////////////////////////////////////////////////////////
// Create a transactional template.
// POST /templates

string data = @"{
  'name': 'example_name'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.POST, urlPath: "templates", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all transactional templates.
// GET /templates

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "templates");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Edit a transactional template.
// PATCH /templates/{template_id}

string data = @"{
  'name': 'new_example_name'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var template_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "templates/" + template_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a single transactional template.
// GET /templates/{template_id}

var template_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "templates/" + template_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a template.
// DELETE /templates/{template_id}

var template_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.DELETE, urlPath: "templates/" + template_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a new transactional template version.
// POST /templates/{template_id}/versions

string data = @"{
  'active': 1, 
  'html_content': '<%body%>', 
  'name': 'example_version_name', 
  'plain_content': '<%body%>', 
  'subject': '<%subject%>', 
  'template_id': 'ddb96bbc-9b92-425e-8979-99464621b543'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var template_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.POST, urlPath: "templates/" + template_id + "/versions", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Edit a transactional template version.
// PATCH /templates/{template_id}/versions/{version_id}

string data = @"{
  'active': 1, 
  'html_content': '<%body%>', 
  'name': 'updated_example_name', 
  'plain_content': '<%body%>', 
  'subject': '<%subject%>'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var template_id = "test_url_param";
var version_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "templates/" + template_id + "/versions/" + version_id, requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific transactional template version.
// GET /templates/{template_id}/versions/{version_id}

var template_id = "test_url_param";
var version_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "templates/" + template_id + "/versions/" + version_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a transactional template version.
// DELETE /templates/{template_id}/versions/{version_id}

var template_id = "test_url_param";
var version_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.DELETE, urlPath: "templates/" + template_id + "/versions/" + version_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Activate a transactional template version.
// POST /templates/{template_id}/versions/{version_id}/activate

var template_id = "test_url_param";
var version_id = "test_url_param";
Response response = await client.RequestAsync(method: Client.Methods.POST, urlPath: "templates/" + template_id + "/versions/" + version_id + "/activate");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

