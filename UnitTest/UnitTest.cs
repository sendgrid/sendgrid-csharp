using System;
using NUnit.Framework;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;

namespace UnitTest
{

    [TestFixture]
    public class UnitTest
    {
        static string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        static string host = "http://localhost:4010";
        public SendGridClient sg = new SendGridClient(apiKey, host);
        Process process = new Process();

        [OneTimeSetUp]
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
            /*
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
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
            Assert.AreEqual(final, "{\"from\":{\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}]}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"}]}");
            */
        }

        // All paramaters available for sending an email
        [Test]
        public void TestKitchenSink()
        {
            /*
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
                                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, DefaultValueHandling = DefaultValueHandling.Ignore });
            Assert.AreEqual(final, "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"cc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"bcc\":[{\"name\":\"Example User\",\"email\":\"test@example.com\"},{\"name\":\"Example User\",\"email\":\"test@example.com\"}],\"subject\":\"Thank you for signing up, %name%\",\"headers\":{\"X-Test\":\"True\",\"X-Mock\":\"True\"},\"substitutions\":{\"%name%\":\"Example User\",\"%city%\":\"Denver\"},\"custom_args\":{\"marketing\":\"false\",\"transactional\":\"true\"},\"send_at\":1461775051}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"<html><body>HTML content</body></html>\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2\":\"Substitution for Section 2 Tag\"},\"categories\":[\"customer\",\"vip\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence\":\"2\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"},\"bypass_list_management\":{\"enable\":true},\"footer\":{\"enable\":true,\"text\":\"Some Footer Text\",\"html\":\"<bold>Some HTML Here</bold>\"},\"sandbox_mode\":{\"enable\":true},\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"https://gotchya.example.com\"}},\"tracking_settings\":{\"click_tracking\":{\"enable\":true,\"enable_text\":false},\"open_tracking\":{\"enable\":true,\"substitution_tag\":\"Optional tag to replace with the open image in the body of the message\"},\"subscription_tracking\":{\"enable\":true,\"text\":\"text to insert into the text/plain portion of the message\",\"html\":\"<bold>HTML to insert into the text/html portion of the message</bold>\",\"substitution_tag\":\"text to insert into the text/plain portion of the message\"},\"ganalytics\":{\"enable\":true,\"utm_source\":\"some source\",\"utm_medium\":\"some medium\",\"utm_term\":\"some term\",\"utm_content\":\"some content\",\"utm_campaign\":\"some campaign\"}},\"reply_to\":{\"email\":\"test@example.com\"}}");
            */
        }

