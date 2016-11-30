using System;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
Client client = new Client(apiKey);

////////////////////////////////////////////////////////
// Returns a list of all partner settings.
// GET /partner_settings

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "partner_settings", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Updates New Relic partner settings.
// PATCH /partner_settings/new_relic

string data = @"{
  'enable_subuser_statistics': true, 
  'enabled': true, 
  'license_key': ''
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
Response response = await client.RequestAsync(method: Client.Methods.PATCH, urlPath: "partner_settings/new_relic", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Returns all New Relic partner settings.
// GET /partner_settings/new_relic

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "partner_settings/new_relic");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

