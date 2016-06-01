using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Net;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Retrieve Tracking Settings #
# GET /tracking_settings #

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
dynamic response = sg.client.tracking_settings.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Update Click Tracking Settings #
# PATCH /tracking_settings/click #

string data = @"{
  'enabled': true
}";
dynamic response = sg.client.tracking_settings.click.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Retrieve Click Track Settings #
# GET /tracking_settings/click #

dynamic response = sg.client.tracking_settings.click.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Update Google Analytics Settings #
# PATCH /tracking_settings/google_analytics #

string data = @"{
  'enabled': true, 
  'utm_campaign': 'website', 
  'utm_content': '', 
  'utm_medium': 'email', 
  'utm_source': 'sendgrid.com', 
  'utm_term': ''
}";
dynamic response = sg.client.tracking_settings.google_analytics.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Retrieve Google Analytics Settings #
# GET /tracking_settings/google_analytics #

dynamic response = sg.client.tracking_settings.google_analytics.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Update Open Tracking Settings #
# PATCH /tracking_settings/open #

string data = @"{
  'enabled': true
}";
dynamic response = sg.client.tracking_settings.open.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Get Open Tracking Settings #
# GET /tracking_settings/open #

dynamic response = sg.client.tracking_settings.open.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Update Subscription Tracking Settings #
# PATCH /tracking_settings/subscription #

string data = @"{
  'enabled': true, 
  'html_content': 'html content', 
  'landing': 'landing page html', 
  'plain_content': 'text content', 
  'replace': 'replacement tag', 
  'url': 'url'
}";
dynamic response = sg.client.tracking_settings.subscription.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Retrieve Subscription Tracking Settings #
# GET /tracking_settings/subscription #

dynamic response = sg.client.tracking_settings.subscription.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

