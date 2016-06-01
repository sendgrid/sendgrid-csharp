using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Net;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Returns a list of all partner settings. #
# GET /partner_settings #

string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
dynamic response = sg.client.partner_settings.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Updates New Relic partner settings. #
# PATCH /partner_settings/new_relic #

string data = @"{
  'enable_subuser_statistics': true, 
  'enabled': true, 
  'license_key': ''
}";
dynamic response = sg.client.partner_settings.new_relic.patch(requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

##################################################
# Returns all New Relic partner settings. #
# GET /partner_settings/new_relic #

dynamic response = sg.client.partner_settings.new_relic.get();
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());