        [Test]
        public void TestAddTo()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddTo(new EmailAddress("dx+test001@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test001@sendgrid.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var email = new EmailAddress("dx+test002@sendgrid.com", "DX Team");
            var personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddTo(new EmailAddress("dx+test003@sendgrid.com", "DX Team"), 0, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test002@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test003@sendgrid.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test004@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("dx+test005@sendgrid.com", "DX Team");
            personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddTo(new EmailAddress("dx+test006@sendgrid.com", "DX Team"), 1, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test004@sendgrid.com\"}]},{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test005@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test006@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test007@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            msg.AddTo(new EmailAddress("dx+test008@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test007@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test008@sendgrid.com\"}]}]}");


            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test009@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("dx+test010@sendgrid.com", "DX Team");
            personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddTo(new EmailAddress("dx+test011@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test009@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test011@sendgrid.com\"}]},{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test010@sendgrid.com\"}]}]}");
        }

        [Test]
        public void TestAddTos()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test012@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test013@sendgrid.com", "DX Team"));
            msg.AddTos(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test012@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test013@sendgrid.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test014@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test015@sendgrid.com", "DX Team"));
            var personalization = new Personalization()
            {
                Tos = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test016@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test017@sendgrid.com", "DX Team"));
            msg.AddTos(emails, 0, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test014@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test015@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test016@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test017@sendgrid.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test018@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test019@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test020@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test021@sendgrid.com", "DX Team"));
            personalization = new Personalization()
            {
                Tos = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test022@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test023@sendgrid.com", "DX Team"));
            msg.AddTos(emails, 1, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test018@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test019@sendgrid.com\"}]},{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test020@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test021@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test022@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test023@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test024@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test025@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test026@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test027@sendgrid.com", "DX Team"));
            msg.AddTos(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test024@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test025@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test026@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test027@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test028@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test029@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test030@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test031@sendgrid.com", "DX Team"));
            personalization = new Personalization()
            {
                Tos = emails
            };
            msg.Personalizations.Add(personalization);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test032@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test033@sendgrid.com", "DX Team"));
            msg.AddTos(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test028@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test029@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test032@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test033@sendgrid.com\"}]},{\"to\":[{\"name\":\"DX Team\",\"email\":\"dx+test030@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test031@sendgrid.com\"}]}]}");
        }

        [Test]
        public void TestAddCc()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddCc(new EmailAddress("dx+test001@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test001@sendgrid.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var email = new EmailAddress("dx+test002@sendgrid.com", "DX Team");
            var personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddCc(new EmailAddress("dx+test003@sendgrid.com", "DX Team"), 0, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test002@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test003@sendgrid.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test004@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("dx+test005@sendgrid.com", "DX Team");
            personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddCc(new EmailAddress("dx+test006@sendgrid.com", "DX Team"), 1, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test004@sendgrid.com\"}]},{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test005@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test006@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test007@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            msg.AddCc(new EmailAddress("dx+test008@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test007@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test008@sendgrid.com\"}]}]}");


            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            email = new EmailAddress("dx+test009@sendgrid.com", "DX Team");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("dx+test010@sendgrid.com", "DX Team");
            personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddCc(new EmailAddress("dx+test011@sendgrid.com", "DX Team"));
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test009@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test011@sendgrid.com\"}]},{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test010@sendgrid.com\"}]}]}");
        }

        [Test]
        public void TestAddCcs()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test012@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test013@sendgrid.com", "DX Team"));
            msg.AddCcs(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test012@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test013@sendgrid.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test014@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test015@sendgrid.com", "DX Team"));
            var personalization = new Personalization()
            {
                Ccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test016@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test017@sendgrid.com", "DX Team"));
            msg.AddCcs(emails, 0, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test014@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test015@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test016@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test017@sendgrid.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test018@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test019@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test020@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test021@sendgrid.com", "DX Team"));
            personalization = new Personalization()
            {
                Ccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test022@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test023@sendgrid.com", "DX Team"));
            msg.AddCcs(emails, 1, personalization);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test018@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test019@sendgrid.com\"}]},{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test020@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test021@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test022@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test023@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test024@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test025@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test026@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test027@sendgrid.com", "DX Team"));
            msg.AddCcs(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test024@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test025@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test026@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test027@sendgrid.com\"}]}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test028@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test029@sendgrid.com", "DX Team"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test030@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test031@sendgrid.com", "DX Team"));
            personalization = new Personalization()
            {
                Ccs = emails
            };
            msg.Personalizations.Add(personalization);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("dx+test032@sendgrid.com", "DX Team"));
            emails.Add(new EmailAddress("dx+test033@sendgrid.com", "DX Team"));
            msg.AddCcs(emails);
            Assert.AreEqual(msg.Serialize(), "{\"personalizations\":[{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test028@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test029@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test032@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test033@sendgrid.com\"}]},{\"cc\":[{\"name\":\"DX Team\",\"email\":\"dx+test030@sendgrid.com\"},{\"name\":\"DX Team\",\"email\":\"dx+test031@sendgrid.com\"}]}]}");
        }

        [Test]
        public async Task test_access_settings_activity_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/activity", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_access_settings_whitelist_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "access_settings/whitelist", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_access_settings_whitelist_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/whitelist", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_access_settings_whitelist_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "access_settings/whitelist", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_access_settings_whitelist__rule_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/whitelist/" + rule_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_access_settings_whitelist__rule_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "access_settings/whitelist/" + rule_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_alerts_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "alerts", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_alerts_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "alerts", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_alerts__alert_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'email_to': 'example@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "alerts/" + alert_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_alerts__alert_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "alerts/" + alert_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_alerts__alert_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "alerts/" + alert_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "api_keys", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "api_keys", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys__api_key_id__put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "api_keys/" + api_key_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys__api_key_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'A New Hope'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "api_keys/" + api_key_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys__api_key_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "api_keys/" + api_key_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_api_keys__api_key_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "api_keys/" + api_key_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'id': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "asm/groups/" + group_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__suppressions_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups/" + group_id + "/suppressions", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__suppressions_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id + "/suppressions", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__suppressions_search_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/groups/" + group_id + "/suppressions/search", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_groups__group_id__suppressions__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var group_id = "test_url_param";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id + "/suppressions/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_asm_suppressions_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_suppressions_global_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "asm/suppressions/global", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_asm_suppressions_global__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/global/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_asm_suppressions_global__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/suppressions/global/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_asm_suppressions__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_browsers_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "browsers/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "campaigns", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "campaigns/" + campaign_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns/" + campaign_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "campaigns/" + campaign_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'send_at': 1489451436
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "campaigns/" + campaign_id + "/schedules", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'send_at': 1489771528
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "campaigns/" + campaign_id + "/schedules", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns/" + campaign_id + "/schedules", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "campaigns/" + campaign_id + "/schedules", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_now_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "campaigns/" + campaign_id + "/schedules/now", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_campaigns__campaign_id__schedules_test_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'to': 'your.email@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "campaigns/" + campaign_id + "/schedules/test", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_categories_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'category': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_categories_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_categories_stats_sums_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories/stats/sums", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_clients_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "clients/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_clients__client_type__stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
            var client_type = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "clients/" + client_type + "/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_custom_fields_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'pet', 
  'type': 'text'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/custom_fields", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_custom_fields_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_custom_fields__custom_field_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields/" + custom_field_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_custom_fields__custom_field_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/custom_fields/" + custom_field_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'your list name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/lists/" + list_id, requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'list_id': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__recipients_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists/" + list_id + "/recipients", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__recipients_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'list_id': 1, 
  'page': 1, 
  'page_size': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists/" + list_id + "/recipients", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__recipients__recipient_id__post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var list_id = "test_url_param";
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists/" + list_id + "/recipients/" + recipient_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_lists__list_id__recipients__recipient_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'list_id': 1, 
  'recipient_id': 1
}";
            var list_id = "test_url_param";
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists/" + list_id + "/recipients/" + recipient_id, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/recipients", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/recipients", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"[
  'recipient_id1', 
  'recipient_id2'
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/recipients", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_billable_count_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/billable_count", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_count_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/count", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients_search_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  '{field_name}': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/search", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients__recipient_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients__recipient_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/recipients/" + recipient_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_recipients__recipient_id__lists_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id + "/lists", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_reserved_fields_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/reserved_fields", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/segments", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments__segment_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "contactdb/segments/" + segment_id, requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments__segment_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'segment_id': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments__segment_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_contactdb_segments__segment_id__recipients_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id + "/recipients", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_devices_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "devices/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_geo_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "geo/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_assigned_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/assigned", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'marketing'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/pools", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools__pool_name__put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'new_pool_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "ips/pools/" + pool_name, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools__pool_name__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools/" + pool_name, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools__pool_name__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools__pool_name__ips_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'ip': '0.0.0.0'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/pools/" + pool_name + "/ips", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_ips_pools__pool_name__ips__ip__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var pool_name = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name + "/ips/" + ip, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_ips_warmup_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'ip': '0.0.0.0'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/warmup", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_warmup_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_warmup__ip_address__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup/" + ip_address, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_ips_warmup__ip_address__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/warmup/" + ip_address, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_ips__ip_address__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/" + ip_address, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_batch_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "mail/batch", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_mail_batch__batch_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail/batch/" + batch_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_send_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "mail/send", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_address_whitelist_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/address_whitelist", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_address_whitelist_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/address_whitelist", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_bcc_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'email': 'email@example.com', 
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/bcc", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_bcc_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/bcc", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_bounce_purge_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/bounce_purge", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_bounce_purge_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/bounce_purge", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_footer_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/footer", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_footer_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/footer", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_forward_bounce_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'email': 'example@example.com', 
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/forward_bounce", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_forward_bounce_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/forward_bounce", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_forward_spam_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'email': '', 
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/forward_spam", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_forward_spam_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/forward_spam", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_plain_content_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/plain_content", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_plain_content_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/plain_content", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_spam_check_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/spam_check", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_spam_check_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/spam_check", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_template_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'enabled': true, 
  'html_content': '<% body %>'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/template", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mail_settings_template_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/template", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_mailbox_providers_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mailbox_providers/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_partner_settings_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "partner_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_partner_settings_new_relic_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "partner_settings/new_relic", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_partner_settings_new_relic_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "partner_settings/new_relic", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_scopes_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "scopes", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_senders_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "senders", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_senders_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "senders", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_senders__sender_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "senders/" + sender_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_senders__sender_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "senders/" + sender_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_senders__sender_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "senders/" + sender_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_senders__sender_id__resend_verification_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "senders/" + sender_id + "/resend_verification", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "subusers", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_reputations_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'usernames': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/reputations", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_stats_monthly_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats/monthly", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers_stats_sums_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/stats/sums", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'disabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "subusers/" + subuser_name, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__ips_put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"[
  '127.0.0.1'
]";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "subusers/" + subuser_name + "/ips", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__monitor_put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "subusers/" + subuser_name + "/monitor", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__monitor_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "subusers/" + subuser_name + "/monitor", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__monitor_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/" + subuser_name + "/monitor", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__monitor_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name + "/monitor", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_subusers__subuser_name__stats_monthly_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/" + subuser_name + "/stats/monthly", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_blocks_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/blocks", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_blocks_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/blocks", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_blocks__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/blocks/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_blocks__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/blocks/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_bounces_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_bounces_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/bounces", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_bounces__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_bounces__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'email_address': 'example@example.com'
}";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/bounces/" + email, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_invalid_emails_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/invalid_emails", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_invalid_emails_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/invalid_emails", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_invalid_emails__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/invalid_emails/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_invalid_emails__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/invalid_emails/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_spam_report__email__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/spam_report/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_spam_report__email__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/spam_report/" + email, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_spam_reports_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/spam_reports", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_spam_reports_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/spam_reports", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_suppression_unsubscribes_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'limit': 1, 
  'offset': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/unsubscribes", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'example_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "templates", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_templates_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'name': 'new_example_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "templates/" + template_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates/" + template_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "templates/" + template_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__versions_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "templates/" + template_id + "/versions", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__versions__version_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "templates/" + template_id + "/versions/" + version_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__versions__version_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates/" + template_id + "/versions/" + version_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__versions__version_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "templates/" + template_id + "/versions/" + version_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_templates__template_id__versions__version_id__activate_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "templates/" + template_id + "/versions/" + version_id + "/activate", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_click_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/click", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_click_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/click", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_google_analytics_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/google_analytics", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_google_analytics_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/google_analytics", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_open_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/open", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_open_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/open", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_subscription_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/subscription", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_tracking_settings_subscription_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/subscription", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_account_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/account", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_credits_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/credits", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_email_put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'email': 'example@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/email", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_email_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/email", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_password_put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'new_password': 'new_password', 
  'old_password': 'old_password'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/password", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_profile_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/profile", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_profile_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/profile", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_scheduled_sends_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'batch_id': 'YOUR_BATCH_ID', 
  'status': 'pause'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/scheduled_sends", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_user_scheduled_sends_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_scheduled_sends__batch_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'status': 'pause'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/scheduled_sends/" + batch_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_user_scheduled_sends__batch_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends/" + batch_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_scheduled_sends__batch_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/scheduled_sends/" + batch_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_user_settings_enforced_tls_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'require_tls': true, 
  'require_valid_cert': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/settings/enforced_tls", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_settings_enforced_tls_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/settings/enforced_tls", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_username_put()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'username': 'test_username'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/username", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_username_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/username", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_event_settings_patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/webhooks/event/settings", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_event_settings_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/event/settings", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_event_test_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/webhooks/event/test", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_settings_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/webhooks/parse/settings", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_settings_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_settings__hostname__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "user/webhooks/parse/settings/" + hostname, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_settings__hostname__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings/" + hostname, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_settings__hostname__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/webhooks/parse/settings/" + hostname, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_user_webhooks_parse_stats_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains_default_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/default", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains_subuser_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/subuser", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains_subuser_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/subuser", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__domain_id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "whitelabel/domains/" + domain_id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__domain_id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/" + domain_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__domain_id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + domain_id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__domain_id__subuser_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'username': 'jane@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + domain_id + "/subuser", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__id__ips_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'ip': '192.168.0.1'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + id + "/ips", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__id__ips__ip__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + id + "/ips/" + ip, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_domains__id__validate_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + id + "/validate", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_ips_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/ips", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_ips_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_ips__id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips/" + id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_ips__id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/ips/" + id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_ips__id__validate_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/ips/" + id + "/validate", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
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
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links", requestBody: data, queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links_default_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'domain': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/default", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links_subuser_get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/subuser", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links_subuser_delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/subuser", queryParams: queryParams, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links__id__patch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'default': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "whitelabel/links/" + id, requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links__id__get()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/" + id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links__id__delete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/" + id, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links__id__validate_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links/" + id + "/validate", requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public async Task test_whitelabel_links__link_id__subuser_post()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(apiKey, host);
            string data = @"{
  'username': 'jane@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            var link_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links/" + link_id + "/subuser", requestBody: data, requestHeaders: headers);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [OneTimeTearDown]
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
