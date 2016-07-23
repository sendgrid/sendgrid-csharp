using System;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using Newtonsoft.Json; // You can generate your JSON string yourelf or with another library if you prefer

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

////////////////////////////////////////////////////////
// Create a Custom Field
// POST /contactdb/custom_fields

string data = @"{
  'name': 'pet', 
  'type': 'text'
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
dynamic response = await sg.client.contactdb.custom_fields.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all custom fields
// GET /contactdb/custom_fields

dynamic response = await sg.client.contactdb.custom_fields.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Custom Field
// GET /contactdb/custom_fields/{custom_field_id}

var custom_field_id = "test_url_param";
dynamic response = await sg.client.contactdb.custom_fields._(custom_field_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Custom Field
// DELETE /contactdb/custom_fields/{custom_field_id}

var custom_field_id = "test_url_param";
dynamic response = await sg.client.contactdb.custom_fields._(custom_field_id).delete();
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
dynamic response = await sg.client.contactdb.lists.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all lists
// GET /contactdb/lists

dynamic response = await sg.client.contactdb.lists.get();
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
dynamic response = await sg.client.contactdb.lists.delete(requestBody: data);
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
dynamic response = await sg.client.contactdb.lists._(list_id).patch(requestBody: data, queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.lists._(list_id).get(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.lists._(list_id).delete(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.lists._(list_id).recipients.post(requestBody: data);
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
dynamic response = await sg.client.contactdb.lists._(list_id).recipients.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Add a Single Recipient to a List
// POST /contactdb/lists/{list_id}/recipients/{recipient_id}

var list_id = "test_url_param";
var recipient_id = "test_url_param";
dynamic response = await sg.client.contactdb.lists._(list_id).recipients._(recipient_id).post();
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
dynamic response = await sg.client.contactdb.lists._(list_id).recipients._(recipient_id).delete(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.recipients.patch(requestBody: data);
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
dynamic response = await sg.client.contactdb.recipients.post(requestBody: data);
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
dynamic response = await sg.client.contactdb.recipients.get(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.recipients.delete(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve the count of billable recipients
// GET /contactdb/recipients/billable_count

dynamic response = await sg.client.contactdb.recipients.billable_count.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a Count of Recipients
// GET /contactdb/recipients/count

dynamic response = await sg.client.contactdb.recipients.count.get();
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
dynamic response = await sg.client.contactdb.recipients.search.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve a single recipient
// GET /contactdb/recipients/{recipient_id}

var recipient_id = "test_url_param";
dynamic response = await sg.client.contactdb.recipients._(recipient_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Delete a Recipient
// DELETE /contactdb/recipients/{recipient_id}

var recipient_id = "test_url_param";
dynamic response = await sg.client.contactdb.recipients._(recipient_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve the lists that a recipient is on
// GET /contactdb/recipients/{recipient_id}/lists

var recipient_id = "test_url_param";
dynamic response = await sg.client.contactdb.recipients._(recipient_id).lists.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve reserved fields
// GET /contactdb/reserved_fields

dynamic response = await sg.client.contactdb.reserved_fields.get();
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
dynamic response = await sg.client.contactdb.segments.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Retrieve all segments
// GET /contactdb/segments

dynamic response = await sg.client.contactdb.segments.get();
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
dynamic response = await sg.client.contactdb.segments._(segment_id).patch(requestBody: data, queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.segments._(segment_id).get(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.segments._(segment_id).delete(queryParams: queryParams);
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
dynamic response = await sg.client.contactdb.segments._(segment_id).recipients.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

