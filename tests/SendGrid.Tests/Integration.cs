namespace SendGrid.Tests
{
    using Helpers.Mail;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;
    using Xunit;

    public class IntegrationFixture : IDisposable
    {
        public IntegrationFixture()
        {

            if (Environment.GetEnvironmentVariable("TRAVIS") != "true")
            {
                Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
                Trace.WriteLine("Starting Prism (~20 seconds)");
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
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

        public void Dispose()
        {
            if (Environment.GetEnvironmentVariable("TRAVIS") != "true")
            {
                process.Kill();
                Trace.WriteLine("Shutting Down Prism");
            }
        }

        public string apiKey = Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
        public string host = "http://localhost:4010";
        public Process process = new Process();
    }

    public class Integration : IClassFixture<IntegrationFixture>
    {
        IntegrationFixture fixture;

        public Integration(IntegrationFixture fixture)
        {
            this.fixture = fixture;
        }

        // Base case for sending a single email
        [Fact]
        public void TestSendSingleEmailWithHelper()
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com"));
            msg.AddTo(new EmailAddress("test@example.com"));
            msg.SetSubject("Hello World from the SendGrid CSharp Library");
            msg.AddContent(MimeType.Text, "Textual content");
            msg.AddContent(MimeType.Html, "HTML content");
            Assert.True(msg.Serialize() == "{\"from\":{\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}],\"subject\":\"Hello World from the SendGrid CSharp Library\"}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"HTML content\"}]}");

            msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test@example.com"));
            msg.AddTo(new EmailAddress("test@example.com"));
            msg.SetSubject("Hello World from the SendGrid CSharp Library");
            msg.AddContent(MimeType.Html, "HTML content");
            Console.WriteLine(msg.Serialize());
        }

        // All paramaters available for sending an email
        [Fact]
        public void TestKitchenSink()
        {
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress("test1@example.com", "Example User1"));
            msg.SetGlobalSubject("Hello World from the SendGrid CSharp Library");
            msg.AddTo(new EmailAddress("test2@example.com", "Example User2"));
            msg.AddTo(new EmailAddress("test3@example.com", "Example User3"));
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test4@example.com", "Example User4"));
            emails.Add(new EmailAddress("test5@example.com", "Example User5"));
            msg.AddTos(emails);
            msg.AddCc(new EmailAddress("test6@example.com", "Example User6"));
            msg.AddCc(new EmailAddress("test7@example.com", "Example User7"));
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test8@example.com", "Example User8"));
            emails.Add(new EmailAddress("test9@example.com", "Example User9"));
            msg.AddCcs(emails);
            msg.AddBcc(new EmailAddress("test10example.com", "Example User10"));
            msg.AddBcc(new EmailAddress("test11@example.com", "Example User11"));
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test12@example.com", "Example User12"));
            emails.Add(new EmailAddress("test13@example.com", "Example User13"));
            msg.AddBccs(emails);
            msg.SetSubject("Thank you for signing up, % name %");
            msg.AddHeader("X-Test1", "True1");
            msg.AddHeader("X-Test2", "Test2");
            var headers = new Dictionary<string, string>()
            {
                { "X-Test3", "True3" },
                { "X-Test4", "True4" }
            };
            msg.AddHeaders(headers);
            msg.AddSubstitution("%name1%", "Example User1");
            msg.AddSubstitution("%city2%", "Denver1");
            var substitutions = new Dictionary<string, string>()
            {
                { "%name3%", "Example User2" },
                { "%city4%", "Orange1" }
            };
            msg.AddSubstitutions(substitutions);
            msg.AddCustomArg("marketing1", "false");
            msg.AddCustomArg("transactional1", "true");
            var customArgs = new Dictionary<string, string>()
            {
                { "marketing2", "true" },
                { "transactional2", "false" }
            };
            msg.AddCustomArgs(customArgs);
            msg.SetSendAt(1461775051);

            msg.AddTo(new EmailAddress("test14@example.com", "Example User14"), 1);
            msg.AddTo(new EmailAddress("test15@example.com", "Example User15"), 1);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test16@example.com", "Example User16"));
            emails.Add(new EmailAddress("test17@example.com", "Example User17"));
            msg.AddTos(emails, 1);
            msg.AddCc(new EmailAddress("test18@example.com", "Example User18"), 1);
            msg.AddCc(new EmailAddress("test19@example.com", "Example User19"), 1);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test20@example.com", "Example User20"));
            emails.Add(new EmailAddress("test21@example.com", "Example User21"));
            msg.AddCcs(emails, 1);
            msg.AddBcc(new EmailAddress("test22example.com", "Example User22"), 1);
            msg.AddBcc(new EmailAddress("test23@example.com", "Example User23"), 1);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test24@example.com", "Example User24"));
            emails.Add(new EmailAddress("test25@example.com", "Example User25"));
            msg.AddBccs(emails, 1);
            msg.SetSubject("Thank you for signing up, % name % 2", 1);
            msg.AddHeader("X-Test5", "True5", 1);
            msg.AddHeader("X-Test6", "Test6", 1);
            headers = new Dictionary<string, string>()
            {
                { "X-Test7", "True7" },
                { "X-Test8", "True8" }
            };
            msg.AddHeaders(headers, 1);
            msg.AddSubstitution("%name5%", "Example User5", 1);
            msg.AddSubstitution("%city6%", "Denver6", 1);
            substitutions = new Dictionary<string, string>()
            {
                { "%name7%", "Example User7" },
                { "%city8%", "Orange8" }
            };
            msg.AddSubstitutions(substitutions, 1);
            msg.AddCustomArg("marketing3", "false", 1);
            msg.AddCustomArg("transactional3", "true", 1);
            customArgs = new Dictionary<string, string>()
            {
                { "marketing4", "true" },
                { "transactional4", "false" }
            };
            msg.AddCustomArgs(customArgs, 1);
            msg.SetSendAt(1461775052, 1);

            msg.AddTo(new EmailAddress("test26@example.com", "Example User26"), 2);
            msg.AddTo(new EmailAddress("test27@example.com", "Example User27"), 2);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test28@example.com", "Example User28"));
            emails.Add(new EmailAddress("test29@example.com", "Example User29"));
            msg.AddTos(emails, 2);
            msg.AddCc(new EmailAddress("test30@example.com", "Example User30"), 2);
            msg.AddCc(new EmailAddress("test31@example.com", "Example User31"), 2);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test32@example.com", "Example User32"));
            emails.Add(new EmailAddress("test33@example.com", "Example User33"));
            msg.AddCcs(emails, 2);
            msg.AddBcc(new EmailAddress("test34example.com", "Example User34"), 2);
            msg.AddBcc(new EmailAddress("test35@example.com", "Example User35"), 2);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test36@example.com", "Example User36"));
            emails.Add(new EmailAddress("test37@example.com", "Example User37"));
            msg.AddBccs(emails, 2);
            msg.SetSubject("Thank you for signing up, % name % 3", 2);
            msg.AddHeader("X-Test7", "True7", 2);
            msg.AddHeader("X-Test8", "Test8", 2);
            headers = new Dictionary<string, string>()
            {
                { "X-Test9", "True9" },
                { "X-Test10", "True10" }
            };
            msg.AddHeaders(headers, 2);
            msg.AddSubstitution("%name9%", "Example User9", 2);
            msg.AddSubstitution("%city10%", "Denver10", 2);
            substitutions = new Dictionary<string, string>()
            {
                { "%name11%", "Example User11" },
                { "%city12%", "Orange12" }
            };
            msg.AddSubstitutions(substitutions, 2);
            msg.AddCustomArg("marketing5", "false", 2);
            msg.AddCustomArg("transactional5", "true", 2);
            customArgs = new Dictionary<string, string>()
            {
                { "marketing6", "true" },
                { "transactional6", "false" }
            };
            msg.AddCustomArgs(customArgs, 2);
            msg.SetSendAt(1461775053, 2);

            var contents = new List<Content>();
            var content = new Content()
            {
                Type = "text/calendar",
                Value = "Party Time!!"
            };
            contents.Add(content);
            content = new Content()
            {
                Type = "text/calendar2",
                Value = "Party Time2!!"
            };
            contents.Add(content);
            msg.AddContents(contents);
            msg.AddContent(MimeType.Html, "HTML content");
            msg.AddContent(MimeType.Text, "Textual content");

            msg.AddAttachment("balance_001.pdf",
                              "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12",
                              "application/pdf",
                              "attachment",
                              "Balance Sheet");
            var attachments = new List<Attachment>();
            var attachment = new Attachment()
            {
                Content = "BwdW",
                Type = "image/png",
                Filename = "banner.png",
                Disposition = "inline",
                ContentId = "Banner"
            };
            attachments.Add(attachment);
            attachment = new Attachment()
            {
                Content = "BwdW2",
                Type = "image/png",
                Filename = "banner2.png",
                Disposition = "inline",
                ContentId = "Banner 2"
            };
            attachments.Add(attachment);
            msg.AddAttachments(attachments);
            msg.SetTemplateId("13b8f94f-bcae-4ec6-b752-70d6cb59f932");
            msg.AddGlobalHeader("X-Day", "Monday");
            var globalHeaders = new Dictionary<string, string>();
            globalHeaders.Add("X-Month", "January");
            globalHeaders.Add("X-Year", "2017");
            msg.AddGlobalHeaders(globalHeaders);
            msg.AddSection("%section1", "Substitution for Section 1 Tag");
            var sections = new Dictionary<string, string>();
            sections.Add("%section2%", "Substitution for Section 2 Tag");
            sections.Add("%section3%", "Substitution for Section 3 Tag");
            msg.AddSections(sections);
            msg.AddCategory("customer");
            var categories = new List<string>();
            categories.Add("vip");
            categories.Add("new_account");
            msg.AddCategories(categories);
            msg.AddGlobalCustomArg("campaign", "welcome");
            var globalCustomArgs = new Dictionary<string, string>();
            globalCustomArgs.Add("sequence2", "2");
            globalCustomArgs.Add("sequence3", "3");
            msg.AddGlobalCustomArgs(globalCustomArgs);
            msg.SetAsm(3, new List<int>() { 1, 4, 5 });
            msg.SetGlobalSendAt(1461775051);
            msg.SetIpPoolName("23");
            // This must be a valid [batch ID](https://sendgrid.com/docs/API_Reference/SMTP_API/scheduling_parameters.html)
            msg.SetBatchId("some_batch_id");
            msg.SetBccSetting(true, "test@example.com");
            msg.SetBypassListManagement(true);
            msg.SetFooterSetting(true, "Some Footer HTML", "Some Footer Text");
            msg.SetSandBoxMode(true);
            msg.SetSpamCheck(true, 1, "https://gotchya.example.com");
            msg.SetClickTracking(true, false);
            msg.SetOpenTracking(true, "Optional tag to replace with the open image in the body of the message");
            msg.SetSubscriptionTracking(true,
                                       "HTML to insert into the text / html portion of the message",
                                       "text to insert into the text/plain portion of the message",
                                       "substitution tag");
            msg.SetGoogleAnalytics(true,
                                   "some campaign",
                                   "some content",
                                   "some medium",
                                   "some source",
                                   "some term");
            msg.SetReplyTo(new EmailAddress("test+reply@example.com", "Reply To Me"));
            Assert.True(msg.Serialize() == "{\"from\":{\"name\":\"Example User1\",\"email\":\"test1@example.com\"},\"subject\":\"Hello World from the SendGrid CSharp Library\",\"personalizations\":[{\"to\":[{\"name\":\"Example User2\",\"email\":\"test2@example.com\"},{\"name\":\"Example User3\",\"email\":\"test3@example.com\"},{\"name\":\"Example User4\",\"email\":\"test4@example.com\"},{\"name\":\"Example User5\",\"email\":\"test5@example.com\"}],\"cc\":[{\"name\":\"Example User6\",\"email\":\"test6@example.com\"},{\"name\":\"Example User7\",\"email\":\"test7@example.com\"},{\"name\":\"Example User8\",\"email\":\"test8@example.com\"},{\"name\":\"Example User9\",\"email\":\"test9@example.com\"}],\"bcc\":[{\"name\":\"Example User10\",\"email\":\"test10example.com\"},{\"name\":\"Example User11\",\"email\":\"test11@example.com\"},{\"name\":\"Example User12\",\"email\":\"test12@example.com\"},{\"name\":\"Example User13\",\"email\":\"test13@example.com\"}],\"subject\":\"Thank you for signing up, % name %\",\"headers\":{\"X-Test1\":\"True1\",\"X-Test2\":\"Test2\",\"X-Test3\":\"True3\",\"X-Test4\":\"True4\"},\"substitutions\":{\"%name1%\":\"Example User1\",\"%city2%\":\"Denver1\",\"%name3%\":\"Example User2\",\"%city4%\":\"Orange1\"},\"custom_args\":{\"marketing1\":\"false\",\"transactional1\":\"true\",\"marketing2\":\"true\",\"transactional2\":\"false\"},\"send_at\":1461775051},{\"to\":[{\"name\":\"Example User14\",\"email\":\"test14@example.com\"},{\"name\":\"Example User15\",\"email\":\"test15@example.com\"},{\"name\":\"Example User16\",\"email\":\"test16@example.com\"},{\"name\":\"Example User17\",\"email\":\"test17@example.com\"}],\"cc\":[{\"name\":\"Example User18\",\"email\":\"test18@example.com\"},{\"name\":\"Example User19\",\"email\":\"test19@example.com\"},{\"name\":\"Example User20\",\"email\":\"test20@example.com\"},{\"name\":\"Example User21\",\"email\":\"test21@example.com\"}],\"bcc\":[{\"name\":\"Example User22\",\"email\":\"test22example.com\"},{\"name\":\"Example User23\",\"email\":\"test23@example.com\"},{\"name\":\"Example User24\",\"email\":\"test24@example.com\"},{\"name\":\"Example User25\",\"email\":\"test25@example.com\"}],\"subject\":\"Thank you for signing up, % name % 2\",\"headers\":{\"X-Test5\":\"True5\",\"X-Test6\":\"Test6\",\"X-Test7\":\"True7\",\"X-Test8\":\"True8\"},\"substitutions\":{\"%name5%\":\"Example User5\",\"%city6%\":\"Denver6\",\"%name7%\":\"Example User7\",\"%city8%\":\"Orange8\"},\"custom_args\":{\"marketing3\":\"false\",\"transactional3\":\"true\",\"marketing4\":\"true\",\"transactional4\":\"false\"},\"send_at\":1461775052},{\"to\":[{\"name\":\"Example User26\",\"email\":\"test26@example.com\"},{\"name\":\"Example User27\",\"email\":\"test27@example.com\"},{\"name\":\"Example User28\",\"email\":\"test28@example.com\"},{\"name\":\"Example User29\",\"email\":\"test29@example.com\"}],\"cc\":[{\"name\":\"Example User30\",\"email\":\"test30@example.com\"},{\"name\":\"Example User31\",\"email\":\"test31@example.com\"},{\"name\":\"Example User32\",\"email\":\"test32@example.com\"},{\"name\":\"Example User33\",\"email\":\"test33@example.com\"}],\"bcc\":[{\"name\":\"Example User34\",\"email\":\"test34example.com\"},{\"name\":\"Example User35\",\"email\":\"test35@example.com\"},{\"name\":\"Example User36\",\"email\":\"test36@example.com\"},{\"name\":\"Example User37\",\"email\":\"test37@example.com\"}],\"subject\":\"Thank you for signing up, % name % 3\",\"headers\":{\"X-Test7\":\"True7\",\"X-Test8\":\"Test8\",\"X-Test9\":\"True9\",\"X-Test10\":\"True10\"},\"substitutions\":{\"%name9%\":\"Example User9\",\"%city10%\":\"Denver10\",\"%name11%\":\"Example User11\",\"%city12%\":\"Orange12\"},\"custom_args\":{\"marketing5\":\"false\",\"transactional5\":\"true\",\"marketing6\":\"true\",\"transactional6\":\"false\"},\"send_at\":1461775053}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Textual content\"},{\"type\":\"text/html\",\"value\":\"HTML content\"},{\"type\":\"text/calendar\",\"value\":\"Party Time!!\"},{\"type\":\"text/calendar2\",\"value\":\"Party Time2!!\"}],\"attachments\":[{\"content\":\"TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBwdW12\",\"type\":\"application/pdf\",\"filename\":\"balance_001.pdf\",\"disposition\":\"attachment\",\"content_id\":\"Balance Sheet\"},{\"content\":\"BwdW\",\"type\":\"image/png\",\"filename\":\"banner.png\",\"disposition\":\"inline\",\"content_id\":\"Banner\"},{\"content\":\"BwdW2\",\"type\":\"image/png\",\"filename\":\"banner2.png\",\"disposition\":\"inline\",\"content_id\":\"Banner 2\"}],\"template_id\":\"13b8f94f-bcae-4ec6-b752-70d6cb59f932\",\"headers\":{\"X-Day\":\"Monday\",\"X-Month\":\"January\",\"X-Year\":\"2017\"},\"sections\":{\"%section1\":\"Substitution for Section 1 Tag\",\"%section2%\":\"Substitution for Section 2 Tag\",\"%section3%\":\"Substitution for Section 3 Tag\"},\"categories\":[\"customer\",\"vip\",\"new_account\"],\"custom_args\":{\"campaign\":\"welcome\",\"sequence2\":\"2\",\"sequence3\":\"3\"},\"send_at\":1461775051,\"asm\":{\"group_id\":3,\"groups_to_display\":[1,4,5]},\"batch_id\":\"some_batch_id\",\"ip_pool_name\":\"23\",\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"},\"bypass_list_management\":{\"enable\":true},\"footer\":{\"enable\":true,\"text\":\"Some Footer Text\",\"html\":\"Some Footer HTML\"},\"sandbox_mode\":{\"enable\":true},\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"https://gotchya.example.com\"}},\"tracking_settings\":{\"click_tracking\":{\"enable\":true,\"enable_text\":false},\"open_tracking\":{\"enable\":true,\"substitution_tag\":\"Optional tag to replace with the open image in the body of the message\"},\"subscription_tracking\":{\"enable\":true,\"text\":\"text to insert into the text/plain portion of the message\",\"html\":\"HTML to insert into the text / html portion of the message\",\"substitution_tag\":\"substitution tag\"},\"ganalytics\":{\"enable\":true,\"utm_source\":\"some source\",\"utm_medium\":\"some medium\",\"utm_term\":\"some term\",\"utm_content\":\"some content\",\"utm_campaign\":\"some campaign\"}},\"reply_to\":{\"name\":\"Reply To Me\",\"email\":\"test+reply@example.com\"}}");
        }

        [Fact]
        public void TestCreateSingleEmail()
        {
            var msg = MailHelper.CreateSingleEmail(new EmailAddress("test@example.com", "Example User"),
                                                   new EmailAddress("test@example.com"),
                                                   "Test Subject",
                                                   "Plain Text Content",
                                                   "HTML Content");
            Assert.True(msg.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}],\"subject\":\"Test Subject\"}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Plain Text Content\"},{\"type\":\"text/html\",\"value\":\"HTML Content\"}]}");

            var msg2 = MailHelper.CreateSingleEmail(new EmailAddress("test@example.com", "Example User"),
                                               new EmailAddress("test@example.com"),
                                               "Test Subject",
                                               null,
                                               "HTML Content");
            Assert.True(msg2.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}],\"subject\":\"Test Subject\"}],\"content\":[{\"type\":\"text/html\",\"value\":\"HTML Content\"}]}");

            var msg3 = MailHelper.CreateSingleEmail(new EmailAddress("test@example.com", "Example User"),
                                                   new EmailAddress("test@example.com"),
                                                   "Test Subject",
                                                   "Plain Text Content",
                                                   null);
            Assert.True(msg3.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test@example.com\"}],\"subject\":\"Test Subject\"}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Plain Text Content\"}]}");
        }

        [Fact]
        public void TestCreateSingleEmailToMultipleRecipients()
        {
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test1@example.com"));
            emails.Add(new EmailAddress("test2@example.com"));
            emails.Add(new EmailAddress("test3@example.com"));
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                       emails,
                                                                       "Test Subject",
                                                                       "Plain Text Content",
                                                                       "HTML Content"
                                                                       );
            Assert.True(msg.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Test Subject\",\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}]},{\"to\":[{\"email\":\"test2@example.com\"}]},{\"to\":[{\"email\":\"test3@example.com\"}]}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Plain Text Content\"},{\"type\":\"text/html\",\"value\":\"HTML Content\"}]}");

            var msg2 = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                        emails,
                                                                        "Test Subject",
                                                                        null,
                                                                        "HTML Content"
                                                                        );
            Assert.True(msg2.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Test Subject\",\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}]},{\"to\":[{\"email\":\"test2@example.com\"}]},{\"to\":[{\"email\":\"test3@example.com\"}]}],\"content\":[{\"type\":\"text/html\",\"value\":\"HTML Content\"}]}");

            var msg3 = MailHelper.CreateSingleEmailToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                       emails,
                                                                       "Test Subject",
                                                                       "Plain Text Content",
                                                                       null
                                                                       );
            Assert.True(msg3.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"subject\":\"Test Subject\",\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}]},{\"to\":[{\"email\":\"test2@example.com\"}]},{\"to\":[{\"email\":\"test3@example.com\"}]}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Plain Text Content\"}]}");

        }

        [Fact]
        public void TestCreateMultipleEmailsToMultipleRecipients()
        {
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test1@example.com"));
            emails.Add(new EmailAddress("test2@example.com"));
            emails.Add(new EmailAddress("test3@example.com"));
            var subjects = new List<string>();
            subjects.Add("Test Subject1");
            subjects.Add("Test Subject2");
            subjects.Add("Test Subject3");
            var plainTextContent = "Hello -name-";
            var htmlContent = "Goodbye -name-";
            var substitutions = new List<Dictionary<string, string>>();
            substitutions.Add(new Dictionary<string, string>() { { "-name-", "Name1" } });
            substitutions.Add(new Dictionary<string, string>() { { "-name-", "Name1" } });
            substitutions.Add(new Dictionary<string, string>() { { "-name-", "Name1" } });
            var msg = MailHelper.CreateMultipleEmailsToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                          emails,
                                                                          subjects,
                                                                          plainTextContent,
                                                                          htmlContent,
                                                                          substitutions
                                                                          );
            Assert.True(msg.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}],\"subject\":\"Test Subject1\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test2@example.com\"}],\"subject\":\"Test Subject2\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test3@example.com\"}],\"subject\":\"Test Subject3\",\"substitutions\":{\"-name-\":\"Name1\"}}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Hello -name-\"},{\"type\":\"text/html\",\"value\":\"Goodbye -name-\"}]}");

            plainTextContent = null;
            htmlContent = "Goodbye -name-";
            var msg2 = MailHelper.CreateMultipleEmailsToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                          emails,
                                                                          subjects,
                                                                          plainTextContent,
                                                                          htmlContent,
                                                                          substitutions
                                                                          );
            Assert.True(msg2.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}],\"subject\":\"Test Subject1\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test2@example.com\"}],\"subject\":\"Test Subject2\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test3@example.com\"}],\"subject\":\"Test Subject3\",\"substitutions\":{\"-name-\":\"Name1\"}}],\"content\":[{\"type\":\"text/html\",\"value\":\"Goodbye -name-\"}]}");

            plainTextContent = "Hello -name-";
            htmlContent = null;
            var msg3 = MailHelper.CreateMultipleEmailsToMultipleRecipients(new EmailAddress("test@example.com", "Example User"),
                                                                          emails,
                                                                          subjects,
                                                                          plainTextContent,
                                                                          htmlContent,
                                                                          substitutions
                                                                          );
            Assert.True(msg3.Serialize() == "{\"from\":{\"name\":\"Example User\",\"email\":\"test@example.com\"},\"personalizations\":[{\"to\":[{\"email\":\"test1@example.com\"}],\"subject\":\"Test Subject1\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test2@example.com\"}],\"subject\":\"Test Subject2\",\"substitutions\":{\"-name-\":\"Name1\"}},{\"to\":[{\"email\":\"test3@example.com\"}],\"subject\":\"Test Subject3\",\"substitutions\":{\"-name-\":\"Name1\"}}],\"content\":[{\"type\":\"text/plain\",\"value\":\"Hello -name-\"}]}");
        }

        [Fact]
        public void TestAddTo()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddTo(new EmailAddress("test001@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test001@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var email = new EmailAddress("test002@example.com", "Example User");
            var personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddTo(new EmailAddress("test003@example.com", "Example User"), 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test002@example.com\"},{\"name\":\"Example User\",\"email\":\"test003@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test004@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test005@example.com", "Example User");
            personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddTo(new EmailAddress("test006@example.com", "Example User"), 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test004@example.com\"}]},{\"to\":[{\"name\":\"Example User\",\"email\":\"test005@example.com\"},{\"name\":\"Example User\",\"email\":\"test006@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test007@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            msg.AddTo(new EmailAddress("test008@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test007@example.com\"},{\"name\":\"Example User\",\"email\":\"test008@example.com\"}]}]}");


            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            email = new EmailAddress("test009@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test010@example.com", "Example User");
            personalization = new Personalization()
            {
                Tos = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddTo(new EmailAddress("test011@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test009@example.com\"},{\"name\":\"Example User\",\"email\":\"test011@example.com\"}]},{\"to\":[{\"name\":\"Example User\",\"email\":\"test010@example.com\"}]}]}");
        }

        [Fact]
        public void TestAddTos()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test012@example.com", "Example User"));
            emails.Add(new EmailAddress("test013@example.com", "Example User"));
            msg.AddTos(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test012@example.com\"},{\"name\":\"Example User\",\"email\":\"test013@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test014@example.com", "Example User"));
            emails.Add(new EmailAddress("test015@example.com", "Example User"));
            var personalization = new Personalization()
            {
                Tos = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test016@example.com", "Example User"));
            emails.Add(new EmailAddress("test017@example.com", "Example User"));
            msg.AddTos(emails, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test014@example.com\"},{\"name\":\"Example User\",\"email\":\"test015@example.com\"},{\"name\":\"Example User\",\"email\":\"test016@example.com\"},{\"name\":\"Example User\",\"email\":\"test017@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test018@example.com", "Example User"));
            emails.Add(new EmailAddress("test019@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test020@example.com", "Example User"));
            emails.Add(new EmailAddress("test021@example.com", "Example User"));
            personalization = new Personalization()
            {
                Tos = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test022@example.com", "Example User"));
            emails.Add(new EmailAddress("test023@example.com", "Example User"));
            msg.AddTos(emails, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test018@example.com\"},{\"name\":\"Example User\",\"email\":\"test019@example.com\"}]},{\"to\":[{\"name\":\"Example User\",\"email\":\"test020@example.com\"},{\"name\":\"Example User\",\"email\":\"test021@example.com\"},{\"name\":\"Example User\",\"email\":\"test022@example.com\"},{\"name\":\"Example User\",\"email\":\"test023@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test024@example.com", "Example User"));
            emails.Add(new EmailAddress("test025@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test026@example.com", "Example User"));
            emails.Add(new EmailAddress("test027@example.com", "Example User"));
            msg.AddTos(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test024@example.com\"},{\"name\":\"Example User\",\"email\":\"test025@example.com\"},{\"name\":\"Example User\",\"email\":\"test026@example.com\"},{\"name\":\"Example User\",\"email\":\"test027@example.com\"}]}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test028@example.com", "Example User"));
            emails.Add(new EmailAddress("test029@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Tos = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test030@example.com", "Example User"));
            emails.Add(new EmailAddress("test031@example.com", "Example User"));
            personalization = new Personalization()
            {
                Tos = emails
            };
            msg.Personalizations.Add(personalization);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test032@example.com", "Example User"));
            emails.Add(new EmailAddress("test033@example.com", "Example User"));
            msg.AddTos(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"to\":[{\"name\":\"Example User\",\"email\":\"test028@example.com\"},{\"name\":\"Example User\",\"email\":\"test029@example.com\"},{\"name\":\"Example User\",\"email\":\"test032@example.com\"},{\"name\":\"Example User\",\"email\":\"test033@example.com\"}]},{\"to\":[{\"name\":\"Example User\",\"email\":\"test030@example.com\"},{\"name\":\"Example User\",\"email\":\"test031@example.com\"}]}]}");
        }

        [Fact]
        public void TestAddCc()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddCc(new EmailAddress("test001@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test001@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var email = new EmailAddress("test002@example.com", "Example User");
            var personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddCc(new EmailAddress("test003@example.com", "Example User"), 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test002@example.com\"},{\"name\":\"Example User\",\"email\":\"test003@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test004@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test005@example.com", "Example User");
            personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddCc(new EmailAddress("test006@example.com", "Example User"), 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test004@example.com\"}]},{\"cc\":[{\"name\":\"Example User\",\"email\":\"test005@example.com\"},{\"name\":\"Example User\",\"email\":\"test006@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test007@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            msg.AddCc(new EmailAddress("test008@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test007@example.com\"},{\"name\":\"Example User\",\"email\":\"test008@example.com\"}]}]}");


            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            email = new EmailAddress("test009@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test010@example.com", "Example User");
            personalization = new Personalization()
            {
                Ccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddCc(new EmailAddress("test011@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test009@example.com\"},{\"name\":\"Example User\",\"email\":\"test011@example.com\"}]},{\"cc\":[{\"name\":\"Example User\",\"email\":\"test010@example.com\"}]}]}");
        }

        [Fact]
        public void TestAddCcs()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test012@example.com", "Example User"));
            emails.Add(new EmailAddress("test013@example.com", "Example User"));
            msg.AddCcs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test012@example.com\"},{\"name\":\"Example User\",\"email\":\"test013@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test014@example.com", "Example User"));
            emails.Add(new EmailAddress("test015@example.com", "Example User"));
            var personalization = new Personalization()
            {
                Ccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test016@example.com", "Example User"));
            emails.Add(new EmailAddress("test017@example.com", "Example User"));
            msg.AddCcs(emails, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test014@example.com\"},{\"name\":\"Example User\",\"email\":\"test015@example.com\"},{\"name\":\"Example User\",\"email\":\"test016@example.com\"},{\"name\":\"Example User\",\"email\":\"test017@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test018@example.com", "Example User"));
            emails.Add(new EmailAddress("test019@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test020@example.com", "Example User"));
            emails.Add(new EmailAddress("test021@example.com", "Example User"));
            personalization = new Personalization()
            {
                Ccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test022@example.com", "Example User"));
            emails.Add(new EmailAddress("test023@example.com", "Example User"));
            msg.AddCcs(emails, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test018@example.com\"},{\"name\":\"Example User\",\"email\":\"test019@example.com\"}]},{\"cc\":[{\"name\":\"Example User\",\"email\":\"test020@example.com\"},{\"name\":\"Example User\",\"email\":\"test021@example.com\"},{\"name\":\"Example User\",\"email\":\"test022@example.com\"},{\"name\":\"Example User\",\"email\":\"test023@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test024@example.com", "Example User"));
            emails.Add(new EmailAddress("test025@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test026@example.com", "Example User"));
            emails.Add(new EmailAddress("test027@example.com", "Example User"));
            msg.AddCcs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test024@example.com\"},{\"name\":\"Example User\",\"email\":\"test025@example.com\"},{\"name\":\"Example User\",\"email\":\"test026@example.com\"},{\"name\":\"Example User\",\"email\":\"test027@example.com\"}]}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test028@example.com", "Example User"));
            emails.Add(new EmailAddress("test029@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Ccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test030@example.com", "Example User"));
            emails.Add(new EmailAddress("test031@example.com", "Example User"));
            personalization = new Personalization()
            {
                Ccs = emails
            };
            msg.Personalizations.Add(personalization);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test032@example.com", "Example User"));
            emails.Add(new EmailAddress("test033@example.com", "Example User"));
            msg.AddCcs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"cc\":[{\"name\":\"Example User\",\"email\":\"test028@example.com\"},{\"name\":\"Example User\",\"email\":\"test029@example.com\"},{\"name\":\"Example User\",\"email\":\"test032@example.com\"},{\"name\":\"Example User\",\"email\":\"test033@example.com\"}]},{\"cc\":[{\"name\":\"Example User\",\"email\":\"test030@example.com\"},{\"name\":\"Example User\",\"email\":\"test031@example.com\"}]}]}");
        }

        [Fact]
        public void TestAddBcc()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddBcc(new EmailAddress("test001@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test001@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var email = new EmailAddress("test002@example.com", "Example User");
            var personalization = new Personalization()
            {
                Bccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddBcc(new EmailAddress("test003@example.com", "Example User"), 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test002@example.com\"},{\"name\":\"Example User\",\"email\":\"test003@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test004@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test005@example.com", "Example User");
            personalization = new Personalization()
            {
                Bccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.AddBcc(new EmailAddress("test006@example.com", "Example User"), 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test004@example.com\"}]},{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test005@example.com\"},{\"name\":\"Example User\",\"email\":\"test006@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            email = new EmailAddress("test007@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            msg.AddBcc(new EmailAddress("test008@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test007@example.com\"},{\"name\":\"Example User\",\"email\":\"test008@example.com\"}]}]}");


            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            email = new EmailAddress("test009@example.com", "Example User");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = new List<EmailAddress>()
                    {
                        email
                    }
                }
            };
            email = new EmailAddress("test010@example.com", "Example User");
            personalization = new Personalization()
            {
                Bccs = new List<EmailAddress>()
                {
                    email
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddBcc(new EmailAddress("test011@example.com", "Example User"));
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test009@example.com\"},{\"name\":\"Example User\",\"email\":\"test011@example.com\"}]},{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test010@example.com\"}]}]}");
        }

        [Fact]
        public void TestAddBccs()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test012@example.com", "Example User"));
            emails.Add(new EmailAddress("test013@example.com", "Example User"));
            msg.AddBccs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test012@example.com\"},{\"name\":\"Example User\",\"email\":\"test013@example.com\"}]}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test014@example.com", "Example User"));
            emails.Add(new EmailAddress("test015@example.com", "Example User"));
            var personalization = new Personalization()
            {
                Bccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test016@example.com", "Example User"));
            emails.Add(new EmailAddress("test017@example.com", "Example User"));
            msg.AddBccs(emails, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test014@example.com\"},{\"name\":\"Example User\",\"email\":\"test015@example.com\"},{\"name\":\"Example User\",\"email\":\"test016@example.com\"},{\"name\":\"Example User\",\"email\":\"test017@example.com\"}]}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test018@example.com", "Example User"));
            emails.Add(new EmailAddress("test019@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test020@example.com", "Example User"));
            emails.Add(new EmailAddress("test021@example.com", "Example User"));
            personalization = new Personalization()
            {
                Bccs = emails
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test022@example.com", "Example User"));
            emails.Add(new EmailAddress("test023@example.com", "Example User"));
            msg.AddBccs(emails, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test018@example.com\"},{\"name\":\"Example User\",\"email\":\"test019@example.com\"}]},{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test020@example.com\"},{\"name\":\"Example User\",\"email\":\"test021@example.com\"},{\"name\":\"Example User\",\"email\":\"test022@example.com\"},{\"name\":\"Example User\",\"email\":\"test023@example.com\"}]}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test024@example.com", "Example User"));
            emails.Add(new EmailAddress("test025@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test026@example.com", "Example User"));
            emails.Add(new EmailAddress("test027@example.com", "Example User"));
            msg.AddBccs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test024@example.com\"},{\"name\":\"Example User\",\"email\":\"test025@example.com\"},{\"name\":\"Example User\",\"email\":\"test026@example.com\"},{\"name\":\"Example User\",\"email\":\"test027@example.com\"}]}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test028@example.com", "Example User"));
            emails.Add(new EmailAddress("test029@example.com", "Example User"));
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Bccs = emails
                }
            };
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test030@example.com", "Example User"));
            emails.Add(new EmailAddress("test031@example.com", "Example User"));
            personalization = new Personalization()
            {
                Bccs = emails
            };
            msg.Personalizations.Add(personalization);
            emails = new List<EmailAddress>();
            emails.Add(new EmailAddress("test032@example.com", "Example User"));
            emails.Add(new EmailAddress("test033@example.com", "Example User"));
            msg.AddBccs(emails);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test028@example.com\"},{\"name\":\"Example User\",\"email\":\"test029@example.com\"},{\"name\":\"Example User\",\"email\":\"test032@example.com\"},{\"name\":\"Example User\",\"email\":\"test033@example.com\"}]},{\"bcc\":[{\"name\":\"Example User\",\"email\":\"test030@example.com\"},{\"name\":\"Example User\",\"email\":\"test031@example.com\"}]}]}");
        }

        [Fact]
        public void TestSetSubject()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.SetSubject("subject1");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"subject\":\"subject1\"}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var subject = "subject2";
            var personalization = new Personalization()
            {
                Subject = subject
            };
            msg.SetSubject("subject3", 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"subject\":\"subject3\"}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            subject = "subject4";
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Subject = subject
                }
            };
            subject = "subject5";
            personalization = new Personalization()
            {
                Subject = subject
            };
            msg.SetSubject("subject6", 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"subject\":\"subject4\"},{\"subject\":\"subject6\"}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            subject = "subject7";
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Subject = subject
                }
            };
            msg.SetSubject("subject8");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"subject\":\"subject8\"}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            subject = "subject9";
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Subject = subject
                }
            };
            subject = "subject10";
            personalization = new Personalization()
            {
                Subject = subject
            };
            msg.Personalizations.Add(personalization);
            msg.SetSubject("subject11");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"subject\":\"subject11\"},{\"subject\":\"subject10\"}]}");
        }

        [Fact]
        public void TestAddHeader()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddHeader("X-Test", "Test Value");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test\":\"Test Value\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var personalization = new Personalization()
            {
                Headers = new Dictionary<string, string>()
                {
                    { "X-Test", "Test Value" }
                }
            };
            msg.AddHeader("X-Test2", "Test Value 2", 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test\":\"Test Value\",\"X-Test2\":\"Test Value 2\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = new Dictionary<string, string>()
                    {
                        { "X-Test3", "Test Value 3" }
                    }
                }
            };
            personalization = new Personalization()
            {
                Headers = new Dictionary<string, string>()
                {
                    { "X-Test4", "Test Value 4" }
                }
            };
            msg.AddHeader("X-Test5", "Test Value 5", 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test3\":\"Test Value 3\"}},{\"headers\":{\"X-Test4\":\"Test Value 4\",\"X-Test5\":\"Test Value 5\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = new Dictionary<string, string>()
                    {
                        { "X-Test6", "Test Value 6" }
                    }
                }
            };
            msg.AddHeader("X-Test7", "Test Value 7");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test6\":\"Test Value 6\",\"X-Test7\":\"Test Value 7\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = new Dictionary<string, string>()
                    {
                        { "X-Test8", "Test Value 8" }
                    }
                }
            };
            personalization = new Personalization()
            {
                Headers = new Dictionary<string, string>()
                {
                    { "X-Test9", "Test Value 9" }
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddHeader("X-Test10", "Test Value 10");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test8\":\"Test Value 8\",\"X-Test10\":\"Test Value 10\"}},{\"headers\":{\"X-Test9\":\"Test Value 9\"}}]}");
        }

        [Fact]
        public void TestAddHeaders()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var headers = new Dictionary<string, string>();
            headers.Add("X-Test1", "Test Value 1");
            headers.Add("X-Test2", "Test Value 2");
            msg.AddHeaders(headers);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test1\":\"Test Value 1\",\"X-Test2\":\"Test Value 2\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            headers = new Dictionary<string, string>();
            headers.Add("X-Test3", "Test Value 3");
            headers.Add("X-Test4", "Test Value 4");
            var personalization = new Personalization()
            {
                Headers = headers
            };
            headers = new Dictionary<string, string>();
            headers.Add("X-Test5", "Test Value 5");
            headers.Add("X-Test6", "Test Value 6");
            msg.AddHeaders(headers, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test3\":\"Test Value 3\",\"X-Test4\":\"Test Value 4\",\"X-Test5\":\"Test Value 5\",\"X-Test6\":\"Test Value 6\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            headers = new Dictionary<string, string>();
            headers.Add("X-Test7", "Test Value 7");
            headers.Add("X-Test8", "Test Value 8");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = headers
                }
            };
            headers = new Dictionary<string, string>();
            headers.Add("X-Test9", "Test Value 9");
            headers.Add("X-Test10", "Test Value 10");
            personalization = new Personalization()
            {
                Headers = headers
            };
            headers = new Dictionary<string, string>();
            headers.Add("X-Test11", "Test Value 11");
            headers.Add("X-Test12", "Test Value 12");
            msg.AddHeaders(headers, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test7\":\"Test Value 7\",\"X-Test8\":\"Test Value 8\"}},{\"headers\":{\"X-Test9\":\"Test Value 9\",\"X-Test10\":\"Test Value 10\",\"X-Test11\":\"Test Value 11\",\"X-Test12\":\"Test Value 12\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            headers = new Dictionary<string, string>();
            headers.Add("X-Test13", "Test Value 13");
            headers.Add("X-Test14", "Test Value 14");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = headers
                }
            };
            headers = new Dictionary<string, string>();
            headers.Add("X-Test15", "Test Value 15");
            headers.Add("X-Test16", "Test Value 16");
            msg.AddHeaders(headers);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test13\":\"Test Value 13\",\"X-Test14\":\"Test Value 14\",\"X-Test15\":\"Test Value 15\",\"X-Test16\":\"Test Value 16\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            headers = new Dictionary<string, string>();
            headers.Add("X-Test17", "Test Value 17");
            headers.Add("X-Test18", "Test Value 18");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Headers = headers
                }
            };
            headers = new Dictionary<string, string>();
            headers.Add("X-Test19", "Test Value 19");
            headers.Add("X-Test20", "Test Value 20");
            personalization = new Personalization()
            {
                Headers = headers
            };
            msg.Personalizations.Add(personalization);
            headers = new Dictionary<string, string>();
            headers.Add("X-Test21", "Test Value 21");
            headers.Add("X-Test22", "Test Value 22");
            msg.AddHeaders(headers);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"headers\":{\"X-Test17\":\"Test Value 17\",\"X-Test18\":\"Test Value 18\",\"X-Test21\":\"Test Value 21\",\"X-Test22\":\"Test Value 22\"}},{\"headers\":{\"X-Test19\":\"Test Value 19\",\"X-Test20\":\"Test Value 20\"}}]}");
        }

        [Fact]
        public void TestAddSubstitution()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddSubstitution("-sub1-", "Substituted Value 1");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub1-\":\"Substituted Value 1\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var personalization = new Personalization()
            {
                Substitutions = new Dictionary<string, string>()
                {
                    { "-sub2-", "Substituted Value 2" }
                }
            };
            msg.AddSubstitution("-sub3-", "Substituted Value 3", 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub2-\":\"Substituted Value 2\",\"-sub3-\":\"Substituted Value 3\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = new Dictionary<string, string>()
                    {
                        { "-sub4-", "Substituted Value 4" }
                    }
                }
            };
            personalization = new Personalization()
            {
                Substitutions = new Dictionary<string, string>()
                {
                    { "-sub5-", "Substituted Value 5" }
                }
            };
            msg.AddSubstitution("-sub6-", "Substituted Value 6", 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub4-\":\"Substituted Value 4\"}},{\"substitutions\":{\"-sub5-\":\"Substituted Value 5\",\"-sub6-\":\"Substituted Value 6\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = new Dictionary<string, string>()
                    {
                        { "-sub7-", "Substituted Value 7" }
                    }
                }
            };
            msg.AddSubstitution("-sub8-", "Substituted Value 8");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub7-\":\"Substituted Value 7\",\"-sub8-\":\"Substituted Value 8\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = new Dictionary<string, string>()
                    {
                        { "-sub9-", "Substituted Value 9" }
                    }
                }
            };
            personalization = new Personalization()
            {
                Substitutions = new Dictionary<string, string>()
                {
                    { "-sub10-", "Substituted Value 10" }
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddSubstitution("-sub11-", "Substituted Value 11");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub9-\":\"Substituted Value 9\",\"-sub11-\":\"Substituted Value 11\"}},{\"substitutions\":{\"-sub10-\":\"Substituted Value 10\"}}]}");
        }

        [Fact]
        public void TestAddSubstitutions()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub12-", "Substituted Value 12");
            substitutions.Add("-sub13-", "Substituted Value 13");
            msg.AddSubstitutions(substitutions);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub12-\":\"Substituted Value 12\",\"-sub13-\":\"Substituted Value 13\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub14-", "Substituted Value 14");
            substitutions.Add("-sub15-", "Substituted Value 15");
            var personalization = new Personalization()
            {
                Substitutions = substitutions
            };
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub16-", "Substituted Value 16");
            substitutions.Add("-sub17-", "Substituted Value 17");
            msg.AddSubstitutions(substitutions, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub14-\":\"Substituted Value 14\",\"-sub15-\":\"Substituted Value 15\",\"-sub16-\":\"Substituted Value 16\",\"-sub17-\":\"Substituted Value 17\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub18-", "Substituted Value 18");
            substitutions.Add("-sub19-", "Substituted Value 19");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = substitutions
                }
            };
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub20-", "Substituted Value 20");
            substitutions.Add("-sub21-", "Substituted Value 21");
            personalization = new Personalization()
            {
                Substitutions = substitutions
            };
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub22-", "Substituted Value 22");
            substitutions.Add("-sub23-", "Substituted Value 23");
            msg.AddSubstitutions(substitutions, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub18-\":\"Substituted Value 18\",\"-sub19-\":\"Substituted Value 19\"}},{\"substitutions\":{\"-sub20-\":\"Substituted Value 20\",\"-sub21-\":\"Substituted Value 21\",\"-sub22-\":\"Substituted Value 22\",\"-sub23-\":\"Substituted Value 23\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub24-", "Substituted Value 24");
            substitutions.Add("-sub25-", "Substituted Value 25");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = substitutions
                }
            };
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub26-", "Substituted Value 26");
            substitutions.Add("-sub27-", "Substituted Value 27");
            msg.AddSubstitutions(substitutions);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub24-\":\"Substituted Value 24\",\"-sub25-\":\"Substituted Value 25\",\"-sub26-\":\"Substituted Value 26\",\"-sub27-\":\"Substituted Value 27\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub28-", "Substituted Value 28");
            substitutions.Add("-sub29-", "Substituted Value 29");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    Substitutions = substitutions
                }
            };
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub30-", "Substituted Value 30");
            substitutions.Add("-sub31-", "Substituted Value 31");
            personalization = new Personalization()
            {
                Substitutions = substitutions
            };
            msg.Personalizations.Add(personalization);
            substitutions = new Dictionary<string, string>();
            substitutions.Add("-sub32-", "Substituted Value 32");
            substitutions.Add("-sub33-", "Substituted Value 33");
            msg.AddSubstitutions(substitutions);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"substitutions\":{\"-sub28-\":\"Substituted Value 28\",\"-sub29-\":\"Substituted Value 29\",\"-sub32-\":\"Substituted Value 32\",\"-sub33-\":\"Substituted Value 33\"}},{\"substitutions\":{\"-sub30-\":\"Substituted Value 30\",\"-sub31-\":\"Substituted Value 31\"}}]}");
        }

        [Fact]
        public void TestAddCustomArg()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.AddCustomArg("arg1", "Arguement Value 1");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg1\":\"Arguement Value 1\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var personalization = new Personalization()
            {
                CustomArgs = new Dictionary<string, string>()
                {
                    { "arg2", "Arguement Value 2" }
                }
            };
            msg.AddCustomArg("arg3", "Arguement Value 3", 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg2\":\"Arguement Value 2\",\"arg3\":\"Arguement Value 3\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = new Dictionary<string, string>()
                    {
                        { "arg4", "Arguement Value 4" }
                    }
                }
            };
            personalization = new Personalization()
            {
                CustomArgs = new Dictionary<string, string>()
                {
                    { "arg5", "Arguement Value 5" }
                }
            };
            msg.AddCustomArg("arg6", "Arguement Value 6", 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg4\":\"Arguement Value 4\"}},{\"custom_args\":{\"arg5\":\"Arguement Value 5\",\"arg6\":\"Arguement Value 6\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = new Dictionary<string, string>()
                    {
                        { "arg7", "Arguement Value 7" }
                    }
                }
            };
            msg.AddCustomArg("arg8", "Arguement Value 8");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg7\":\"Arguement Value 7\",\"arg8\":\"Arguement Value 8\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = new Dictionary<string, string>()
                    {
                        { "arg9", "Arguement Value 9" }
                    }
                }
            };
            personalization = new Personalization()
            {
                CustomArgs = new Dictionary<string, string>()
                {
                    { "arg10", "Arguement Value 10" }
                }
            };
            msg.Personalizations.Add(personalization);
            msg.AddCustomArg("arg11", "Arguement Value 11");
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg9\":\"Arguement Value 9\",\"arg11\":\"Arguement Value 11\"}},{\"custom_args\":{\"arg10\":\"Arguement Value 10\"}}]}");
        }

        [Fact]
        public void TestAddCustomArgs()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            var customArgs = new Dictionary<string, string>();
            customArgs.Add("arg12", "Arguement Value 12");
            customArgs.Add("arg13", "Arguement Value 13");
            msg.AddCustomArgs(customArgs);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg12\":\"Arguement Value 12\",\"arg13\":\"Arguement Value 13\"}}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg14", "Arguement Value 14");
            customArgs.Add("arg15", "Arguement Value 15");
            var personalization = new Personalization()
            {
                CustomArgs = customArgs
            };
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg16", "Arguement Value 16");
            customArgs.Add("arg17", "Arguement Value 17");
            msg.AddCustomArgs(customArgs, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg14\":\"Arguement Value 14\",\"arg15\":\"Arguement Value 15\",\"arg16\":\"Arguement Value 16\",\"arg17\":\"Arguement Value 17\"}}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg18", "Arguement Value 18");
            customArgs.Add("arg19", "Arguement Value 19");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = customArgs
                }
            };
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg20", "Arguement Value 20");
            customArgs.Add("arg21", "Arguement Value 21");
            personalization = new Personalization()
            {
                CustomArgs = customArgs
            };
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg22", "Arguement Value 22");
            customArgs.Add("arg23", "Arguement Value 23");
            msg.AddCustomArgs(customArgs, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg18\":\"Arguement Value 18\",\"arg19\":\"Arguement Value 19\"}},{\"custom_args\":{\"arg20\":\"Arguement Value 20\",\"arg21\":\"Arguement Value 21\",\"arg22\":\"Arguement Value 22\",\"arg23\":\"Arguement Value 23\"}}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg24", "Arguement Value 24");
            customArgs.Add("arg25", "Arguement Value 25");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = customArgs
                }
            };
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg26", "Arguement Value 26");
            customArgs.Add("arg27", "Arguement Value 27");
            msg.AddCustomArgs(customArgs);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg24\":\"Arguement Value 24\",\"arg25\":\"Arguement Value 25\",\"arg26\":\"Arguement Value 26\",\"arg27\":\"Arguement Value 27\"}}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg28", "Arguement Value 28");
            customArgs.Add("arg29", "Arguement Value 29");
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    CustomArgs = customArgs
                }
            };
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg30", "Arguement Value 30");
            customArgs.Add("arg31", "Arguement Value 31");
            personalization = new Personalization()
            {
                CustomArgs = customArgs
            };
            msg.Personalizations.Add(personalization);
            customArgs = new Dictionary<string, string>();
            customArgs.Add("arg32", "Arguement Value 32");
            customArgs.Add("arg33", "Arguement Value 33");
            msg.AddCustomArgs(customArgs);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"custom_args\":{\"arg28\":\"Arguement Value 28\",\"arg29\":\"Arguement Value 29\",\"arg32\":\"Arguement Value 32\",\"arg33\":\"Arguement Value 33\"}},{\"custom_args\":{\"arg30\":\"Arguement Value 30\",\"arg31\":\"Arguement Value 31\"}}]}");
        }

        [Fact]
        public void TestSendAt()
        {
            // Personalization not passed in, Personalization does not exist
            var msg = new SendGridMessage();
            msg.SetSendAt(1409348513);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"send_at\":1409348513}]}");

            // Personalization passed in, no Personalizations
            msg = new SendGridMessage();
            var sendAt = 1409348513;
            var personalization = new Personalization()
            {
                SendAt = sendAt
            };
            msg.SetSendAt(1409348513, 0, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"send_at\":1409348513}]}");

            // Personalization passed in, Personalization exists
            msg = new SendGridMessage();
            sendAt = 1409348513;
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    SendAt = sendAt
                }
            };
            sendAt = 1409348513;
            personalization = new Personalization()
            {
                SendAt = sendAt
            };
            msg.SetSendAt(1409348513, 1, personalization);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"send_at\":1409348513},{\"send_at\":1409348513}]}");

            // Personalization not passed in Personalization exists
            msg = new SendGridMessage();
            sendAt = 1409348513;
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    SendAt = sendAt
                }
            };
            msg.SetSendAt(1409348513);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"send_at\":1409348513}]}");

            // Personalization not passed in Personalizations exists
            msg = new SendGridMessage();
            sendAt = 1409348513;
            msg.Personalizations = new List<Personalization>() {
                new Personalization() {
                    SendAt = sendAt
                }
            };
            sendAt = 1409348513;
            personalization = new Personalization()
            {
                SendAt = sendAt
            };
            msg.Personalizations.Add(personalization);
            msg.SetSendAt(1409348513);
            Assert.True(msg.Serialize() == "{\"personalizations\":[{\"send_at\":1409348513},{\"send_at\":1409348513}]}");
        }

        [Fact]
        public void TestSetFrom()
        {
            var msg = new SendGridMessage();
            var fromEmail = new EmailAddress()
            {
                Email = "test1@example.com",
                Name = "Test User1"
            };
            msg.SetFrom(fromEmail);
            Assert.True(msg.Serialize() == "{\"from\":{\"name\":\"Test User1\",\"email\":\"test1@example.com\"}}");
        }

        [Fact]
        public void TestSetReplyTo()
        {
            var msg = new SendGridMessage();
            var replyToEmail = new EmailAddress()
            {
                Email = "test2@example.com",
                Name = "Test User2"
            };
            msg.SetReplyTo(replyToEmail);
            Assert.True(msg.Serialize() == "{\"reply_to\":{\"name\":\"Test User2\",\"email\":\"test2@example.com\"}}");
        }

        [Fact]
        public void TestSetGlobalSubject()
        {
            var msg = new SendGridMessage();
            var globalSubject = "subject1";
            msg.SetGlobalSubject(globalSubject);
            Assert.True(msg.Serialize() == "{\"subject\":\"subject1\"}");
        }

        [Fact]
        public void TestAddContent()
        {
            //Content object does not exist
            var msg = new SendGridMessage();
            msg.AddContent(MimeType.Html, "content1");
            Assert.True(msg.Serialize() == "{\"content\":[{\"type\":\"text/html\",\"value\":\"content1\"}]}");

            msg.AddContent(MimeType.Text, "content2");
            Assert.True(msg.Serialize() == "{\"content\":[{\"type\":\"text/plain\",\"value\":\"content2\"},{\"type\":\"text/html\",\"value\":\"content1\"}]}");


            //Content object exists
            msg = new SendGridMessage();
            var content = new Content()
            {
                Type = MimeType.Html,
                Value = "content3"
            };
            msg.Contents = new List<Content>();
            msg.Contents.Add(content);
            msg.AddContent(MimeType.Text, "content4");
            Assert.True(msg.Serialize() == "{\"content\":[{\"type\":\"text/plain\",\"value\":\"content4\"},{\"type\":\"text/html\",\"value\":\"content3\"}]}");
        }

        [Fact]
        public void TestAddContents()
        {
            //Content object does not exist
            var msg = new SendGridMessage();
            var contents = new List<Content>();
            var content = new Content()
            {
                Type = MimeType.Html,
                Value = "content5"
            };
            contents.Add(content);
            content = new Content()
            {
                Type = MimeType.Text,
                Value = "content6"
            };
            contents.Add(content);
            msg.AddContents(contents);
            Assert.True(msg.Serialize() == "{\"content\":[{\"type\":\"text/plain\",\"value\":\"content6\"},{\"type\":\"text/html\",\"value\":\"content5\"}]}");

            //Content object exists
            msg = new SendGridMessage();
            content = new Content()
            {
                Type = MimeType.Html,
                Value = "content7"
            };
            msg.Contents = new List<Content>();
            msg.Contents.Add(content);
            contents = new List<Content>();
            content = new Content()
            {
                Type = "fake/mimetype",
                Value = "content8"
            };
            contents.Add(content);
            content = new Content()
            {
                Type = MimeType.Text,
                Value = "content9"
            };
            contents.Add(content);
            msg.AddContents(contents);
            Assert.True(msg.Serialize() == "{\"content\":[{\"type\":\"text/plain\",\"value\":\"content9\"},{\"type\":\"text/html\",\"value\":\"content7\"},{\"type\":\"fake/mimetype\",\"value\":\"content8\"}]}");
        }

        [Fact]
        public void TestAddAttachment()
        {
            //Attachment object does not exist
            var msg = new SendGridMessage();
            msg.AddAttachment("filename1", "base64content1", "jpg", "inline", "id1");
            Assert.True(msg.Serialize() == "{\"attachments\":[{\"content\":\"base64content1\",\"type\":\"jpg\",\"filename\":\"filename1\",\"disposition\":\"inline\",\"content_id\":\"id1\"}]}");

            //Attachment object exists
            msg = new SendGridMessage();
            var attachment = new Attachment()
            {
                Filename = "filename2",
                Content = "base64content2",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id2"
            };
            msg.Attachments = new List<Attachment>();
            msg.Attachments.Add(attachment);
            msg.AddAttachment("filename3", "base64content3", "jpg", "inline", "id3");
            Assert.True(msg.Serialize() == "{\"attachments\":[{\"content\":\"base64content2\",\"type\":\"jpg\",\"filename\":\"filename2\",\"disposition\":\"inline\",\"content_id\":\"id2\"},{\"content\":\"base64content3\",\"type\":\"jpg\",\"filename\":\"filename3\",\"disposition\":\"inline\",\"content_id\":\"id3\"}]}");
        }

        [Fact]
        public void TestAddAttachments()
        {
            //Attachment object does not exist
            var msg = new SendGridMessage();
            var attachments = new List<Attachment>();
            var attachment = new Attachment()
            {
                Filename = "filename4",
                Content = "base64content4",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id4"
            };
            attachments.Add(attachment);
            attachment = new Attachment()
            {
                Filename = "filename5",
                Content = "base64content5",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id5"
            };
            attachments.Add(attachment);
            msg.AddAttachments(attachments);
            Assert.True(msg.Serialize() == "{\"attachments\":[{\"content\":\"base64content4\",\"type\":\"jpg\",\"filename\":\"filename4\",\"disposition\":\"inline\",\"content_id\":\"id4\"},{\"content\":\"base64content5\",\"type\":\"jpg\",\"filename\":\"filename5\",\"disposition\":\"inline\",\"content_id\":\"id5\"}]}");

            //Attachment object exists
            msg = new SendGridMessage();
            attachment = new Attachment()
            {
                Filename = "filename6",
                Content = "base64content6",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id6"
            };
            msg.Attachments = new List<Attachment>();
            msg.Attachments.Add(attachment);
            attachments = new List<Attachment>();
            attachment = new Attachment()
            {
                Filename = "filename7",
                Content = "base64content7",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id7"
            };
            attachments.Add(attachment);
            attachment = new Attachment()
            {
                Filename = "filename8",
                Content = "base64content8",
                Type = "jpg",
                Disposition = "inline",
                ContentId = "id8"
            };
            attachments.Add(attachment);
            msg.AddAttachments(attachments);
            Assert.True(msg.Serialize() == "{\"attachments\":[{\"content\":\"base64content6\",\"type\":\"jpg\",\"filename\":\"filename6\",\"disposition\":\"inline\",\"content_id\":\"id6\"},{\"content\":\"base64content7\",\"type\":\"jpg\",\"filename\":\"filename7\",\"disposition\":\"inline\",\"content_id\":\"id7\"},{\"content\":\"base64content8\",\"type\":\"jpg\",\"filename\":\"filename8\",\"disposition\":\"inline\",\"content_id\":\"id8\"}]}");
        }

        [Fact]
        public void TestSetTemplateId()
        {
            var msg = new SendGridMessage();
            msg.SetTemplateId("template_id1");
            Assert.True(msg.Serialize() == "{\"template_id\":\"template_id1\"}");
        }

        [Fact]
        public void TestAddSection()
        {
            // Section object does not exist
            var msg = new SendGridMessage();
            msg.AddSection("section_key1", "section_value1");
            Assert.True(msg.Serialize() == "{\"sections\":{\"section_key1\":\"section_value1\"}}");

            // Section object exists
            msg.AddSection("section_key2", "section_value2");
            Assert.True(msg.Serialize() == "{\"sections\":{\"section_key1\":\"section_value1\",\"section_key2\":\"section_value2\"}}");
        }

        [Fact]
        public void TestAddSections()
        {
            // Section object does not exist
            var msg = new SendGridMessage();
            var sections = new Dictionary<string, string>()
            {
                { "section_key3", "section_value3" },
                { "section_key4", "section_value4" }
            };
            msg.AddSections(sections);
            Assert.True(msg.Serialize() == "{\"sections\":{\"section_key3\":\"section_value3\",\"section_key4\":\"section_value4\"}}");

            // Section object exists
            sections = new Dictionary<string, string>()
            {
                { "section_key5", "section_value5" },
                { "section_key6", "section_value6" }
            };
            msg.AddSections(sections);
            Assert.True(msg.Serialize() == "{\"sections\":{\"section_key3\":\"section_value3\",\"section_key4\":\"section_value4\",\"section_key5\":\"section_value5\",\"section_key6\":\"section_value6\"}}");
        }

        [Fact]
        public void TestAddGlobalHeader()
        {
            // Header object does not exist
            var msg = new SendGridMessage();
            msg.AddGlobalHeader("X-Header1", "Value1");
            Assert.True(msg.Serialize() == "{\"headers\":{\"X-Header1\":\"Value1\"}}");

            // Header object exists
            msg.AddGlobalHeader("X-Header2", "Value2");
            Assert.True(msg.Serialize() == "{\"headers\":{\"X-Header1\":\"Value1\",\"X-Header2\":\"Value2\"}}");
        }

        [Fact]
        public void TestAddGlobalHeaders()
        {
            // Header object does not exist
            var msg = new SendGridMessage();
            var headers = new Dictionary<string, string>()
            {
                { "X-Header3", "Value3" },
                { "X-Header4", "Value4" }
            };
            msg.AddGlobalHeaders(headers);
            Assert.True(msg.Serialize() == "{\"headers\":{\"X-Header3\":\"Value3\",\"X-Header4\":\"Value4\"}}");

            // Header object exists
            headers = new Dictionary<string, string>()
            {
                { "X-Header5", "Value5" },
                { "X-Header6", "Value6" }
            };
            msg.AddGlobalHeaders(headers);
            Assert.True(msg.Serialize() == "{\"headers\":{\"X-Header3\":\"Value3\",\"X-Header4\":\"Value4\",\"X-Header5\":\"Value5\",\"X-Header6\":\"Value6\"}}");
        }

        [Fact]
        public void TestAddCategory()
        {
            //Categories object does not exist
            var msg = new SendGridMessage();
            msg.AddCategory("category1");
            Assert.True(msg.Serialize() == "{\"categories\":[\"category1\"]}");

            msg.AddCategory("category2");
            Assert.True(msg.Serialize() == "{\"categories\":[\"category1\",\"category2\"]}");

            //Categories object exists
            msg = new SendGridMessage();
            msg.Categories = new List<string>();
            msg.Categories.Add("category3");
            msg.AddCategory("category4");
            Assert.True(msg.Serialize() == "{\"categories\":[\"category3\",\"category4\"]}");
        }

        [Fact]
        public void TestAddCategories()
        {
            //Categories object does not exist
            var msg = new SendGridMessage();
            var categories = new List<string>();
            categories.Add("category5");
            categories.Add("category6");
            msg.AddCategories(categories);
            Assert.True(msg.Serialize() == "{\"categories\":[\"category5\",\"category6\"]}");

            //Categories object exists
            msg = new SendGridMessage();
            msg = new SendGridMessage();
            msg.Categories = new List<string>();
            msg.Categories.Add("category7");
            msg.Categories.Add("category8");
            categories = new List<string>();
            categories.Add("category9");
            categories.Add("category10");
            msg.AddCategories(categories);
            Assert.True(msg.Serialize() == "{\"categories\":[\"category7\",\"category8\",\"category9\",\"category10\"]}");
        }

        [Fact]
        public void TestAddGlobalCustomArg()
        {
            // CustomArgs object does not exist
            var msg = new SendGridMessage();
            msg.AddGlobalCustomArg("Key1", "Value1");
            Assert.True(msg.Serialize() == "{\"custom_args\":{\"Key1\":\"Value1\"}}");

            // CustomArgs object exists
            msg.AddGlobalCustomArg("Key2", "Value2");
            Assert.True(msg.Serialize() == "{\"custom_args\":{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}}");
        }

        [Fact]
        public void TestAddGlobalCustomArgs()
        {
            // CustomArgs object does not exist
            var msg = new SendGridMessage();
            var customArgs = new Dictionary<string, string>()
            {
                { "Key3", "Value3" },
                { "Key4", "Value4" }
            };
            msg.AddGlobalCustomArgs(customArgs);
            Assert.True(msg.Serialize() == "{\"custom_args\":{\"Key3\":\"Value3\",\"Key4\":\"Value4\"}}");

            // CustomArgs object exists
            customArgs = new Dictionary<string, string>()
            {
                { "Key5", "Value5" },
                { "Key6", "Value6" }
            };
            msg.AddGlobalCustomArgs(customArgs);
            Assert.True(msg.Serialize() == "{\"custom_args\":{\"Key3\":\"Value3\",\"Key4\":\"Value4\",\"Key5\":\"Value5\",\"Key6\":\"Value6\"}}");
        }

        [Fact]
        public void TestSetGlobalSendAt()
        {
            var msg = new SendGridMessage();
            msg.SetGlobalSendAt(1409348513);
            Assert.True(msg.Serialize() == "{\"send_at\":1409348513}");
        }

        [Fact]
        public void TestSetBatchId()
        {
            var msg = new SendGridMessage();
            msg.SetBatchId("batch_id");
            Assert.True(msg.Serialize() == "{\"batch_id\":\"batch_id\"}");
        }

        [Fact]
        public void TestSetAsm()
        {
            var msg = new SendGridMessage();
            var groupsToDisplay = new List<int>()
            {
                1, 2, 3, 4, 5
            };
            msg.SetAsm(1, groupsToDisplay);
            Assert.True(msg.Serialize() == "{\"asm\":{\"group_id\":1,\"groups_to_display\":[1,2,3,4,5]}}");
        }

        [Fact]
        public void TestSetIpPoolName()
        {
            var msg = new SendGridMessage();
            msg.SetIpPoolName("pool_name");
            Assert.True(msg.Serialize() == "{\"ip_pool_name\":\"pool_name\"}");
        }

        [Fact]
        public void TestSetBccSetting()
        {
            //MailSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetBccSetting(true, "test@example.com");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test@example.com\"}}}");

            //MailSettings object exists
            msg = new SendGridMessage();
            var bccSetting = new BCCSettings()
            {
                Enable = false,
                Email = "test2@example.com"
            };
            msg.MailSettings = new MailSettings()
            {
                BccSettings = bccSetting
            };
            msg.SetBccSetting(true, "test3@example.com");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"bcc\":{\"enable\":true,\"email\":\"test3@example.com\"}}}");
        }

        [Fact]
        public void TestSetBypassListManagement()
        {
            //MailSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetBypassListManagement(false);
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"bypass_list_management\":{\"enable\":false}}}");

            //MailSettings object exists
            msg = new SendGridMessage();
            var bypassListManagement = new BypassListManagement()
            {
                Enable = true
            };
            msg.MailSettings = new MailSettings()
            {
                BypassListManagement = bypassListManagement
            };
            msg.SetBypassListManagement(true);
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"bypass_list_management\":{\"enable\":true}}}");
        }

        [Fact]
        public void TestSetFooterSetting()
        {
            //MailSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetFooterSetting(true, "html1", "text1");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"footer\":{\"enable\":true,\"text\":\"text1\",\"html\":\"html1\"}}}");

            //MailSettings object exists
            msg = new SendGridMessage();
            var footerSetting = new FooterSettings()
            {
                Enable = false,
                Html = "<strong>html2</strong>",
                Text = "text2"
            };
            msg.MailSettings = new MailSettings()
            {
                FooterSettings = footerSetting
            };
            msg.SetFooterSetting(true, "html3", "text3");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"footer\":{\"enable\":true,\"text\":\"text3\",\"html\":\"html3\"}}}");
        }

        [Fact]
        public void TestSetSandBoxMode()
        {
            //MailSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetSandBoxMode(true);
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"sandbox_mode\":{\"enable\":true}}}");

            //MailSettings object exists
            msg = new SendGridMessage();
            var sandBoxMode = new SandboxMode()
            {
                Enable = false
            };
            msg.MailSettings = new MailSettings()
            {
                SandboxMode = sandBoxMode
            };
            msg.SetSandBoxMode(true);
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"sandbox_mode\":{\"enable\":true}}}");
        }

        [Fact]
        public void TestSetSpamCheck()
        {
            //MailSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetSpamCheck(true, 1, "http://fakeurl.com");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"spam_check\":{\"enable\":true,\"threshold\":1,\"post_to_url\":\"http://fakeurl.com\"}}}");

            //MailSettings object exists
            msg = new SendGridMessage();
            var spamCheck = new SpamCheck()
            {
                Enable = false,
                Threshold = 3,
                PostToUrl = "http://fakeurl1.com"
            };
            msg.MailSettings = new MailSettings()
            {
                SpamCheck = spamCheck
            };
            msg.SetSpamCheck(true, 2, "http://fakeurl2.com");
            Assert.True(msg.Serialize() == "{\"mail_settings\":{\"spam_check\":{\"enable\":true,\"threshold\":2,\"post_to_url\":\"http://fakeurl2.com\"}}}");
        }

        [Fact]
        public void TestSetClickTracking()
        {
            //TrackingSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetClickTracking(false, false);
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"click_tracking\":{\"enable\":false,\"enable_text\":false}}}");

            //TrackingSettings object exists
            msg = new SendGridMessage();
            var clickTrackingSetting = new ClickTracking()
            {
                Enable = false,
                EnableText = false
            };
            msg.TrackingSettings = new TrackingSettings()
            {
                ClickTracking = clickTrackingSetting
            };
            msg.SetClickTracking(true, true);
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"click_tracking\":{\"enable\":true,\"enable_text\":true}}}");
        }

        [Fact]
        public void TestSetOpenTracking()
        {
            //TrackingSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetOpenTracking(false, "subtag1");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"open_tracking\":{\"enable\":false,\"substitution_tag\":\"subtag1\"}}}");

            //TrackingSettings object exists
            msg = new SendGridMessage();
            var openTrackingSetting = new OpenTracking()
            {
                Enable = false,
                SubstitutionTag = "subtag2"
            };
            msg.TrackingSettings = new TrackingSettings()
            {
                OpenTracking = openTrackingSetting
            };
            msg.SetOpenTracking(false, "subtag3");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"open_tracking\":{\"enable\":false,\"substitution_tag\":\"subtag3\"}}}");
        }

        [Fact]
        public void TestSetSubscriptionTracking()
        {
            //TrackingSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetSubscriptionTracking(true, "html1", "text1", "sub1");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"subscription_tracking\":{\"enable\":true,\"text\":\"text1\",\"html\":\"html1\",\"substitution_tag\":\"sub1\"}}}");

            //TrackingSettings object exists
            msg = new SendGridMessage();
            var subscriptionTracking = new SubscriptionTracking()
            {
                Enable = false,
                Html = "html2",
                Text = "text2",
                SubstitutionTag = "sub2"
            };
            msg.TrackingSettings = new TrackingSettings()
            {
                SubscriptionTracking = subscriptionTracking
            };
            msg.SetSubscriptionTracking(true, "html3", "text3", "sub3");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"subscription_tracking\":{\"enable\":true,\"text\":\"text3\",\"html\":\"html3\",\"substitution_tag\":\"sub3\"}}}");
        }

        [Fact]
        public void TestSetGoogleAnalytics()
        {
            //TrackingSettings object does not exist
            var msg = new SendGridMessage();
            msg.SetGoogleAnalytics(true, "campaign1", "content1", "medium1", "source1", "term1");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"ganalytics\":{\"enable\":true,\"utm_source\":\"source1\",\"utm_medium\":\"medium1\",\"utm_term\":\"term1\",\"utm_content\":\"content1\",\"utm_campaign\":\"campaign1\"}}}");

            //TrackingSettings object exists
            msg = new SendGridMessage();
            var googleAnalytics = new Ganalytics()
            {
                Enable = false,
                UtmCampaign = "campaign2",
                UtmContent = "content2",
                UtmMedium = "medium2",
                UtmSource = "source2",
                UtmTerm = "term2"
            };
            msg.TrackingSettings = new TrackingSettings()
            {
                Ganalytics = googleAnalytics
            };
            msg.SetGoogleAnalytics(true, "campaign3", "content3", "medium3", "source3", "term3");
            Assert.True(msg.Serialize() == "{\"tracking_settings\":{\"ganalytics\":{\"enable\":true,\"utm_source\":\"source3\",\"utm_medium\":\"medium3\",\"utm_term\":\"term3\",\"utm_content\":\"content3\",\"utm_campaign\":\"campaign3\"}}}");
        }
        [Fact]
        public async Task TestAccessSettingsActivityGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/activity", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAccessSettingsWhitelistPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAccessSettingsWhitelistGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/whitelist", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAccessSettingsWhitelistDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAccessSettingsWhitelistRuleIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "access_settings/whitelist/" + rule_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAccessSettingsWhitelistRuleIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var rule_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "access_settings/whitelist/" + rule_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAlertsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAlertsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "alerts", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAlertsAlertIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAlertsAlertIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "alerts/" + alert_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAlertsAlertIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var alert_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "alerts/" + alert_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "api_keys", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysApiKeyIdPut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysApiKeyIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysApiKeyIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "api_keys/" + api_key_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestApiKeysApiKeyIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var api_key_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "api_keys/" + api_key_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'id': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdSuppressionsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdSuppressionsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var group_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/groups/" + group_id + "/suppressions", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdSuppressionsSearchPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmGroupsGroupIdSuppressionsEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var group_id = "test_url_param";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/groups/" + group_id + "/suppressions/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmSuppressionsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmSuppressionsGlobalPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmSuppressionsGlobalEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/global/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmSuppressionsGlobalEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "asm/suppressions/global/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestAsmSuppressionsEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "asm/suppressions/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestBrowsersStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns/" + campaign_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "campaigns/" + campaign_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "campaigns/" + campaign_id + "/schedules", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "campaigns/" + campaign_id + "/schedules", requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesNowPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var campaign_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "campaigns/" + campaign_id + "/schedules/now", requestHeaders: headers);
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestCampaignsCampaignIdSchedulesTestPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestCategoriesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'category': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "categories", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCategoriesStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestCategoriesStatsSumsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestClientsStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'aggregated_by': 'day', 
  'end_date': '2016-04-01', 
  'start_date': '2016-01-01'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "clients/stats", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestClientsClientTypeStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbCustomFieldsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbCustomFieldsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbCustomFieldsCustomFieldIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/custom_fields/" + custom_field_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbCustomFieldsCustomFieldIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var custom_field_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/custom_fields/" + custom_field_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.Accepted == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'name': 'your list name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'list_id': 1
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var list_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "202");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/lists/" + list_id, queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.Accepted == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdRecipientsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdRecipientsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdRecipientsRecipientIdPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var list_id = "test_url_param";
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "contactdb/lists/" + list_id + "/recipients/" + recipient_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbListsListIdRecipientsRecipientIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsBillableCountGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/billable_count", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsCountGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/count", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsSearchGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  '{field_name}': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/search", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsRecipientIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsRecipientIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/recipients/" + recipient_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbRecipientsRecipientIdListsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var recipient_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/recipients/" + recipient_id + "/lists", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbReservedFieldsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/reserved_fields", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsSegmentIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsSegmentIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'segment_id': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsSegmentIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'delete_contacts': 'true'
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "contactdb/segments/" + segment_id, queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestContactdbSegmentsSegmentIdRecipientsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'page': 1, 
  'page_size': 1
}";
            var segment_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "contactdb/segments/" + segment_id + "/recipients", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestDevicesStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestGeoStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsAssignedGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/assigned", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'name': 'marketing'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/pools", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPoolNamePut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPoolNameGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/pools/" + pool_name, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPoolNameDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var pool_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPoolNameIpsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsPoolsPoolNameIpsIpDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var pool_name = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/pools/" + pool_name + "/ips/" + ip, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsWarmupPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'ip': '0.0.0.0'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "ips/warmup", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsWarmupGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsWarmupIpAddressGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/warmup/" + ip_address, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsWarmupIpAddressDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "ips/warmup/" + ip_address, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestIpsIpAddressGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var ip_address = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "ips/" + ip_address, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailBatchPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "mail/batch", requestHeaders: headers);
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestMailBatchBatchIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail/batch/" + batch_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSendPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Accepted == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsAddressWhitelistPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsAddressWhitelistGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/address_whitelist", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsBccPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsBccGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/bcc", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsBouncePurgePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsBouncePurgeGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/bounce_purge", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsFooterPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsFooterGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/footer", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsForwardBouncePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsForwardBounceGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/forward_bounce", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsForwardSpamPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsForwardSpamGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/forward_spam", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsPlainContentPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'enabled': false
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "mail_settings/plain_content", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsPlainContentGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/plain_content", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsSpamCheckPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsSpamCheckGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/spam_check", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsTemplatePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailSettingsTemplateGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "mail_settings/template", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestMailboxProvidersStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestPartnerSettingsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "partner_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestPartnerSettingsNewRelicPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestPartnerSettingsNewRelicGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "partner_settings/new_relic", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestScopesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "scopes", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "senders", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersSenderIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersSenderIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "senders/" + sender_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersSenderIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "senders/" + sender_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSendersSenderIdResendVerificationPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var sender_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "senders/" + sender_id + "/resend_verification", requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1, 
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersReputationsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'usernames': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/reputations", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersStatsMonthlyGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersStatsSumsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNamePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameIpsPut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameMonitorPut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameMonitorPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameMonitorGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "subusers/" + subuser_name + "/monitor", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameMonitorDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var subuser_name = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "subusers/" + subuser_name + "/monitor", requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSubusersSubuserNameStatsMonthlyGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBlocksGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBlocksDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBlocksEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/blocks/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBlocksEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/blocks/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBouncesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'end_time': 1, 
  'start_time': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBouncesDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBouncesEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/bounces/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionBouncesEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'email_address': 'example@example.com'
}";
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/bounces/" + email, queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionInvalidEmailsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionInvalidEmailsDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionInvalidEmailsEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/invalid_emails/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionInvalidEmailsEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/invalid_emails/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionSpamReportEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "suppression/spam_report/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionSpamReportEmailDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var email = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "suppression/spam_report/" + email, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionSpamReportsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionSpamReportsDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestSuppressionUnsubscribesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'name': 'example_name'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "201");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "templates", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates/" + template_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var template_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "templates/" + template_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdVersionsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdVersionsVersionIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdVersionsVersionIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "templates/" + template_id + "/versions/" + version_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdVersionsVersionIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "templates/" + template_id + "/versions/" + version_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestTemplatesTemplateIdVersionsVersionIdActivatePost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var template_id = "test_url_param";
            var version_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "templates/" + template_id + "/versions/" + version_id + "/activate", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsClickPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/click", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsClickGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/click", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsGoogleAnalyticsPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsGoogleAnalyticsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/google_analytics", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsOpenPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'enabled': true
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PATCH, urlPath: "tracking_settings/open", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsOpenGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/open", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsSubscriptionPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestTrackingSettingsSubscriptionGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "tracking_settings/subscription", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserAccountGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/account", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserCreditsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/credits", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserEmailPut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'email': 'example@example.com'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/email", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserEmailGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/email", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserPasswordPut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserProfilePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserProfileGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/profile", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserScheduledSendsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestUserScheduledSendsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserScheduledSendsBatchIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestUserScheduledSendsBatchIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/scheduled_sends/" + batch_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserScheduledSendsBatchIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var batch_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/scheduled_sends/" + batch_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestUserSettingsEnforcedTlsPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserSettingsEnforcedTlsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/settings/enforced_tls", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserUsernamePut()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'username': 'test_username'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.PUT, urlPath: "user/username", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserUsernameGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/username", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksEventSettingsPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksEventSettingsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/event/settings", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksEventTestPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string data = @"{
  'url': 'url'
}";
            Object json = JsonConvert.DeserializeObject<Object>(data);
            data = json.ToString();
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "user/webhooks/event/test", requestBody: data, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseSettingsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseSettingsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseSettingsHostnamePatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseSettingsHostnameGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "user/webhooks/parse/settings/" + hostname, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseSettingsHostnameDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var hostname = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "user/webhooks/parse/settings/" + hostname, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestUserWebhooksParseStatsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsDefaultGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/default", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsSubuserGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/subuser", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsSubuserDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/subuser", requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsDomainIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsDomainIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/domains/" + domain_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsDomainIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var domain_id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + domain_id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsDomainIdSubuserPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsIdIpsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsIdIpsIpDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            var ip = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/domains/" + id + "/ips/" + ip, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelDomainsIdValidatePost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/domains/" + id + "/validate", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelIpsPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelIpsGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'ip': 'test_string', 
  'limit': 1, 
  'offset': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelIpsIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/ips/" + id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelIpsIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/ips/" + id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelIpsIdValidatePost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/ips/" + id + "/validate", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.Created == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'limit': 1
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksDefaultGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'domain': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/default", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksSubuserGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/subuser", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksSubuserDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            string queryParams = @"{
  'username': 'test_string'
}";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/subuser", queryParams: queryParams, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksIdPatch()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksIdGet()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.GET, urlPath: "whitelabel/links/" + id, requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksIdDelete()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "204");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.DELETE, urlPath: "whitelabel/links/" + id, requestHeaders: headers);
            Assert.True(HttpStatusCode.NoContent == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksIdValidatePost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
            var id = "test_url_param";
            Dictionary<String, String> headers = new Dictionary<String, String>();
            headers.Clear();
            headers.Add("X-Mock", "200");
            Response response = await sg.RequestAsync(method: SendGridClient.Method.POST, urlPath: "whitelabel/links/" + id + "/validate", requestHeaders: headers);
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }

        [Fact]
        public async Task TestWhitelabelLinksLinkIdSubuserPost()
        {
            string host = "http://localhost:4010";
            var sg = new SendGridClient(fixture.apiKey, host);
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
            Assert.True(HttpStatusCode.OK == response.StatusCode);
        }
    }
}
