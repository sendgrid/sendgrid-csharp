using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Example
{
    internal class Example
    {
        private static void Main()
        {
            // v3 Mail Helper
            //HelloEmailAsync().Wait(); // this will actually send an email
            //KitchenSinkAsync().Wait(); // this will only send an email if you set SandBox Mode to false

            // v3 Template Example with Mail Helper
            //TemplateWithHelperAsync().Wait();

            // v3 Template Example without Mail Helper
            //TemplateWithoutHelperAsync().Wait();

            // v3 Web API
            ASMGroupsAsync().Wait();
        }

        private static async Task TemplateWithHelperAsync()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY",
                                                               EnvironmentVariableTarget.User);
            Client client = new Client(apiKey: apiKey);

            Email from = new Email("test@example.com");
            string subject = "I'm replacing the subject tag";
            Email to = new Email("test@example.com");
            Content content = new Content("text/html", "I'm replacing the <strong>body tag</strong>");
            Mail mail = new Mail(from, subject, to, content);

            mail.TemplateId = "13b8f94f-bcae-4ec6-b752-70d6cb59f932";
            mail.Personalization[0].AddSubstitution("-name-", "Example User");
            mail.Personalization[0].AddSubstitution("-city-", "Denver");

            Response response = await client.RequestAsync(method: Client.Methods.POST,
                                                          requestBody: mail.Get(),
                                                          urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.ReadLine();
        }

        private static async Task TemplateWithoutHelperAsync()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            Client client = new Client(apiKey);

            string data = @"{
              'personalizations': [
                {
                  'to': [
                    {
                      'email': 'test@example.com'
                    }
                  ],
                  'substitutions': {
                    '-name-': 'Example User',
                    '-city-': 'Denver'
                  },
                  'subject': 'I\'m replacing the subject tag'
                }
              ],
              'from': {
                'email': 'test@example.com'
              },
              'content': [
                {
                  'type': 'text/html',
                  'value': 'I\'m replacing the <strong>body tag</strong>'
                }
              ],
              'template_id': '13b8f94f-bcae-4ec6-b752-70d6cb59f932'
            }";
            //test @example.com
            object json = JsonConvert.DeserializeObject<object>(data);
            Response response = await client.RequestAsync(method: Client.Methods.POST,
                                                          requestBody: json.ToString(),
                                                          urlPath: "mail/send");

            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.ReadLine();
        }

        private static async Task HelloEmailAsync()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            Client client = new Client(apiKey);

            Email from = new Email("test@example.com");
            string subject = "Hello World from the SendGrid CSharp Library";
            Email to = new Email("test@example.com");
            Content content = new Content("text/plain", "Textual content");
            Mail mail = new Mail(from, subject, to, content);
            Email email = new Email("test2@example.com");
            mail.Personalization[0].AddTo(email);

            Response response = await client.RequestAsync(method: Client.Methods.POST,
                                                          requestBody: mail.Get(),
                                                          urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.ReadLine();
        }

        private static async Task KitchenSinkAsync()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            Client client = new Client(apiKey);

            Mail mail = new Mail();

            Email email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            mail.From = email;

            mail.Subject = "Hello World from the SendGrid CSharp Library";

            Personalization personalization = new Personalization();
            email = new Email();
            email.Name = "Example User";
            email.Address = "test1@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test2@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test3@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test4@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test5@example.com";
            personalization.AddBcc(email);
            personalization.Subject = "Thank you for signing up, %name%";
            personalization.AddHeader("X-Test", "True");
            personalization.AddHeader("X-Mock", "True");
            personalization.AddSubstitution("%name%", "Example User");
            personalization.AddSubstitution("%city%", "Denver");
            personalization.AddCustomArgs("marketing", "false");
            personalization.AddCustomArgs("transactional", "true");
            personalization.SendAt = 1461775051;
            mail.AddPersonalization(personalization);

            personalization = new Personalization();
            email = new Email();
            email.Name = "Example User";
            email.Address = "test1@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test2@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test3@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test4@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test5@example.com";
            personalization.AddBcc(email);
            personalization.Subject = "Thank you for signing up, %name%";
            personalization.AddHeader("X-Test", "True");
            personalization.AddHeader("X-Mock", "True");
            personalization.AddSubstitution("%name%", "Example User");
            personalization.AddSubstitution("%city%", "Denver");
            personalization.AddCustomArgs("marketing", "false");
            personalization.AddCustomArgs("transactional", "true");
            personalization.SendAt = 1461775051;
            mail.AddPersonalization(personalization);

            Content content = new Content();
            content.Type = "text/plain";
            content.Value = "Textual content";
            mail.AddContent(content);
            content = new Content();
            content.Type = "text/html";
            content.Value = "<html><body>HTML content</body></html>";
            mail.AddContent(content);
            content = new Content();
            content.Type = "text/calendar";
            content.Value = "Party Time!!";
            mail.AddContent(content);

            Attachment attachment = new Attachment();
            attachment.Content = "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12";
            attachment.Type = "application/pdf";
            attachment.Filename = "balance_001.pdf";
            attachment.Disposition = "attachment";
            attachment.ContentId = "Balance Sheet";
            mail.AddAttachment(attachment);

            attachment = new Attachment();
            attachment.Content = "BwdW";
            attachment.Type = "image/png";
            attachment.Filename = "banner.png";
            attachment.Disposition = "inline";
            attachment.ContentId = "Banner";
            mail.AddAttachment(attachment);

            mail.TemplateId = "13b8f94f-bcae-4ec6-b752-70d6cb59f932";

            mail.AddHeader("X-Day", "Monday");
            mail.AddHeader("X-Month", "January");

            mail.AddSection("%section1", "Substitution for Section 1 Tag");
            mail.AddSection("%section2", "Substitution for Section 2 Tag");

            mail.AddCategory("customer");
            mail.AddCategory("vip");

            mail.AddCustomArgs("campaign", "welcome");
            mail.AddCustomArgs("sequence", "2");

            ASM asm = new ASM();
            asm.GroupId = 3;
            List<int> groups_to_display = new List<int>()
            {
                1, 4, 5
            };
            asm.GroupsToDisplay = groups_to_display;
            mail.Asm = asm;

            mail.SendAt = 1461775051;

            mail.SetIpPoolId = "23";

            // This must be a valid [batch ID](https://sendgrid.com/docs/API_Reference/SMTP_API/scheduling_parameters.html)
            // mail.BatchId = "some_batch_id";

            MailSettings mailSettings = new MailSettings();
            BCCSettings bccSettings = new BCCSettings();
            bccSettings.Enable = true;
            bccSettings.Email = "test@example.com";
            mailSettings.BccSettings = bccSettings;
            BypassListManagement bypassListManagement = new BypassListManagement();
            bypassListManagement.Enable = true;
            mailSettings.BypassListManagement = bypassListManagement;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.Enable = true;
            footerSettings.Text = "Some Footer Text";
            footerSettings.Html = "<bold>Some HTML Here</bold>";
            mailSettings.FooterSettings = footerSettings;
            SandboxMode sandboxMode = new SandboxMode();
            sandboxMode.Enable = true;
            mailSettings.SandboxMode = sandboxMode;
            SpamCheck spamCheck = new SpamCheck();
            spamCheck.Enable = true;
            spamCheck.Threshold = 1;
            spamCheck.PostToUrl = "https://gotchya.example.com";
            mailSettings.SpamCheck = spamCheck;
            mail.MailSettings = mailSettings;

            TrackingSettings trackingSettings = new TrackingSettings();
            ClickTracking clickTracking = new ClickTracking();
            clickTracking.Enable = true;
            clickTracking.EnableText = false;
            trackingSettings.ClickTracking = clickTracking;
            OpenTracking openTracking = new OpenTracking();
            openTracking.Enable = true;
            openTracking.SubstitutionTag = "Optional tag to replace with the open image in the body of the message";
            trackingSettings.OpenTracking = openTracking;
            SubscriptionTracking subscriptionTracking = new SubscriptionTracking();
            subscriptionTracking.Enable = true;
            subscriptionTracking.Text = "text to insert into the text/plain portion of the message";
            subscriptionTracking.Html = "<bold>HTML to insert into the text/html portion of the message</bold>";
            subscriptionTracking.SubstitutionTag = "text to insert into the text/plain portion of the message";
            trackingSettings.SubscriptionTracking = subscriptionTracking;
            Ganalytics ganalytics = new Ganalytics();
            ganalytics.Enable = true;
            ganalytics.UtmCampaign = "some campaign";
            ganalytics.UtmContent = "some content";
            ganalytics.UtmMedium = "some medium";
            ganalytics.UtmSource = "some source";
            ganalytics.UtmTerm = "some term";
            trackingSettings.Ganalytics = ganalytics;
            mail.TrackingSettings = trackingSettings;

            email = new Email();
            email.Address = "test@example.com";
            mail.ReplyTo = email;

            Response response = await client.RequestAsync(method: Client.Methods.POST,
                                                          requestBody: mail.Get(),
                                                          urlPath: "mail/send");
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.ReadLine();
        }

        private static async Task ASMGroupsAsync()
        {
            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY", EnvironmentVariableTarget.User);
            Client client = new Client(apiKey);

            // GET Collection
            string queryParams = @"{
                'limit': 100
            }";
            Response response = await client.RequestAsync(method: Client.Methods.GET,
                                                          urlPath: "asm/groups",
                                                          queryParams: queryParams);
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to POST.");
            Console.ReadLine();

            // POST
            string requestBody = @"{
              'description': 'Suggestions for products our users might like.', 
              'is_default': false, 
              'name': 'Magic Products'
            }";
            object json = JsonConvert.DeserializeObject<object>(requestBody);
            response = await client.RequestAsync(method: Client.Methods.POST,
                                                 urlPath: "asm/groups",
                                                 requestBody: json.ToString());
            var ds_response = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Body.ReadAsStringAsync().Result);
            string group_id = ds_response["id"].ToString();
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to GET single.");
            Console.ReadLine();

            // GET Single
            response = await client.RequestAsync(method: Client.Methods.GET,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to continue to PATCH.");
            Console.ReadLine();

            // PATCH
            requestBody = @"{
                'name': 'Cool Magic Products'
            }";
            json = JsonConvert.DeserializeObject<object>(requestBody);

            response = await client.RequestAsync(method: Client.Methods.PATCH,
                                                 urlPath: string.Format("asm/groups/{0}", group_id),
                                                 requestBody: json.ToString());
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Body.ReadAsStringAsync().Result);
            Console.WriteLine(response.Headers.ToString());

            Console.WriteLine("\n\nPress any key to continue to PUT.");
            Console.ReadLine();

            // DELETE
            response = await client.RequestAsync(method: Client.Methods.DELETE,
                                                 urlPath: string.Format("asm/groups/{0}", group_id));
            Console.WriteLine(response.StatusCode);
            Console.WriteLine(response.Headers.ToString());
            Console.WriteLine("\n\nPress any key to exit.");
            Console.ReadLine();

        }
    }
}
