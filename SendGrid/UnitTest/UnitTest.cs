using System;
using NUnit.Framework;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace UnitTest
{
    // Test the initialization of the Client
    [TestFixture]
    public class APIKeys
    {
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

        [Test]
        public void TestSendGridAPIClient()
        {
            Assert.AreEqual(sg.client.Host, "https://api.sendgrid.com");
            Assert.AreEqual(sg.client.Version, "v3");
            Assert.AreEqual(sg.client.RequestHeaders["Authorization"], "Bearer " + _apiKey);
            Assert.AreEqual(sg.client.RequestHeaders["Content-Type"], "application/json");
            Assert.AreEqual(sg.client.RequestHeaders["User-Agent"], "sendgrid/" + sg.Version + " csharp");
        }
    }

    // Test the building of the v3/mail/send request body
    [TestFixture]
    public class Mail
    {
        static string _apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public dynamic sg = new SendGrid.SendGridAPIClient(_apiKey);

        // Base case for sending an email
        [Test]
        public void TestHelloEmail()
        {
            SendGrid.Helpers.Mail.Mail mail = new SendGrid.Helpers.Mail.Mail();

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
            Assert.AreEqual(ret, "{\"from\":{\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}]}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"}]}");
        }

        // All paramaters available for sending an email
        [Test]
        public void TestKitchenSink()
        {
            SendGrid.Helpers.Mail.Mail mail = new SendGrid.Helpers.Mail.Mail();

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
            Assert.AreEqual(ret, "{\"from\":{\"name\":\"DX\",\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2\":\"Substitution for Section 2 Tag\"},\"categories\":[\"customer\",\"vip\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence\":\"2\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"},\"bypass_list_management\":{\"enable\":true},\"footer\":{\"enable\":true,\"text\":\"Some Footer Text\",\"html\":\"<bold>Some HTML Here</bold>\"},\"sandbox_mode\":{\"enable\":true},\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"https://gotchya.example.com\"}},\"tracking_settings\":{\"click_tracking\":{\"enable\":true},\"open_tracking\":{\"enable\":true,\"substitution_tag\":\"Optional tag to replace with the open image in the body of the message\"},\"subscription_tracking\":{\"enable\":true,\"text\":\"text to insert into the text/plain portion of the message\",\"html\":\"<bold>HTML to insert into the text/html portion of the message</bold>\",\"substitution_tag\":\"text to insert into the text/plain portion of the message\"},\"ganalytics\":{\"enable\":true,\"utm_source\":\"some source\",\"utm_medium\":\"some medium\",\"utm_term\":\"some term\",\"utm_content\":\"some content\",\"utm_campaign\":\"some campaign\"}},\"reply_to\":{\"email\":\"test@example.com\"}}");
        }
    }
}
