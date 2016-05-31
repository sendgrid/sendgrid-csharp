using System;
using NUnit.Framework;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;

namespace UnitTest
{
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
            Assert.AreEqual(ret, "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2\":\"Substitution for Section 2 Tag\"},\"categories\":[\"customer\",\"vip\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence\":\"2\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"},\"bypass_list_management\":{\"enable\":true},\"footer\":{\"enable\":true,\"text\":\"Some Footer Text\",\"html\":\"<bold>Some HTML Here</bold>\"},\"sandbox_mode\":{\"enable\":true},\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"https://gotchya.example.com\"}},\"tracking_settings\":{\"click_tracking\":{\"enable\":true},\"open_tracking\":{\"enable\":true,\"substitution_tag\":\"Optional tag to replace with the open image in the body of the message\"},\"subscription_tracking\":{\"enable\":true,\"text\":\"text to insert into the text/plain portion of the message\",\"html\":\"<bold>HTML to insert into the text/html portion of the message</bold>\",\"substitution_tag\":\"text to insert into the text/plain portion of the message\"},\"ganalytics\":{\"enable\":true,\"utm_source\":\"some source\",\"utm_medium\":\"some medium\",\"utm_term\":\"some term\",\"utm_content\":\"some content\",\"utm_campaign\":\"some campaign\"}},\"reply_to\":{\"email\":\"test@example.com\"}}");
        }
    }

    [TestFixture]
    public class v3WebAPI
    {
        static string _apiKey = "SendGrid API Key";
        var host = "https://e9sk3d3bfaikbpdq7.stoplight-proxy.io";
        public dynamic sg = new SendGrid.SendGridAPIClient(_apiKey, host);
        [Test]
        public void test_access_settings_activity_get()
        {
            var params = @"{'limit': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.access_settings.activity.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_access_settings_whitelist_post()
        {
            var data = @"{u'ips': [{u'ip': u'192.168.1.1'}, {u'ip': u'192.*.*.*'}, {u'ip': u'192.168.1.3/32'}]}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.access_settings.whitelist.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_access_settings_whitelist_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.access_settings.whitelist.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_access_settings_whitelist_delete()
        {
            var data = @"{u'ids': [1, 2, 3]}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.access_settings.whitelist.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_access_settings_whitelist__rule_id__get()
        {
            rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.access_settings.whitelist._(rule_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_access_settings_whitelist__rule_id__delete()
        {
            rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.access_settings.whitelist._(rule_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_api_keys_post()
        {
            var data = @"{u'scopes': [u'mail.send', u'alerts.create', u'alerts.read'], u'name': u'My API Key'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.api_keys.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_api_keys_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.api_keys.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_api_keys__api_key_id__put()
        {
            var data = @"{u'scopes': [u'user.profile.read', u'user.profile.update'], u'name': u'A New Hope'}";
            api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.api_keys._(api_key_id).put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_api_keys__api_key_id__patch()
        {
            var data = @"{u'name': u'A New Hope'}";
            api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.api_keys._(api_key_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_api_keys__api_key_id__get()
        {
            api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.api_keys._(api_key_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_api_keys__api_key_id__delete()
        {
            api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.api_keys._(api_key_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_asm_groups_post()
        {
            var data = @"{u'is_default': False, u'description': u'A group description', u'name': u'A group name'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.asm.groups.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_asm_groups_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.asm.groups.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_asm_groups__group_id__patch()
        {
            var data = @"{u'description': u'Suggestions for items our users might like.', u'name': u'Item Suggestions', u'id': 103}";
            group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.asm.groups._(group_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_asm_groups__group_id__get()
        {
            group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.asm.groups._(group_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_asm_groups__group_id__delete()
        {
            group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.asm.groups._(group_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_asm_groups__group_id__suppressions_post()
        {
            var data = @"{u'recipient_emails': [u'test1@example.com', u'test2@example.com']}";
            group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.asm.groups._(group_id).suppressions.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_asm_groups__group_id__suppressions_get()
        {
            group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.asm.groups._(group_id).suppressions.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_asm_groups__group_id__suppressions__email__delete()
        {
            group_id = "test_url_param";
        email = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.asm.groups._(group_id).suppressions._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_asm_suppressions_global_post()
        {
            var data = @"{u'recipient_emails': [u'test1@example.com', u'test2@example.com']}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.asm.suppressions.global.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_asm_suppressions_global__email__get()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.asm.suppressions.global._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_asm_suppressions_global__email__delete()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.asm.suppressions.global._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_browsers_stats_get()
        {
            var params = @"{'end_date': '2016-04-01', 'aggregated_by': 'day', 'browsers': 'test_string', 'limit': 'test_string', 'offset': 'test_string', 'start_date': '2016-01-01'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.browsers.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns_post()
        {
            var data = @"{u'custom_unsubscribe_url': u'', u'html_content': u'<html><head><title></title></head><body><p>Check out our spring line!</p></body></html>', u'list_ids': [110, 124], u'sender_id': 124451, u'subject': u'New Products for Spring!', u'plain_content': u'Check out our spring line!', u'suppression_group_id': 42, u'title': u'March Newsletter', u'segment_ids': [110], u'categories': [u'spring line'], u'ip_pool': u'marketing'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.campaigns.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_campaigns_get()
        {
            var params = @"{'limit': 0, 'offset': 0}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.campaigns.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns__campaign_id__patch()
        {
            var data = @"{u'html_content': u'<html><head><title></title></head><body><p>Check out our summer line!</p></body></html>', u'subject': u'New Products for Summer!', u'title': u'May Newsletter', u'categories': [u'summer line'], u'plain_content': u'Check out our summer line!'}";
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.campaigns._(campaign_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns__campaign_id__get()
        {
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.campaigns._(campaign_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns__campaign_id__delete()
        {
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.campaigns._(campaign_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_patch()
        {
            var data = @"{u'send_at': 1489451436}";
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_post()
        {
            var data = @"{u'send_at': 1489771528}";
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_get()
        {
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_delete()
        {
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_now_post()
        {
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.now.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_campaigns__campaign_id__schedules_test_post()
        {
            var data = @"{u'to': u'your.email@example.com'}";
            campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.campaigns._(campaign_id).schedules.test.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_categories_get()
        {
            var params = @"{'category': 'test_string', 'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.categories.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_categories_stats_get()
        {
            var params = @"{'end_date': '2016-04-01', 'aggregated_by': 'day', 'limit': 1, 'offset': 1, 'start_date': '2016-01-01', 'categories': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.categories.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_categories_stats_sums_get()
        {
            var params = @"{'end_date': '2016-04-01', 'aggregated_by': 'day', 'limit': 1, 'sort_by_metric': 'test_string', 'offset': 1, 'start_date': '2016-01-01', 'sort_by_direction': 'asc'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.categories.stats.sums.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_clients_stats_get()
        {
            var params = @"{'aggregated_by': 'day', 'start_date': '2016-01-01', 'end_date': '2016-04-01'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.clients.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_clients__client_type__stats_get()
        {
            var params = @"{'aggregated_by': 'day', 'start_date': '2016-01-01', 'end_date': '2016-04-01'}";
            client_type = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.clients._(client_type).stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_custom_fields_post()
        {
            var data = @"{u'type': u'text', u'name': u'pet'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.custom_fields.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_custom_fields_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.custom_fields.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_custom_fields__custom_field_id__get()
        {
            custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.custom_fields._(custom_field_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_custom_fields__custom_field_id__delete()
        {
            custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 202);
            dynamic response = sg.client.contactdb.custom_fields._(custom_field_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 202);
        }

        [Test]
        public void test_contactdb_lists_post()
        {
            var data = @"{u'name': u'your list name'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.lists.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_lists_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.lists.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_lists_delete()
        {
            var data = @"[1, 2, 3, 4]";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.contactdb.lists.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_contactdb_lists__list_id__patch()
        {
            var data = @"{u'name': u'newlistname'}";
            var params = @"{'list_id': 0}";
            list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.lists._(list_id).patch(requestBody: data, queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_lists__list_id__get()
        {
            var params = @"{'list_id': 0}";
            list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.lists._(list_id).get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_lists__list_id__delete()
        {
            var params = @"{'delete_contacts': 'true'}";
            list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 202);
            dynamic response = sg.client.contactdb.lists._(list_id).delete(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 202);
        }

        [Test]
        public void test_contactdb_lists__list_id__recipients_post()
        {
            var data = @"[u'recipient_id1', u'recipient_id2']";
            list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.lists._(list_id).recipients.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_lists__list_id__recipients_get()
        {
            var params = @"{'page': 1, 'page_size': 1, 'list_id': 0}";
            list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.lists._(list_id).recipients.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_lists__list_id__recipients__recipient_id__post()
        {
            list_id = "test_url_param";
        recipient_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.lists._(list_id).recipients._(recipient_id).post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_lists__list_id__recipients__recipient_id__delete()
        {
            var params = @"{'recipient_id': 0, 'list_id': 0}";
            list_id = "test_url_param";
        recipient_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.contactdb.lists._(list_id).recipients._(recipient_id).delete(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_contactdb_recipients_patch()
        {
            var data = @"[{u'first_name': u'Guy', u'last_name': u'Jones', u'email': u'jones@example.com'}]";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.recipients.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_recipients_post()
        {
            var data = @"[{u'age': 25, u'last_name': u'User', u'email': u'example@example.com', u'first_name': u''}, {u'age': 25, u'last_name': u'User', u'email': u'example2@example.com', u'first_name': u'Example'}]";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.contactdb.recipients.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_contactdb_recipients_get()
        {
            var params = @"{'page': 1, 'page_size': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients_delete()
        {
            var data = @"[u'recipient_id1', u'recipient_id2']";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients_billable_count_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients.billable_count.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients_count_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients.count.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients_search_get()
        {
            var params = @"{'{field_name}': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients.search.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients__recipient_id__get()
        {
            recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients._(recipient_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_recipients__recipient_id__delete()
        {
            recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.contactdb.recipients._(recipient_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_contactdb_recipients__recipient_id__lists_get()
        {
            recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.recipients._(recipient_id).lists.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_reserved_fields_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.reserved_fields.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_segments_post()
        {
            var data = @"{u'conditions': [{u'operator': u'eq', u'field': u'last_name', u'and_or': u'', u'value': u'Miller'}, {u'operator': u'gt', u'field': u'last_clicked', u'and_or': u'and', u'value': u'01/02/2015'}, {u'operator': u'eq', u'field': u'clicks.campaign_identifier', u'and_or': u'or', u'value': u'513'}], u'name': u'Last Name Miller', u'list_id': 4}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.segments.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_segments_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.segments.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_segments__segment_id__patch()
        {
            var data = @"{u'conditions': [{u'operator': u'eq', u'field': u'last_name', u'and_or': u'', u'value': u'Miller'}], u'name': u'The Millers', u'list_id': 5}";
            var params = @"{'segment_id': 'test_string'}";
            segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.segments._(segment_id).patch(requestBody: data, queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_segments__segment_id__get()
        {
            var params = @"{'segment_id': 0}";
            segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.segments._(segment_id).get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_contactdb_segments__segment_id__delete()
        {
            var params = @"{'delete_contacts': 'true'}";
            segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.contactdb.segments._(segment_id).delete(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_contactdb_segments__segment_id__recipients_get()
        {
            var params = @"{'page': 1, 'page_size': 1}";
            segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.contactdb.segments._(segment_id).recipients.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_devices_stats_get()
        {
            var params = @"{'aggregated_by': 'day', 'limit': 1, 'start_date': '2016-01-01', 'end_date': '2016-04-01', 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.devices.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_geo_stats_get()
        {
            var params = @"{'end_date': '2016-04-01', 'country': 'US', 'aggregated_by': 'day', 'limit': 1, 'offset': 1, 'start_date': '2016-01-01'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.geo.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_get()
        {
            var params = @"{'subuser': 'test_string', 'ip': 'test_string', 'limit': 1, 'exclude_whitelabels': 'true', 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_assigned_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.assigned.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_pools_post()
        {
            var data = @"{u'name': u'marketing'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.pools.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_pools_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.pools.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_pools__pool_name__put()
        {
            var data = @"{u'name': u'new_pool_name'}";
            pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.pools._(pool_name).put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_pools__pool_name__get()
        {
            pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.pools._(pool_name).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_pools__pool_name__delete()
        {
            pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.ips.pools._(pool_name).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_ips_pools__pool_name__ips_post()
        {
            var data = @"{u'ip': u'0.0.0.0'}";
            pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.ips.pools._(pool_name).ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_ips_pools__pool_name__ips__ip__delete()
        {
            pool_name = "test_url_param";
        ip = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.ips.pools._(pool_name).ips._(ip).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_ips_warmup_post()
        {
            var data = @"{u'ip': u'0.0.0.0'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.warmup.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_warmup_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.warmup.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_warmup__ip_address__get()
        {
            ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips.warmup._(ip_address).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_ips_warmup__ip_address__delete()
        {
            ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.ips.warmup._(ip_address).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_ips__ip_address__get()
        {
            ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.ips._(ip_address).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_batch_post()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.mail.batch.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_mail_batch__batch_id__get()
        {
            batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail.batch._(batch_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_send_beta_post()
        {
            var data = @"{u'custom_args': {u'New Argument 1': u'New Value 1', u'activationAttempt': u'1', u'customerAccountNumber': u'[CUSTOMER ACCOUNT NUMBER GOES HERE]'}, u'from': {u'email': u'sam.smith@example.com', u'name': u'Sam Smith'}, u'attachments': [{u'name': u'file1', u'filename': u'file1.jpg', u'content': u'[BASE64 encoded content block here]', u'disposition': u'inline', u'content_id': u'ii_139db99fdb5c3704', u'type': u'jpg'}], u'personalizations': [{u'to': [{u'email': u'john.doe@example.com', u'name': u'John Doe'}], u'cc': [{u'email': u'jane.doe@example.com', u'name': u'Jane Doe'}], u'bcc': [{u'email': u'sam.doe@example.com', u'name': u'Sam Doe'}], u'custom_args': {u'New Argument 1': u'New Value 1', u'activationAttempt': u'1', u'customerAccountNumber': u'[CUSTOMER ACCOUNT NUMBER GOES HERE]'}, u'headers': {u'X-Accept-Language': u'en', u'X-Mailer': u'MyApp'}, u'send_at': 1409348513, u'substitutions': {u'sub': {u'%name%': [u'John', u'Jane', u'Sam']}}, u'subject': u'Hello, World!'}], u'subject': u'Hello, World!', u'ip_pool_name': u'[YOUR POOL NAME GOES HERE]', u'content': [{u'type': u'text/html', u'value': u"<html><p>Hello, world!</p><img src='cid:ii_139db99fdb5c3704'></img></html>"}], u'headers': {}, u'asm': {u'groups_to_display': [1, 2, 3], u'group_id': 1}, u'batch_id': u'[YOUR BATCH ID GOES HERE]', u'tracking_settings': {u'subscription_tracking': {u'text': u'If you would like to unsubscribe and stop receiveing these emails <% click here %>.', u'enable': True, u'html': u'If you would like to unsubscribe and stop receiving these emails <% clickhere %>.', u'substitution_tag': u'<%click here%>'}, u'open_tracking': {u'enable': True, u'substitution_tag': u'%opentrack'}, u'click_tracking': {u'enable': True, u'enable_text': True}, u'ganalytics': {u'utm_campaign': u'[NAME OF YOUR REFERRER SOURCE]', u'enable': True, u'utm_name': u'[NAME OF YOUR CAMPAIGN]', u'utm_term': u'[IDENTIFY PAID KEYWORDS HERE]', u'utm_content': u'[USE THIS SPACE TO DIFFERENTIATE YOUR EMAIL FROM ADS]', u'utm_medium': u'[NAME OF YOUR MARKETING MEDIUM e.g. email]'}}, u'mail_settings': {u'footer': {u'text': u'Thanks,/n The SendGrid Team', u'enable': True, u'html': u'<p>Thanks</br>The SendGrid Team</p>'}, u'spam_check': {u'threshold': 3, u'post_to_url': u'http://example.com/compliance', u'enable': True}, u'bypass_list_management': {u'enable': True}, u'sandbox_mode': {u'enable': False}, u'bcc': {u'enable': True, u'email': u'ben.doe@example.com'}}, u'reply_to': {u'email': u'sam.smith@example.com', u'name': u'Sam Smith'}, u'sections': {u'section': {u':sectionName2': u'section 2 text', u':sectionName1': u'section 1 text'}}, u'template_id': u'[YOUR TEMPLATE ID GOES HERE]', u'categories': [u'category1', u'category2'], u'send_at': 1409348513}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 202);
            dynamic response = sg.client.mail.send.beta.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 202);
        }

        [Test]
        public void test_mail_settings_get()
        {
            var params = @"{'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_address_whitelist_patch()
        {
            var data = @"{u'list': [u'email1@example.com', u'example.com'], u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.address_whitelist.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_address_whitelist_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.address_whitelist.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_bcc_patch()
        {
            var data = @"{u'enabled': False, u'email': u'email@example.com'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.bcc.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_bcc_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.bcc.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_bounce_purge_patch()
        {
            var data = @"{u'hard_bounces': 5, u'soft_bounces': 5, u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.bounce_purge.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_bounce_purge_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.bounce_purge.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_footer_patch()
        {
            var data = @"{u'html_content': u'...', u'enabled': True, u'plain_content': u'...'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.footer.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_footer_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.footer.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_forward_bounce_patch()
        {
            var data = @"{u'enabled': True, u'email': u'example@example.com'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.forward_bounce.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_forward_bounce_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.forward_bounce.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_forward_spam_patch()
        {
            var data = @"{u'enabled': False, u'email': u''}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.forward_spam.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_forward_spam_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.forward_spam.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_plain_content_patch()
        {
            var data = @"{u'enabled': False}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.plain_content.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_plain_content_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.plain_content.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_spam_check_patch()
        {
            var data = @"{u'url': u'url', u'max_score': 5, u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.spam_check.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_spam_check_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.spam_check.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_template_patch()
        {
            var data = @"{u'html_content': u'<% body %>', u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.template.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mail_settings_template_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mail_settings.template.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_mailbox_providers_stats_get()
        {
            var params = @"{'end_date': '2016-04-01', 'mailbox_providers': 'test_string', 'aggregated_by': 'day', 'limit': 1, 'offset': 1, 'start_date': '2016-01-01'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.mailbox_providers.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_partner_settings_get()
        {
            var params = @"{'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.partner_settings.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_partner_settings_new_relic_patch()
        {
            var data = @"{u'enable_subuser_statistics': True, u'enabled': True, u'license_key': u''}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.partner_settings.new_relic.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_partner_settings_new_relic_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.partner_settings.new_relic.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_scopes_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.scopes.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_stats_get()
        {
            var params = @"{'aggregated_by': 'day', 'limit': 1, 'start_date': '2016-01-01', 'end_date': '2016-04-01', 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_post()
        {
            var data = @"{u'username': u'John@example.com', u'ips': [u'1.1.1.1', u'2.2.2.2'], u'password': u'johns_password', u'email': u'John@example.com'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_get()
        {
            var params = @"{'username': 'test_string', 'limit': 0, 'offset': 0}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_reputations_get()
        {
            var params = @"{'usernames': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.reputations.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_stats_get()
        {
            var params = @"{'end_date': '2016-04-01', 'aggregated_by': 'day', 'limit': 1, 'offset': 1, 'start_date': '2016-01-01', 'subusers': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_stats_monthly_get()
        {
            var params = @"{'subuser': 'test_string', 'limit': 1, 'sort_by_metric': 'test_string', 'offset': 1, 'date': 'test_string', 'sort_by_direction': 'asc'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.stats.monthly.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers_stats_sums_get()
        {
            var params = @"{'end_date': '2016-04-01', 'aggregated_by': 'day', 'limit': 1, 'sort_by_metric': 'test_string', 'offset': 1, 'start_date': '2016-01-01', 'sort_by_direction': 'asc'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers.stats.sums.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers__subuser_name__patch()
        {
            var data = @"{u'disabled': False}";
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.subusers._(subuser_name).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_subusers__subuser_name__delete()
        {
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.subusers._(subuser_name).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_subusers__subuser_name__ips_put()
        {
            var data = @"[u'127.0.0.1']";
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers._(subuser_name).ips.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers__subuser_name__monitor_put()
        {
            var data = @"{u'frequency': 500, u'email': u'example@example.com'}";
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers._(subuser_name).monitor.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers__subuser_name__monitor_post()
        {
            var data = @"{u'frequency': 50000, u'email': u'example@example.com'}";
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers._(subuser_name).monitor.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers__subuser_name__monitor_get()
        {
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers._(subuser_name).monitor.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_subusers__subuser_name__monitor_delete()
        {
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.subusers._(subuser_name).monitor.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_subusers__subuser_name__stats_monthly_get()
        {
            var params = @"{'date': 'test_string', 'sort_by_direction': 'asc', 'limit': 0, 'sort_by_metric': 'test_string', 'offset': 1}";
            subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.subusers._(subuser_name).stats.monthly.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_blocks_get()
        {
            var params = @"{'start_time': 1, 'limit': 1, 'end_time': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.blocks.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_blocks_delete()
        {
            var data = @"{u'emails': [u'example1@example.com', u'example2@example.com'], u'delete_all': False}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.blocks.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_blocks__email__get()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.blocks._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_blocks__email__delete()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.blocks._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_bounces_get()
        {
            var params = @"{'start_time': 0, 'end_time': 0}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.bounces.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_bounces_delete()
        {
            var data = @"{u'emails': [u'example@example.com', u'example2@example.com'], u'delete_all': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.bounces.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_bounces__email__get()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.bounces._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_bounces__email__delete()
        {
            var params = @"{'email_address': 'example@example.com'}";
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.bounces._(email).delete(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_invalid_emails_get()
        {
            var params = @"{'start_time': 1, 'limit': 1, 'end_time': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.invalid_emails.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_invalid_emails_delete()
        {
            var data = @"{u'emails': [u'example1@example.com', u'example2@example.com'], u'delete_all': False}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.invalid_emails.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_invalid_emails__email__get()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.invalid_emails._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_invalid_emails__email__delete()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.invalid_emails._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_spam_report__email__get()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.spam_report._(email).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_spam_report__email__delete()
        {
            email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.spam_report._(email).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_spam_reports_get()
        {
            var params = @"{'start_time': 1, 'limit': 1, 'end_time': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.spam_reports.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_suppression_spam_reports_delete()
        {
            var data = @"{u'emails': [u'example1@example.com', u'example2@example.com'], u'delete_all': False}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.suppression.spam_reports.delete(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_suppression_unsubscribes_get()
        {
            var params = @"{'start_time': 1, 'limit': 1, 'end_time': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.suppression.unsubscribes.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates_post()
        {
            var data = @"{u'name': u'example_name'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.templates.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_templates_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates__template_id__patch()
        {
            var data = @"{u'name': u'new_example_name'}";
            template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates._(template_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates__template_id__get()
        {
            template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates._(template_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates__template_id__delete()
        {
            template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.templates._(template_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_templates__template_id__versions_post()
        {
            var data = @"{u'name': u'example_version_name', u'html_content': u'<%body%>', u'plain_content': u'<%body%>', u'active': 1, u'template_id': u'ddb96bbc-9b92-425e-8979-99464621b543', u'subject': u'<%subject%>'}";
            template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.templates._(template_id).versions.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_templates__template_id__versions__version_id__patch()
        {
            var data = @"{u'active': 1, u'html_content': u'<%body%>', u'subject': u'<%subject%>', u'name': u'updated_example_name', u'plain_content': u'<%body%>'}";
            template_id = "test_url_param";
        version_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates._(template_id).versions._(version_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates__template_id__versions__version_id__get()
        {
            template_id = "test_url_param";
        version_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates._(template_id).versions._(version_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_templates__template_id__versions__version_id__delete()
        {
            template_id = "test_url_param";
        version_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.templates._(template_id).versions._(version_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_templates__template_id__versions__version_id__activate_post()
        {
            template_id = "test_url_param";
        version_id = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.templates._(template_id).versions._(version_id).activate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_get()
        {
            var params = @"{'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_click_patch()
        {
            var data = @"{u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.click.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_click_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.click.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_google_analytics_patch()
        {
            var data = @"{u'utm_campaign': u'website', u'utm_term': u'', u'utm_content': u'', u'enabled': True, u'utm_source': u'sendgrid.com', u'utm_medium': u'email'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.google_analytics.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_google_analytics_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.google_analytics.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_open_patch()
        {
            var data = @"{u'enabled': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.open.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_open_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.open.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_subscription_patch()
        {
            var data = @"{u'url': u'url', u'html_content': u'html content', u'enabled': True, u'landing': u'landing page html', u'replace': u'replacement tag', u'plain_content': u'text content'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.subscription.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_tracking_settings_subscription_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.tracking_settings.subscription.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_account_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.account.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_credits_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.credits.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_email_put()
        {
            var data = @"{u'email': u'example@example.com'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.email.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_email_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.email.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_password_put()
        {
            var data = @"{u'new_password': u'new_password', u'old_password': u'old_password'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.password.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_profile_patch()
        {
            var data = @"{u'city': u'Orange', u'first_name': u'Example', u'last_name': u'User'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.profile.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_profile_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.profile.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_scheduled_sends_post()
        {
            var data = @"{u'batch_id': u'YOUR_BATCH_ID', u'status': u'pause'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.user.scheduled_sends.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_user_scheduled_sends_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.scheduled_sends.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_scheduled_sends__batch_id__patch()
        {
            var data = @"{u'status': u'pause'}";
            batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.user.scheduled_sends._(batch_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_user_scheduled_sends__batch_id__get()
        {
            batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.scheduled_sends._(batch_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_scheduled_sends__batch_id__delete()
        {
            batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.user.scheduled_sends._(batch_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_user_settings_enforced_tls_patch()
        {
            var data = @"{u'require_tls': True, u'require_valid_cert': False}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.settings.enforced_tls.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_settings_enforced_tls_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.settings.enforced_tls.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_username_put()
        {
            var data = @"{u'username': u'test_username'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.username.put(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_username_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.username.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_webhooks_event_settings_patch()
        {
            var data = @"{u'group_resubscribe': True, u'delivered': True, u'group_unsubscribe': True, u'spam_report': True, u'url': u'url', u'enabled': True, u'bounce': True, u'deferred': True, u'unsubscribe': True, u'dropped': True, u'open': True, u'click': True, u'processed': True}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.webhooks.event.settings.patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_webhooks_event_settings_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.webhooks.event.settings.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_webhooks_event_test_post()
        {
            var data = @"{u'url': u'url'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.user.webhooks.event.test.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_user_webhooks_parse_settings_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.webhooks.parse.settings.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_user_webhooks_parse_stats_get()
        {
            var params = @"{'aggregated_by': 'day', 'limit': 'test_string', 'start_date': '2016-01-01', 'end_date': '2016-04-01', 'offset': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.user.webhooks.parse.stats.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains_post()
        {
            var data = @"{u'automatic_security': False, u'username': u'john@example.com', u'domain': u'example.com', u'default': True, u'custom_spf': True, u'ips': [u'192.168.1.1', u'192.168.1.2'], u'subdomain': u'news'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.whitelabel.domains.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_whitelabel_domains_get()
        {
            var params = @"{'username': 'test_string', 'domain': 'test_string', 'exclude_subusers': 'true', 'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains_default_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains.default.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains_subuser_get()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains.subuser.get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains_subuser_delete()
        {
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.whitelabel.domains.subuser.delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_whitelabel_domains__domain_id__patch()
        {
            var data = @"{u'default': False, u'custom_spf': True}";
            domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains._(domain_id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains__domain_id__get()
        {
            domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains._(domain_id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains__domain_id__delete()
        {
            domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.whitelabel.domains._(domain_id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_whitelabel_domains__domain_id__subuser_post()
        {
            var data = @"{u'username': u'jane@example.com'}";
            domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.whitelabel.domains._(domain_id).subuser.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_whitelabel_domains__id__ips_post()
        {
            var data = @"{u'ip': u'192.168.0.1'}";
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains._(id).ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains__id__ips__ip__delete()
        {
            id = "test_url_param";
        ip = "test_url_param"
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains._(id).ips._(ip).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_domains__id__validate_post()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.domains._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_ips_post()
        {
            var data = @"{u'ip': u'192.168.1.1', u'domain': u'example.com', u'subdomain': u'email'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.whitelabel.ips.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_whitelabel_ips_get()
        {
            var params = @"{'ip': 'test_string', 'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.ips.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_ips__id__get()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.ips._(id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_ips__id__delete()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.whitelabel.ips._(id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_whitelabel_ips__id__validate_post()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.ips._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links_post()
        {
            var data = @"{u'default': True, u'domain': u'example.com', u'subdomain': u'mail'}";
            var params = @"{'limit': 1, 'offset': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 201);
            dynamic response = sg.client.whitelabel.links.post(requestBody: data, queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 201);
        }

        [Test]
        public void test_whitelabel_links_get()
        {
            var params = @"{'limit': 1}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links_default_get()
        {
            var params = @"{'domain': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links.default.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links_subuser_get()
        {
            var params = @"{'username': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links.subuser.get(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links_subuser_delete()
        {
            var params = @"{'username': 'test_string'}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.whitelabel.links.subuser.delete(queryParams: params, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_whitelabel_links__id__patch()
        {
            var data = @"{u'default': True}";
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links._(id).patch(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links__id__get()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links._(id).get(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links__id__delete()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 204);
            dynamic response = sg.client.whitelabel.links._(id).delete(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 204);
        }

        [Test]
        public void test_whitelabel_links__id__validate_post()
        {
            id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links._(id).validate.post(requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

        [Test]
        public void test_whitelabel_links__link_id__subuser_post()
        {
            var data = @"{u'username': u'jane@example.com'}";
            link_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", 200);
            dynamic response = sg.client.whitelabel.links._(link_id).subuser.post(requestBody: data, requestHeaders: headers);
            Assert.AreEqual(response.StatusCode, 200);
        }

    }
}
