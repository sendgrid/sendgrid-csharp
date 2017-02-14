using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Create a Custom Field
// POST /contactdb/custom_fields

string data = @"{
  'name': 'pet', 
  'type': 'text'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/custom_fields", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all custom fields
// GET /contactdb/custom_fields

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Custom Field
// GET /contactdb/custom_fields/{custom_field_id}

var custom_field_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields/" + custom_field_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Custom Field
// DELETE /contactdb/custom_fields/{custom_field_id}

var custom_field_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/custom_fields/" + custom_field_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a List
// POST /contactdb/lists

string data = @"{
  'name': 'your list name'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all lists
// GET /contactdb/lists

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete Multiple lists
// DELETE /contactdb/lists

string data = @"[
  1, 
  2, 
  3, 
  4
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a List
// PATCH /contactdb/lists/{list_id}

string data = @"{
  'name': 'newlistname'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
string queryParams = @"{
  'list_id': 1
}";
var list_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/lists/" + list_id, requestBody: data, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a single list
// GET /contactdb/lists/{list_id}

string queryParams = @"{
  'list_id': 1
}";
var list_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a List
// DELETE /contactdb/lists/{list_id}

string queryParams = @"{
  'delete_contacts': 'true'
}";
var list_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add Multiple Recipients to a List
// POST /contactdb/lists/{list_id}/recipients

string data = @"[
  'recipient_id1', 
  'recipient_id2'
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var list_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists/" + list_id + "/recipients", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all recipients on a List
// GET /contactdb/lists/{list_id}/recipients

string queryParams = @"{
  'list_id': 1, 
  'page': 1, 
  'page_size': 1
}";
var list_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists/" + list_id + "/recipients", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add a Single Recipient to a List
// POST /contactdb/lists/{list_id}/recipients/{recipient_id}

var list_id = "test_url_param";
var recipient_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists/" + list_id + "/recipients/" + recipient_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Single Recipient from a Single List
// DELETE /contactdb/lists/{list_id}/recipients/{recipient_id}

string queryParams = @"{
  'list_id': 1, 
  'recipient_id': 1
}";
var list_id = "test_url_param";
var recipient_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists/" + list_id + "/recipients/" + recipient_id, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update Recipient
// PATCH /contactdb/recipients

string data = @"[
  {
    'email': 'jones@example.com', 
    'first_name': 'Guy', 
    'last_name': 'Jones'
  }
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/recipients", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add recipients
// POST /contactdb/recipients

string data = @"[
  {
    'age': 25, 
    'email': 'example@example.com', 
    'first_name': '', 
    'last_name': 'User'
  }, 
  {
    'age': 25, 
    'email': 'example2@example.com', 
    'first_name': 'Example', 
    'last_name': 'User'
  }
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/recipients", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve recipients
// GET /contactdb/recipients

string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete Recipient
// DELETE /contactdb/recipients

string data = @"[
  'recipient_id1', 
  'recipient_id2'
]";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/recipients", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve the count of billable recipients
// GET /contactdb/recipients/billable_count

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/billable_count");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Count of Recipients
// GET /contactdb/recipients/count

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/count");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve recipients matching search criteria
// GET /contactdb/recipients/search

string queryParams = @"{
  '{field_name}': 'test_string'
}";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/search", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a single recipient
// GET /contactdb/recipients/{recipient_id}

var recipient_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Recipient
// DELETE /contactdb/recipients/{recipient_id}

var recipient_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/recipients/" + recipient_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve the lists that a recipient is on
// GET /contactdb/recipients/{recipient_id}/lists

var recipient_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id + "/lists");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve reserved fields
// GET /contactdb/reserved_fields

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/reserved_fields");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Create a Segment
// POST /contactdb/segments

string data = @"{
  'conditions': [
    {
      'and_or': '', 
      'field': 'last_name', 
      'operator': 'eq', 
      'value': 'Miller'
    }, 
    {
      'and_or': 'and', 
      'field': 'last_clicked', 
      'operator': 'gt', 
      'value': '01/02/2015'
    }, 
    {
      'and_or': 'or', 
      'field': 'clicks.campaign_identifier', 
      'operator': 'eq', 
      'value': '513'
    }
  ], 
  'list_id': 4, 
  'name': 'Last Name Miller'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/segments", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all segments
// GET /contactdb/segments

var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Update a segment
// PATCH /contactdb/segments/{segment_id}

string data = @"{
  'conditions': [
    {
      'and_or': '', 
      'field': 'last_name', 
      'operator': 'eq', 
      'value': 'Miller'
    }
  ], 
  'list_id': 5, 
  'name': 'The Millers'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
string queryParams = @"{
  'segment_id': 'test_string'
}";
var segment_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/segments/" + segment_id, requestBody: data, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a segment
// GET /contactdb/segments/{segment_id}

string queryParams = @"{
  'segment_id': 1
}";
var segment_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a segment
// DELETE /contactdb/segments/{segment_id}

string queryParams = @"{
  'delete_contacts': 'true'
}";
var segment_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve recipients on a segment
// GET /contactdb/segments/{segment_id}/recipients

string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
var segment_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id + "/recipients", queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

