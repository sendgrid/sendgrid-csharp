using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Create API keys #
# POST /api_keys #

string data = @"{
  'name': 'My API Key', 
  'scopes': [
    'mail.send', 
    'alerts.create', 
    'alerts.read'
  ]
}";
dynamic response = sg.client.api_keys.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve all API Keys belonging to the authenticated user #
# GET /api_keys #

dynamic response = sg.client.api_keys.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Update the name & scopes of an API Key #
# PUT /api_keys/{api_key_id} #

string data = @"{
  'name': 'A New Hope', 
  'scopes': [
    'user.profile.read', 
    'user.profile.update'
  ]
}";
var api_key_id = "test_url_param";
dynamic response = sg.client.api_keys._(api_key_id).put(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Update API keys #
# PATCH /api_keys/{api_key_id} #

string data = @"{
  'name': 'A New Hope'
}";
var api_key_id = "test_url_param";
dynamic response = sg.client.api_keys._(api_key_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve an existing API Key #
# GET /api_keys/{api_key_id} #

var api_key_id = "test_url_param";
dynamic response = sg.client.api_keys._(api_key_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Delete API keys #
# DELETE /api_keys/{api_key_id} #

var api_key_id = "test_url_param";
dynamic response = sg.client.api_keys._(api_key_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

