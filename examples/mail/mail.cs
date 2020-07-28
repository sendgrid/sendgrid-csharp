using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail; // If you are using the Mail Helper
using System;


var apiKey = Environment.GetEnvironmentVariable("NAME_OF_THE_ENVIRONMENT_VARIABLE_FOR_YOUR_SENDGRID_KEY");
var client = new SendGridClient(apiKey);

////////////////////////////////////////////////////////
// Create a batch ID
// POST /mail/batch

var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "mail/batch");
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// Validate batch ID
// GET /mail/batch/{batch_id}

var batch_id = "test_url_param";
var response = await client.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail/batch/" + batch_id);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

////////////////////////////////////////////////////////
// v3 Mail Send
// POST /mail/send
// This endpoint has a helper, check it out [here](https://github.com/sendgrid/sendgrid-csharp/blob/master/src/SendGrid/Helpers/Mail/README.md).

string data = @"{
  'asm': {
    'group_id': 1, 
    'groups_to_display': [
      1, 
      2, 
      3
    ]
  }, 
  'attachments': [
    {
      'content': '[BASE64 encoded content block here]', 
      'content_id': 'ii_139db99fdb5c3704', 
      'disposition': 'inline', 
      'filename': 'file1.jpg', 
      'name': 'file1', 
      'type': 'jpg'
    }
  ], 
  'batch_id': '[YOUR BATCH ID GOES HERE]', 
  'categories': [
    'category1', 
    'category2'
  ], 
  'content': [
    {
      'type': 'text/html', 
      'value': '<html><p>Hello, world!</p><img src=[CID GOES HERE]></img></html>'
    }
  ], 
  'custom_args': {
    'New Argument 1': 'New Value 1', 
    'activationAttempt': '1', 
    'customerAccountNumber': '[CUSTOMER ACCOUNT NUMBER GOES HERE]'
  }, 
  'from': {
    'email': 'sam.smith@example.com', 
    'name': 'Sam Smith'
  }, 
  'headers': {}, 
  'ip_pool_name': '[YOUR POOL NAME GOES HERE]', 
  'mail_settings': {
    'bcc': {
      'email': 'ben.doe@example.com', 
      'enable': true
    }, 
    'bypass_list_management': {
      'enable': true
    }, 
    'footer': {
      'enable': true, 
      'html': '<p>Thanks</br>The Twilio SendGrid Team</p>', 
      'text': 'Thanks,/n The Twilio SendGrid Team'
    }, 
    'sandbox_mode': {
      'enable': false
    }, 
    'spam_check': {
      'enable': true, 
      'post_to_url': 'http://example.com/compliance', 
      'threshold': 3
    }
  }, 
  'personalizations': [
    {
      'bcc': [
        {
          'email': 'sam.doe@example.com', 
          'name': 'Sam Doe'
        }
      ], 
      'cc': [
        {
          'email': 'jane.doe@example.com', 
          'name': 'Jane Doe'
        }
      ], 
      'custom_args': {
        'New Argument 1': 'New Value 1', 
        'activationAttempt': '1', 
        'customerAccountNumber': '[CUSTOMER ACCOUNT NUMBER GOES HERE]'
      }, 
      'headers': {
        'X-Accept-Language': 'en', 
        'X-Mailer': 'MyApp'
      }, 
      'send_at': 1409348513, 
      'subject': 'Hello, World!', 
      'substitutions': {
        'id': 'substitutions', 
        'type': 'object'
      }, 
      'to': [
        {
          'email': 'john.doe@example.com', 
          'name': 'John Doe'
        }
      ]
    }
  ], 
  'reply_to': {
    'email': 'sam.smith@example.com', 
    'name': 'Sam Smith'
  }, 
  'sections': {
    'section': {
      ':sectionName1': 'section 1 text', 
      ':sectionName2': 'section 2 text'
    }
  }, 
  'send_at': 1409348513, 
  'subject': 'Hello, World!', 
  'template_id': '[YOUR TEMPLATE ID GOES HERE]', 
  'tracking_settings': {
    'click_tracking': {
      'enable': true, 
      'enable_text': true
    }, 
    'ganalytics': {
      'enable': true, 
      'utm_campaign': '[NAME OF YOUR REFERRER SOURCE]', 
      'utm_content': '[USE THIS SPACE TO DIFFERENTIATE YOUR EMAIL FROM ADS]', 
      'utm_medium': '[NAME OF YOUR MARKETING MEDIUM e.g. email]', 
      'utm_name': '[NAME OF YOUR CAMPAIGN]', 
      'utm_term': '[IDENTIFY PAID KEYWORDS HERE]'
    }, 
    'open_tracking': {
      'enable': true, 
      'substitution_tag': '%opentrack'
    }, 
    'subscription_tracking': {
      'enable': true, 
      'html': 'If you would like to unsubscribe and stop receiving these emails <% clickhere %>.', 
      'substitution_tag': '<%click here%>', 
      'text': 'If you would like to unsubscribe and stop receiving these emails <% click here %>.'
    }
  }
}";
Object json = JsonConvert.DeserializeObject<Object>(data);
data = json.ToString();
var response = await client.RequestAsync(method: SendGridClient.Method.POST, urlPath: "mail/send", requestBody: data);
Console.WriteLine(response.StatusCode);
Console.WriteLine(response.Body.ReadAsStringAsync().Result);
Console.WriteLine(response.Headers.ToString());
Console.ReadLine();

