using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Retrieve all blocks
// GET /suppression/blocks

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
dynamic response = await sg.client.suppression.blocks.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete blocks
// DELETE /suppression/blocks

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.suppression.blocks.delete(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific block
// GET /suppression/blocks/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.blocks._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific block
// DELETE /suppression/blocks/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.blocks._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all bounces
// GET /suppression/bounces

string queryParams = @"{
  'end_time': 1, 
  'start_time': 1
}";
dynamic response = await sg.client.suppression.bounces.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete bounces
// DELETE /suppression/bounces

string data = @"{
  'delete_all': true, 
  'emails': [
    'example@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.suppression.bounces.delete(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Bounce
// GET /suppression/bounces/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.bounces._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a bounce
// DELETE /suppression/bounces/{email}

string queryParams = @"{
  'email_address': 'example@example.com'
}";
var email = "test_url_param";
dynamic response = await sg.client.suppression.bounces._(email).delete(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all invalid emails
// GET /suppression/invalid_emails

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
dynamic response = await sg.client.suppression.invalid_emails.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete invalid emails
// DELETE /suppression/invalid_emails

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.suppression.invalid_emails.delete(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific invalid email
// GET /suppression/invalid_emails/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.invalid_emails._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific invalid email
// DELETE /suppression/invalid_emails/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.invalid_emails._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a specific spam report
// GET /suppression/spam_report/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.spam_report._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a specific spam report
// DELETE /suppression/spam_report/{email}

var email = "test_url_param";
dynamic response = await sg.client.suppression.spam_report._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all spam reports
// GET /suppression/spam_reports

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
dynamic response = await sg.client.suppression.spam_reports.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete spam reports
// DELETE /suppression/spam_reports

string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.suppression.spam_reports.delete(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all global suppressions
// GET /suppression/unsubscribes

string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
dynamic response = await sg.client.suppression.unsubscribes.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

