using System;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
Client client = new Client(apiKey);

////////////////////////////////////////////////////////
// Retrieve a list of scopes for which this user has access.
// GET /scopes

Response response = await client.RequestAsync(method: Client.Methods.GET, urlPath: "scopes");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

