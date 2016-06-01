using System;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

##################################################
# Retrieve all categories #
# GET /categories #

string queryParams = @"{
  'category': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
dynamic response = sg.client.categories.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve Email Statistics for Categories #
# GET /categories/stats #

string queryParams = @"{
  'aggregated_by': 'day', 
  'categories': 'test_string', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
dynamic response = sg.client.categories.stats.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

##################################################
# Retrieve sums of email stats for each category [Needs: Stats object defined, has category ID?] #
# GET /categories/stats/sums #

string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'start_date': '2016-01-01'
}";
dynamic response = sg.client.categories.stats.sums.get(queryParams: queryParams);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.ResponseBody.ReadAsStringAsync().Result);
Console.WriteLine(response.ResponseHeaders.ToString());
Console.ReadLine();

