using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Create a Group #
# POST /asm/groups #

string data = @"{
  'description': 'A group description', 
  'is_default': false, 
  'name': 'A group name'
}";
dynamic response = sg.client.asm.groups.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve all suppression groups associated with the user. #
# GET /asm/groups #

dynamic response = sg.client.asm.groups.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Update a suppression group. #
# PATCH /asm/groups/{group_id} #

string data = @"{
  'description': 'Suggestions for items our users might like.', 
  'id': 103, 
  'name': 'Item Suggestions'
}";
var group_id = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Get information on a single suppression group. #
# GET /asm/groups/{group_id} #

var group_id = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Delete a suppression group. #
# DELETE /asm/groups/{group_id} #

var group_id = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Add suppressions to a suppression group #
# POST /asm/groups/{group_id}/suppressions #

string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
var group_id = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).suppressions.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve all suppressions for a suppression group #
# GET /asm/groups/{group_id}/suppressions #

var group_id = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).suppressions.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Delete a suppression from a suppression group #
# DELETE /asm/groups/{group_id}/suppressions/{email} #

var group_id = "test_url_param";
var email = "test_url_param";
dynamic response = sg.client.asm.groups._(group_id).suppressions._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Add recipient addresses to the global suppression group. #
# POST /asm/suppressions/global #

string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
dynamic response = sg.client.asm.suppressions.global.post(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve a Global Suppression #
# GET /asm/suppressions/global/{email} #

var email = "test_url_param";
dynamic response = sg.client.asm.suppressions.global._(email).get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Delete a Global Suppression #
# DELETE /asm/suppressions/global/{email} #

var email = "test_url_param";
dynamic response = sg.client.asm.suppressions.global._(email).delete();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

