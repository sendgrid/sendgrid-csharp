using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Retrieve email statistics by client type. #
# GET /clients/stats #

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
dynamic response = sg.client.clients.stats.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve stats by a specific client type. #
# GET /clients/{client_type}/stats #

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
var client_type = "test_url_param";
dynamic response = sg.client.clients._(client_type).stats.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

