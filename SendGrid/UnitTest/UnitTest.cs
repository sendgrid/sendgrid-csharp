using System;
using NUnit.Framework;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;

namespace UnitTest
{

    [TestFixture]
    public class UnitTests
    {
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        static string host = "http://localhost:4010";
        public dynamic sg = new SendGrid.SendGridAPIClient(_apiKey, host);
        Process process = new Process();

        [TestFixtureSetUp]
        public void Init()
        {
            if (Environment.GetEnvironmentVariable("TRAVIS") != "true")
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Trace.WriteLine("Starting Prism (~20 seconds)");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.FileName = "prism.exe";
                startInfo.Arguments = "run -s https://raw.githubusercontent.com/sendgrid/sendgrid-oai/master/oai_stoplight.json";
                process.StartInfo = startInfo;
                process.Start();
                System.Threading.Thread.Sleep(15000);
            }
            else
            {
                System.Threading.Thread.Sleep(15000);
            }
        }

        // Base case for sending an email
        [Test]
        public void TestHelloEmail()
        {
            Mail mail = new Mail();

            Email email = new Email();
            email.Address = "test@example.com";
            mail.From = email;

            Personalization personalization = new Personalization();
            email = new Email();
            email.Address = "test@example.com";
            personalization.AddTo(email);
            mail.AddPersonalization(personalization);

            mail.Subject = "Hello World from the SendGrid CSharp Library";

            Content content = new Content();
            content.Type = "text/plain";
            content.Value = "Textual content";
            mail.AddContent(content);
            content = new Content();
            content.Type = "text/html";
            content.Value = "<html><body>HTML content</body></html>";
            mail.AddContent(content);

            String ret = mail.Get();
            String final = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(ret),
                                Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
            Assert.AreEqual(final, "{\"from\":{\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}]}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"}]}");
        }

        // All paramaters available for sending an email
        [Test]
        public void TestKitchenSink()
        {
            Mail mail = new Mail();

            Email email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            mail.From = email;

            mail.Subject = "Hello World from the SendGrid CSharp Library";

            Personalization personalization = new Personalization();
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
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
            email.Address = "test@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
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

            String ret = mail.Get();
            String final = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(ret),
                                Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Include });
            Assert.AreEqual(final, "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2\":\"Substitution for Section 2 Tag\"},\"categories\":[\"customer\",\"vip\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence\":\"2\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"},\"bypass_list_management\":{\"enable\":true},\"footer\":{\"enable\":true,\"text\":\"Some Footer Text\",\"html\":\"<bold>Some HTML Here</bold>\"},\"sandbox_mode\":{\"enable\":true},\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"https://gotchya.example.com\"}},\"tracking_settings\":{\"click_tracking\":{\"enable\":true,\"enable_text\":false},\"open_tracking\":{\"enable\":true,\"substitution_tag\":\"Optional tag to replace with the open image in the body of the message\"},\"subscription_tracking\":{\"enable\":true,\"text\":\"text to insert into the text/plain portion of the message\",\"html\":\"<bold>HTML to insert into the text/html portion of the message</bold>\",\"substitution_tag\":\"text to insert into the text/plain portion of the message\"},\"ganalytics\":{\"enable\":true,\"utm_source\":\"some source\",\"utm_medium\":\"some medium\",\"utm_term\":\"some term\",\"utm_content\":\"some content\",\"utm_campaign\":\"some campaign\"}},\"reply_to\":{\"email\":\"test@example.com\"}}");
        }

        // All paramaters available for sending an email
        [Test]
        public void TestKitchenSinkInverse()
        {
            Mail mail = new Mail();

            Email email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            mail.From = email;

            mail.Subject = "Hello World from the SendGrid CSharp Library";

            Personalization personalization = new Personalization();
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
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
            email.Address = "test@example.com";
            personalization.AddTo(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddCc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
            personalization.AddBcc(email);
            email = new Email();
            email.Name = "Example User";
            email.Address = "test@example.com";
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
            bccSettings.Enable = false;
            mailSettings.BccSettings = bccSettings;
            BypassListManagement bypassListManagement = new BypassListManagement();
            bypassListManagement.Enable = false;
            mailSettings.BypassListManagement = bypassListManagement;
            FooterSettings footerSettings = new FooterSettings();
            footerSettings.Enable = false;
            mailSettings.FooterSettings = footerSettings;
            SandboxMode sandboxMode = new SandboxMode();
            sandboxMode.Enable = false;
            mailSettings.SandboxMode = sandboxMode;
            SpamCheck spamCheck = new SpamCheck();
            spamCheck.Enable = false;
            mailSettings.SpamCheck = spamCheck;
            mail.MailSettings = mailSettings;

            TrackingSettings trackingSettings = new TrackingSettings();
            ClickTracking clickTracking = new ClickTracking();
            clickTracking.Enable = false;
            trackingSettings.ClickTracking = clickTracking;
            OpenTracking openTracking = new OpenTracking();
            openTracking.Enable = false;
            trackingSettings.OpenTracking = openTracking;
            SubscriptionTracking subscriptionTracking = new SubscriptionTracking();
            subscriptionTracking.Enable = false;
            trackingSettings.SubscriptionTracking = subscriptionTracking;
            Ganalytics ganalytics = new Ganalytics();
            ganalytics.Enable = false;
            trackingSettings.Ganalytics = ganalytics;
            mail.TrackingSettings = trackingSettings;

            email = new Email();
            email.Address = "test@example.com";
            mail.ReplyTo = email;

            String ret = mail.Get();
            String final = JsonConvert.SerializeObject(JsonConvert.DeserializeObject(ret),
                                Formatting.None,
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
            Assert.AreEqual(final, "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2\":\"Substitution for Section 2 Tag\"},\"categories\":[\"customer\",\"vip\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence\":\"2\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":false},\"bypass_list_management\":{\"enable\":false},\"footer\":{\"enable\":false},\"sandbox_mode\":{\"enable\":false},\"spam_check\":{\"enable\":false}},\"tracking_settings\":{\"click_tracking\":{\"enable\":false},\"open_tracking\":{\"enable\":false},\"subscription_tracking\":{\"enable\":false},\"ganalytics\":{\"enable\":false}},\"reply_to\":{\"email\":\"test@example.com\"}}");
        }

        [Test]
        public async void test_access_settings_activity_get()
        {
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.access_settings.activity.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_access_settings_whitelist_post()
        {
            string data = @"{
  'ips': [
    {
      'ip': '192.168.1.1'
    }, 
    {
      'ip': '192.*.*.*'
    }, 
    {
      'ip': '192.168.1.3/32'
    }
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.access_settings.whitelist.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_access_settings_whitelist_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.access_settings.whitelist.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_access_settings_whitelist_delete()
        {
            string data = @"{
  'ids': [
    1, 
    2, 
    3
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.access_settings.whitelist.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_access_settings_whitelist__rule_id__get()
        {
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.access_settings.whitelist._(rule_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_access_settings_whitelist__rule_id__delete()
        {
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.access_settings.whitelist._(rule_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_alerts_post()
        {
            string data = @"{
  'email_to': 'example@example.com', 
  'frequency': 'daily', 
  'type': 'stats_notification'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.alerts.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_alerts_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.alerts.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_alerts__alert_id__patch()
        {
            string data = @"{
  'email_to': 'example@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.alerts._(alert_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_alerts__alert_id__get()
        {
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.alerts._(alert_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_alerts__alert_id__delete()
        {
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.alerts._(alert_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_api_keys_post()
        {
            string data = @"{
  'name': 'My API Key', 
  'sample': 'data', 
  'scopes': [
    'mail.send', 
    'alerts.create', 
    'alerts.read'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.api_keys.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_api_keys_get()
        {
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.api_keys.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_api_keys__api_key_id__put()
        {
            string data = @"{
  'name': 'A New Hope', 
  'scopes': [
    'user.profile.read', 
    'user.profile.update'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.api_keys._(api_key_id).put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_api_keys__api_key_id__patch()
        {
            string data = @"{
  'name': 'A New Hope'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.api_keys._(api_key_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_api_keys__api_key_id__get()
        {
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.api_keys._(api_key_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_api_keys__api_key_id__delete()
        {
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.api_keys._(api_key_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_asm_groups_post()
        {
            string data = @"{
  'description': 'Suggestions for products our users might like.', 
  'is_default': true, 
  'name': 'Product Suggestions'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.asm.groups.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_asm_groups_get()
        {
            string queryParams = @"{
  'id': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.groups.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_groups__group_id__patch()
        {
            string data = @"{
  'description': 'Suggestions for items our users might like.', 
  'id': 103, 
  'name': 'Item Suggestions'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.asm.groups._(group_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_asm_groups__group_id__get()
        {
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.groups._(group_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_groups__group_id__delete()
        {
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.asm.groups._(group_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_asm_groups__group_id__suppressions_post()
        {
            string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.asm.groups._(group_id).suppressions.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_asm_groups__group_id__suppressions_get()
        {
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.groups._(group_id).suppressions.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_groups__group_id__suppressions_search_post()
        {
            string data = @"{
  'recipient_emails': [
    'exists1@example.com', 
    'exists2@example.com', 
    'doesnotexists@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.groups._(group_id).suppressions.search.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_groups__group_id__suppressions__email__delete()
        {
            var group_id = "test_url_param";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.asm.groups._(group_id).suppressions._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_asm_suppressions_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.suppressions.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_suppressions_global_post()
        {
            string data = @"{
  'recipient_emails': [
    'test1@example.com', 
    'test2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.asm.suppressions.global.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_asm_suppressions_global__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.suppressions.global._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_asm_suppressions_global__email__delete()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.asm.suppressions.global._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_asm_suppressions__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.asm.suppressions._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_browsers_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'browsers': 'test_string', 
  'end_date': '2016-04-01', 
  'limit': 'test_string', 
  'offset': 'test_string', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.browsers.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns_post()
        {
            string data = @"{
  'categories': [
    'spring line'
  ], 
  'custom_unsubscribe_url': '', 
  'html_content': '<html><head><title></title></head><body><p>Check out our spring line!</p></body></html>', 
  'ip_pool': 'marketing', 
  'list_ids': [
    110, 
    124
  ], 
  'plain_content': 'Check out our spring line!', 
  'segment_ids': [
    110
  ], 
  'sender_id': 124451, 
  'subject': 'New Products for Spring!', 
  'suppression_group_id': 42, 
  'title': 'March Newsletter'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.campaigns.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_campaigns_get()
        {
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.campaigns.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns__campaign_id__patch()
        {
            string data = @"{
  'categories': [
    'summer line'
  ], 
  'html_content': '<html><head><title></title></head><body><p>Check out our summer line!</p></body></html>', 
  'plain_content': 'Check out our summer line!', 
  'subject': 'New Products for Summer!', 
  'title': 'May Newsletter'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.campaigns._(campaign_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns__campaign_id__get()
        {
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.campaigns._(campaign_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns__campaign_id__delete()
        {
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.campaigns._(campaign_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_patch()
        {
            string data = @"{
  'send_at': 1489451436
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_post()
        {
            string data = @"{
  'send_at': 1489771528
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_get()
        {
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_delete()
        {
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_now_post()
        {
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.now.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_campaigns__campaign_id__schedules_test_post()
        {
            string data = @"{
  'to': 'your.email@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.campaigns._(campaign_id).schedules.test.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_categories_get()
        {
            string queryParams = @"{
  'category': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.categories.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_categories_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'categories': 'test_string', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.categories.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_categories_stats_sums_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.categories.stats.sums.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_clients_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.clients.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_clients__client_type__stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
            var client_type = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.clients._(client_type).stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_custom_fields_post()
        {
            string data = @"{
  'name': 'pet', 
  'type': 'text'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.custom_fields.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_custom_fields_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.custom_fields.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_custom_fields__custom_field_id__get()
        {
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.custom_fields._(custom_field_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_custom_fields__custom_field_id__delete()
        {
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            dynamic response = await sg.client.contactdb.custom_fields._(custom_field_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Accepted);
        }

        [Test]
        public async void test_contactdb_lists_post()
        {
            string data = @"{
  'name': 'your list name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.lists.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_lists_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.lists.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_lists_delete()
        {
            string data = @"[
  1, 
  2, 
  3, 
  4
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.contactdb.lists.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_contactdb_lists__list_id__patch()
        {
            string data = @"{
  'name': 'newlistname'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            string queryParams = @"{
  'list_id': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.lists._(list_id).patch(requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_lists__list_id__get()
        {
            string queryParams = @"{
  'list_id': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.lists._(list_id).get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_lists__list_id__delete()
        {
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            dynamic response = await sg.client.contactdb.lists._(list_id).delete(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Accepted);
        }

        [Test]
        public async void test_contactdb_lists__list_id__recipients_post()
        {
            string data = @"[
  'recipient_id1', 
  'recipient_id2'
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.lists._(list_id).recipients.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_lists__list_id__recipients_get()
        {
            string queryParams = @"{
  'list_id': 1, 
  'page': 1, 
  'page_size': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.lists._(list_id).recipients.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_lists__list_id__recipients__recipient_id__post()
        {
            var list_id = "test_url_param";
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.lists._(list_id).recipients._(recipient_id).post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_lists__list_id__recipients__recipient_id__delete()
        {
            string queryParams = @"{
  'list_id': 1, 
  'recipient_id': 1
}";
            var list_id = "test_url_param";
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.contactdb.lists._(list_id).recipients._(recipient_id).delete(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_contactdb_recipients_patch()
        {
            string data = @"[
  {
    'email': 'jones@example.com', 
    'first_name': 'Guy', 
    'last_name': 'Jones'
  }
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.recipients.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_recipients_post()
        {
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
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.contactdb.recipients.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_contactdb_recipients_get()
        {
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients_delete()
        {
            string data = @"[
  'recipient_id1', 
  'recipient_id2'
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients_billable_count_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients.billable_count.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients_count_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients.count.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients_search_get()
        {
            string queryParams = @"{
  '{field_name}': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients.search.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients__recipient_id__get()
        {
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients._(recipient_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_recipients__recipient_id__delete()
        {
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.contactdb.recipients._(recipient_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_contactdb_recipients__recipient_id__lists_get()
        {
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.recipients._(recipient_id).lists.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_reserved_fields_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.reserved_fields.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_segments_post()
        {
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
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.segments.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_segments_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.segments.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_segments__segment_id__patch()
        {
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
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.segments._(segment_id).patch(requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_segments__segment_id__get()
        {
            string queryParams = @"{
  'segment_id': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.segments._(segment_id).get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_contactdb_segments__segment_id__delete()
        {
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.contactdb.segments._(segment_id).delete(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_contactdb_segments__segment_id__recipients_get()
        {
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.contactdb.segments._(segment_id).recipients.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_devices_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.devices.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_geo_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'country': 'US', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.geo.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_get()
        {
            string queryParams = @"{
  'exclude_whitelabels': 'true', 
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'subuser': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_assigned_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.assigned.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_pools_post()
        {
            string data = @"{
  'name': 'marketing'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.pools.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_pools_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.pools.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_pools__pool_name__put()
        {
            string data = @"{
  'name': 'new_pool_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.pools._(pool_name).put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_pools__pool_name__get()
        {
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.pools._(pool_name).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_pools__pool_name__delete()
        {
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.ips.pools._(pool_name).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_ips_pools__pool_name__ips_post()
        {
            string data = @"{
  'ip': '0.0.0.0'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.ips.pools._(pool_name).ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_ips_pools__pool_name__ips__ip__delete()
        {
            var pool_name = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.ips.pools._(pool_name).ips._(ip).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_ips_warmup_post()
        {
            string data = @"{
  'ip': '0.0.0.0'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.warmup.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_warmup_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.warmup.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_warmup__ip_address__get()
        {
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips.warmup._(ip_address).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_ips_warmup__ip_address__delete()
        {
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.ips.warmup._(ip_address).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_ips__ip_address__get()
        {
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.ips._(ip_address).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_batch_post()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.mail.batch.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_mail_batch__batch_id__get()
        {
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail.batch._(batch_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_send_post()
        {
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
      'html': '<p>Thanks</br>The SendGrid Team</p>', 
      'text': 'Thanks,/n The SendGrid Team'
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
      'text': 'If you would like to unsubscribe and stop receiveing these emails <% click here %>.'
    }
  }
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            dynamic response = await sg.client.mail.send.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Accepted);
        }

        [Test]
        public async void test_mail_settings_get()
        {
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_address_whitelist_patch()
        {
            string data = @"{
  'enabled': true, 
  'list': [
    'email1@example.com', 
    'example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.address_whitelist.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_address_whitelist_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.address_whitelist.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_bcc_patch()
        {
            string data = @"{
  'email': 'email@example.com', 
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.bcc.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_bcc_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.bcc.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_bounce_purge_patch()
        {
            string data = @"{
  'enabled': true, 
  'hard_bounces': 5, 
  'soft_bounces': 5
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.bounce_purge.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_bounce_purge_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.bounce_purge.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_footer_patch()
        {
            string data = @"{
  'enabled': true, 
  'html_content': '...', 
  'plain_content': '...'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.footer.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_footer_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.footer.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_forward_bounce_patch()
        {
            string data = @"{
  'email': 'example@example.com', 
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.forward_bounce.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_forward_bounce_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.forward_bounce.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_forward_spam_patch()
        {
            string data = @"{
  'email': '', 
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.forward_spam.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_forward_spam_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.forward_spam.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_plain_content_patch()
        {
            string data = @"{
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.plain_content.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_plain_content_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.plain_content.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_spam_check_patch()
        {
            string data = @"{
  'enabled': true, 
  'max_score': 5, 
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.spam_check.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_spam_check_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.spam_check.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_template_patch()
        {
            string data = @"{
  'enabled': true, 
  'html_content': '<% body %>'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.template.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mail_settings_template_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mail_settings.template.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_mailbox_providers_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'mailbox_providers': 'test_string', 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.mailbox_providers.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_partner_settings_get()
        {
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.partner_settings.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_partner_settings_new_relic_patch()
        {
            string data = @"{
  'enable_subuser_statistics': true, 
  'enabled': true, 
  'license_key': ''
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.partner_settings.new_relic.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_partner_settings_new_relic_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.partner_settings.new_relic.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_scopes_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.scopes.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_senders_post()
        {
            string data = @"{
  'address': '123 Elm St.', 
  'address_2': 'Apt. 456', 
  'city': 'Denver', 
  'country': 'United States', 
  'from': {
    'email': 'from@example.com', 
    'name': 'Example INC'
  }, 
  'nickname': 'My Sender ID', 
  'reply_to': {
    'email': 'replyto@example.com', 
    'name': 'Example INC'
  }, 
  'state': 'Colorado', 
  'zip': '80202'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.senders.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_senders_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.senders.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_senders__sender_id__patch()
        {
            string data = @"{
  'address': '123 Elm St.', 
  'address_2': 'Apt. 456', 
  'city': 'Denver', 
  'country': 'United States', 
  'from': {
    'email': 'from@example.com', 
    'name': 'Example INC'
  }, 
  'nickname': 'My Sender ID', 
  'reply_to': {
    'email': 'replyto@example.com', 
    'name': 'Example INC'
  }, 
  'state': 'Colorado', 
  'zip': '80202'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.senders._(sender_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_senders__sender_id__get()
        {
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.senders._(sender_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_senders__sender_id__delete()
        {
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.senders._(sender_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_senders__sender_id__resend_verification_post()
        {
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.senders._(sender_id).resend_verification.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_post()
        {
            string data = @"{
  'email': 'John@example.com', 
  'ips': [
    '1.1.1.1', 
    '2.2.2.2'
  ], 
  'password': 'johns_password', 
  'username': 'John@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_get()
        {
            string queryParams = @"{
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_reputations_get()
        {
            string queryParams = @"{
  'usernames': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.reputations.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'start_date': '2016-01-01', 
  'subusers': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_stats_monthly_get()
        {
            string queryParams = @"{
  'date': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'subuser': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.stats.monthly.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers_stats_sums_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers.stats.sums.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers__subuser_name__patch()
        {
            string data = @"{
  'disabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.subusers._(subuser_name).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_subusers__subuser_name__delete()
        {
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.subusers._(subuser_name).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_subusers__subuser_name__ips_put()
        {
            string data = @"[
  '127.0.0.1'
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers._(subuser_name).ips.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers__subuser_name__monitor_put()
        {
            string data = @"{
  'email': 'example@example.com', 
  'frequency': 500
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers._(subuser_name).monitor.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers__subuser_name__monitor_post()
        {
            string data = @"{
  'email': 'example@example.com', 
  'frequency': 50000
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers._(subuser_name).monitor.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers__subuser_name__monitor_get()
        {
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers._(subuser_name).monitor.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_subusers__subuser_name__monitor_delete()
        {
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.subusers._(subuser_name).monitor.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_subusers__subuser_name__stats_monthly_get()
        {
            string queryParams = @"{
  'date': 'test_string', 
  'limit': 1, 
  'offset': 1, 
  'sort_by_direction': 'asc', 
  'sort_by_metric': 'test_string'
}";
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.subusers._(subuser_name).stats.monthly.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_blocks_get()
        {
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.blocks.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_blocks_delete()
        {
            string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.blocks.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_blocks__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.blocks._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_blocks__email__delete()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.blocks._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_bounces_get()
        {
            string queryParams = @"{
  'end_time': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.bounces.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_bounces_delete()
        {
            string data = @"{
  'delete_all': true, 
  'emails': [
    'example@example.com', 
    'example2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.bounces.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_bounces__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.bounces._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_bounces__email__delete()
        {
            string queryParams = @"{
  'email_address': 'example@example.com'
}";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.bounces._(email).delete(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_invalid_emails_get()
        {
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.invalid_emails.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_invalid_emails_delete()
        {
            string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.invalid_emails.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_invalid_emails__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.invalid_emails._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_invalid_emails__email__delete()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.invalid_emails._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_spam_report__email__get()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.spam_report._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_spam_report__email__delete()
        {
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.spam_report._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_spam_reports_get()
        {
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.spam_reports.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_suppression_spam_reports_delete()
        {
            string data = @"{
  'delete_all': false, 
  'emails': [
    'example1@example.com', 
    'example2@example.com'
  ]
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.suppression.spam_reports.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_suppression_unsubscribes_get()
        {
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.suppression.unsubscribes.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates_post()
        {
            string data = @"{
  'name': 'example_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.templates.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_templates_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates__template_id__patch()
        {
            string data = @"{
  'name': 'new_example_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates._(template_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates__template_id__get()
        {
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates._(template_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates__template_id__delete()
        {
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.templates._(template_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_templates__template_id__versions_post()
        {
            string data = @"{
  'active': 1, 
  'html_content': '<%body%>', 
  'name': 'example_version_name', 
  'plain_content': '<%body%>', 
  'subject': '<%subject%>', 
  'template_id': 'ddb96bbc-9b92-425e-8979-99464621b543'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.templates._(template_id).versions.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_templates__template_id__versions__version_id__patch()
        {
            string data = @"{
  'active': 1, 
  'html_content': '<%body%>', 
  'name': 'updated_example_name', 
  'plain_content': '<%body%>', 
  'subject': '<%subject%>'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates._(template_id).versions._(version_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates__template_id__versions__version_id__get()
        {
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates._(template_id).versions._(version_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_templates__template_id__versions__version_id__delete()
        {
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.templates._(template_id).versions._(version_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_templates__template_id__versions__version_id__activate_post()
        {
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.templates._(template_id).versions._(version_id).activate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_get()
        {
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_click_patch()
        {
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.click.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_click_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.click.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_google_analytics_patch()
        {
            string data = @"{
  'enabled': true, 
  'utm_campaign': 'website', 
  'utm_content': '', 
  'utm_medium': 'email', 
  'utm_source': 'sendgrid.com', 
  'utm_term': ''
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.google_analytics.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_google_analytics_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.google_analytics.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_open_patch()
        {
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.open.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_open_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.open.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_subscription_patch()
        {
            string data = @"{
  'enabled': true, 
  'html_content': 'html content', 
  'landing': 'landing page html', 
  'plain_content': 'text content', 
  'replace': 'replacement tag', 
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.subscription.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_tracking_settings_subscription_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.tracking_settings.subscription.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_account_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.account.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_credits_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.credits.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_email_put()
        {
            string data = @"{
  'email': 'example@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.email.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_email_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.email.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_password_put()
        {
            string data = @"{
  'new_password': 'new_password', 
  'old_password': 'old_password'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.password.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_profile_patch()
        {
            string data = @"{
  'city': 'Orange', 
  'first_name': 'Example', 
  'last_name': 'User'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.profile.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_profile_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.profile.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_scheduled_sends_post()
        {
            string data = @"{
  'batch_id': 'YOUR_BATCH_ID', 
  'status': 'pause'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.user.scheduled_sends.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_user_scheduled_sends_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.scheduled_sends.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_scheduled_sends__batch_id__patch()
        {
            string data = @"{
  'status': 'pause'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.user.scheduled_sends._(batch_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_user_scheduled_sends__batch_id__get()
        {
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.scheduled_sends._(batch_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_scheduled_sends__batch_id__delete()
        {
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.user.scheduled_sends._(batch_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_user_settings_enforced_tls_patch()
        {
            string data = @"{
  'require_tls': true, 
  'require_valid_cert': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.settings.enforced_tls.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_settings_enforced_tls_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.settings.enforced_tls.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_username_put()
        {
            string data = @"{
  'username': 'test_username'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.username.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_username_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.username.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_event_settings_patch()
        {
            string data = @"{
  'bounce': true, 
  'click': true, 
  'deferred': true, 
  'delivered': true, 
  'dropped': true, 
  'enabled': true, 
  'group_resubscribe': true, 
  'group_unsubscribe': true, 
  'open': true, 
  'processed': true, 
  'spam_report': true, 
  'unsubscribe': true, 
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks._("event").settings.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_event_settings_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks._("event").settings.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_event_test_post()
        {
            string data = @"{
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.user.webhooks._("event").test.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_user_webhooks_parse_settings_post()
        {
            string data = @"{
  'hostname': 'myhostname.com', 
  'send_raw': false, 
  'spam_check': true, 
  'url': 'http://email.myhosthame.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.user.webhooks.parse.settings.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_user_webhooks_parse_settings_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks.parse.settings.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_parse_settings__hostname__patch()
        {
            string data = @"{
  'send_raw': true, 
  'spam_check': false, 
  'url': 'http://newdomain.com/parse'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_parse_settings__hostname__get()
        {
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_user_webhooks_parse_settings__hostname__delete()
        {
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.user.webhooks.parse.settings._(hostname).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_user_webhooks_parse_stats_get()
        {
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'limit': 'test_string', 
  'offset': 'test_string', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.user.webhooks.parse.stats.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains_post()
        {
            string data = @"{
  'automatic_security': false, 
  'custom_spf': true, 
  'default': true, 
  'domain': 'example.com', 
  'ips': [
    '192.168.1.1', 
    '192.168.1.2'
  ], 
  'subdomain': 'news', 
  'username': 'john@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.whitelabel.domains.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_whitelabel_domains_get()
        {
            string queryParams = @"{
  'domain': 'test_string', 
  'exclude_subusers': 'true', 
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains_default_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._("default").get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains_subuser_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains.subuser.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains_subuser_delete()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.whitelabel.domains.subuser.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_whitelabel_domains__domain_id__patch()
        {
            string data = @"{
  'custom_spf': true, 
  'default': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._(domain_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains__domain_id__get()
        {
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._(domain_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains__domain_id__delete()
        {
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.whitelabel.domains._(domain_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_whitelabel_domains__domain_id__subuser_post()
        {
            string data = @"{
  'username': 'jane@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.whitelabel.domains._(domain_id).subuser.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_whitelabel_domains__id__ips_post()
        {
            string data = @"{
  'ip': '192.168.0.1'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._(id).ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains__id__ips__ip__delete()
        {
            var id = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._(id).ips._(ip).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_domains__id__validate_post()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.domains._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_ips_post()
        {
            string data = @"{
  'domain': 'example.com', 
  'ip': '192.168.1.1', 
  'subdomain': 'email'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.whitelabel.ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_whitelabel_ips_get()
        {
            string queryParams = @"{
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.ips.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_ips__id__get()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.ips._(id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_ips__id__delete()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.whitelabel.ips._(id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_whitelabel_ips__id__validate_post()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.ips._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links_post()
        {
            string data = @"{
  'default': true, 
  'domain': 'example.com', 
  'subdomain': 'mail'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            dynamic response = await sg.client.whitelabel.links.post(requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
        }

        [Test]
        public async void test_whitelabel_links_get()
        {
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links_default_get()
        {
            string queryParams = @"{
  'domain': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links._("default").get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links_subuser_get()
        {
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links.subuser.get(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links_subuser_delete()
        {
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.whitelabel.links.subuser.delete(queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_whitelabel_links__id__patch()
        {
            string data = @"{
  'default': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links._(id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links__id__get()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links._(id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links__id__delete()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            dynamic response = await sg.client.whitelabel.links._(id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.NoContent);
        }

        [Test]
        public async void test_whitelabel_links__id__validate_post()
        {
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [Test]
        public async void test_whitelabel_links__link_id__subuser_post()
        {
            string data = @"{
  'username': 'jane@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var link_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            dynamic response = await sg.client.whitelabel.links._(link_id).subuser.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            if (Environment.GetEnvironmentVariable("TRAVIS") != "true")
            {
                process.Kill();
                Trace.WriteLine("Sutting Down Prism");
            }
        }

    }
}
