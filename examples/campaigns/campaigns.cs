using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create a Campaign
// POST /campaigns

string data = @"{
  'categories': [
    'spring line'
  ], 
  'custom_unsubscribe_url': '', 
  'html_content': '<html><head><title></title></head><body><p>Check out our spring line!</p></body></html>', 
  'ip_pool': 'marketing', 
  'list_ids': [
    110, 
    124
  ], 
  'plain_content': 'Check out our spring line!', 
  'segment_ids': [
    110
  ], 
  'sender_id': 124451, 
  'subject': 'New Products for Spring!', 
  'suppression_group_id': 42, 
  'title': 'March Newsletter'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.campaigns.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all Campaigns
// GET /campaigns

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
dynamic response = await sg.client.campaigns.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a Campaign
// PATCH /campaigns/{campaign_id}

string data = @"{
  'categories': [
    'summer line'
  ], 
  'html_content': '<html><head><title></title></head><body><p>Check out our summer line!</p></body></html>', 
  'plain_content': 'Check out our summer line!', 
  'subject': 'New Products for Summer!', 
  'title': 'May Newsletter'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a single campaign
// GET /campaigns/{campaign_id}

var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Campaign
// DELETE /campaigns/{campaign_id}

var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a Scheduled Campaign
// PATCH /campaigns/{campaign_id}/schedules

string data = @"{
  'send_at': 1489451436
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Schedule a Campaign
// POST /campaigns/{campaign_id}/schedules

string data = @"{
  'send_at': 1489771528
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// View Scheduled Time of a Campaign
// GET /campaigns/{campaign_id}/schedules

var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Unschedule a Scheduled Campaign
// DELETE /campaigns/{campaign_id}/schedules

var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Send a Campaign
// POST /campaigns/{campaign_id}/schedules/now

var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.now.post();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Send a Test Campaign
// POST /campaigns/{campaign_id}/schedules/test

string data = @"{
  'to': 'your.email@example.com'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var campaign_id = "test_url_param";
dynamic response = await sg.client.campaigns._(campaign_id).schedules.test.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

