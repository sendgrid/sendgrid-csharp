using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    class TestSendgrid
    {
        [Test]
        public void TestDisableGravatar()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableGravatar();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"gravatar\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void TestDisableOpenTracking()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableOpenTracking();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"opentrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void DisableClickTracking()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableClickTracking();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"clicktrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void DisableSpamCheck()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableSpamCheck();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"spamcheck\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void DisableUnsubscribe()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableUnsubscribe();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"subscriptiontrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void DisableFooter()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableFooter();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"footer\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void DisableGoogleAnalytics()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableGoogleAnalytics();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"ganalytics\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);            
        }

        [Test]
        public void DisableTemplate()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableTemplate();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"template\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);  
        }

        [Test]
        public void DisableBcc()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableBcc();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"bcc\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);  
        }

        [Test]
        public void DisableBypassListManagement()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.DisableBypassListManagement();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"bypass_list_management\" : {\"settings\" : {\"enable\" : \"0\"}}}}}", json);
        }

        [Test]
        public void EnableGravatar()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.EnableGravatar();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"gravatar\" : {\"settings\" : {\"enable\" : \"1\"}}}}}", json);
        }

        [Test]
        public void EnableOpenTracking()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.EnableOpenTracking();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"opentrack\" : {\"settings\" : {\"enable\" : \"1\"}}}}}", json);
        }

        [Test]
        public void EnableClickTracking()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);
            var text = "hello world";
            sendgrid.EnableClickTracking(text);

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"clicktrack\" : {\"settings\" : {\"enable\" : \"1\",\"text\" : \"hello world\"}}}}}", json);
        }

        [Test]
        public void EnableSpamCheck()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            var score = 5;
            var url = "http://www.example.com";
            sendgrid.EnableSpamCheck(score, url);

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"spamcheck\" : {\"settings\" : {\"enable\" : \"1\",\"score\" : \"5\",\"url\" : \"http:\\/\\/www.example.com\"}}}}}", json);
        }

        [Test]
        public void EnableUnsubscribe()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            var text = "<% %>";
            var html = "<% name %>";
            var replace = "John";
            var url = "http://www.example.com";
            var landing = "this_landing";
            sendgrid.EnableUnsubscribe(text, html, replace, url, landing);

            var jsonText = "\"text\" : \""+text+"\"";
            var jsonHtml = "\"html\" : \""+html+"\"";
            var jsonReplace = "\"replace\" : \""+replace+"\"";
            var jsonUrl = "\"url\" : \"http:\\/\\/www.example.com\"";
            var jsonLanding = "\"landing\" : \""+landing+"\"";

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"subscriptiontrack\" : {\"settings\" : {\"enable\" : \"1\","+
                jsonText+","+jsonHtml+","+jsonReplace+","+jsonUrl+","+jsonLanding+"}}}}}", json);

        }

        [Test]
        public void EnableFooter()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            var text = "My Text";
            var html = "<body><p>hello, <% name %></p></body>";
            var escHtml = "<body><p>hello, <% name %><\\/p><\\/body>";

            sendgrid.EnableFooter(text, html);

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"footer\" : {\"settings\" : {\"enable\" : \"1\",\"text\" : \""+text+"\",\"html\" : \""+escHtml+"\"}}}}}", json);
        }

        [Test]
        public void EnableGoogleAnalytics()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            var source = "SomeDomain.com";
            var medium = "Email";
            var term = "keyword1, keyword2, keyword3";
            var content = "PG, PG13";
            var campaign = "my_campaign";

            sendgrid.EnableGoogleAnalytics(source, medium, term, content, campaign);

            var jsonSource = "\"source\" : \"SomeDomain.com\"";
            var jsonMedium = "\"medium\" : \""+medium+"\"";
            var jsonTerm = "\"term\" : \""+term+"\"";
            var jsonContent = "\"content\" : \""+content+"\"";
            var jsonCampaign = "\"campaign\" : \""+campaign+"\"";

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"ganalytics\" : {\"settings\" : {\"enable\" : \"1\","+
                            jsonSource+","+jsonMedium+","+jsonTerm+","+jsonContent+","+jsonCampaign+"}}}}}", json);
        }

        [Test]
        public void EnableTemplate()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);
            var html = "<% hadhdhd %>";

            var escHtml = "<% hadhdhd %>";
            sendgrid.EnableTemplate(html);

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"template\" : {\"settings\" : {\"enable\" : \"1\",\"html\" : \""+escHtml+"\"}}}}}", json);
        }

        [Test]
        public void EnableBcc()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            var email = "somebody@someplace.com";
            sendgrid.EnableBcc(email);

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"bcc\" : {\"settings\" : {\"enable\" : \"1\",\"email\" : \"" + email + "\"}}}}}", json);
        }

        [Test]
        public void EnableBypassListManagement()
        {
            var header = new Header();
            var sendgrid = new SendGrid(header);

            sendgrid.EnableBypassListManagement();

            String json = header.AsJson();
            Assert.AreEqual("{\"filters\" : {\"data\" : {\"bypass_list_management\" : {\"settings\" : {\"enable\" : \"1\"}}}}}", json);
        }
    }
}
