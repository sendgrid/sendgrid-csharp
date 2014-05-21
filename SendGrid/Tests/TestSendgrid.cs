using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using NUnit.Framework;
using SendGridMail;
using Smtpapi;

namespace Tests
{
	[TestFixture]
	internal class TestSendgrid
	{
		[Test]
		public void CreateMimeMessage()
		{
			var message = new SendGrid();
			var attachment = Path.GetTempFileName();
			var text = "this is a test";
			var html = "<b>This<\b> is a better test";
			var headers = new KeyValuePair<String, String>("custom", "header");
			message.AddAttachment(attachment);
			message.Text = text;
			message.Html = html;
			message.AddTo("foo@bar.com");
			message.From = new MailAddress("foo@bar.com");
			message.AddHeaders(new Dictionary<string, string> {{headers.Key, headers.Value}});
			message.EnableGravatar();

			var mime = message.CreateMimeMessage();

			var sr = new StreamReader(mime.AlternateViews[0].ContentStream);
			var result = sr.ReadToEnd();
			Assert.AreEqual(text, result);

			sr = new StreamReader(mime.AlternateViews[1].ContentStream);
			result = sr.ReadToEnd();
			Assert.AreEqual(html, result);

			result = mime.Headers.Get(headers.Key);
			Assert.AreEqual(headers.Value, result);

			result = mime.Headers.Get("X-Smtpapi");
			var expected = "{\"filters\" : {\"gravatar\" : {\"settings\" : {\"enable\" : \"1\"}}}}";
			Assert.AreEqual(expected, result);

			result = mime.Attachments[0].Name;
			Assert.AreEqual(Path.GetFileName(attachment), result);
		}

		[Test]
		public void DisableBcc()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableBcc();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"bcc\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableBypassListManagement()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableBypassListManagement();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"bypass_list_management\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableClickTracking()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableClickTracking();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"clicktrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableFooter()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableFooter();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"footer\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableGoogleAnalytics()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableGoogleAnalytics();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"ganalytics\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableSpamCheck()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableSpamCheck();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"spamcheck\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableTemplate()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableTemplate();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"template\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void DisableUnsubscribe()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableUnsubscribe();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"subscriptiontrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void EnableBcc()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			var email = "somebody@someplace.com";
			sendgrid.EnableBcc(email);

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"bcc\" : {\"settings\" : {\"enable\" : \"1\",\"email\" : \"" + email + "\"}}}}",
				json);
		}

		[Test]
		public void EnableBypassListManagement()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.EnableBypassListManagement();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"bypass_list_management\" : {\"settings\" : {\"enable\" : \"1\"}}}}", json);
		}

		[Test]
		public void EnableClickTracking()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);
			sendgrid.EnableClickTracking(true);

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"clicktrack\" : {\"settings\" : {\"enable\" : \"1\",\"enable_text\" : \"1\"}}}}",
				json);
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

			var json = header.JsonString();
			Assert.AreEqual(
				"{\"filters\" : {\"footer\" : {\"settings\" : {\"enable\" : \"1\",\"text\\/plain\" : \"" + text +
				"\",\"text\\/html\" : \"" + escHtml + "\"}}}}", json);
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

			var jsonSource = "\"utm_source\" : \"SomeDomain.com\"";
			var jsonMedium = "\"utm_medium\" : \"" + medium + "\"";
			var jsonTerm = "\"utm_term\" : \"" + term + "\"";
			var jsonContent = "\"utm_content\" : \"" + content + "\"";
			var jsonCampaign = "\"utm_campaign\" : \"" + campaign + "\"";

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"ganalytics\" : {\"settings\" : {\"enable\" : \"1\"," +
			                jsonSource + "," + jsonMedium + "," + jsonTerm + "," + jsonContent + "," + jsonCampaign + "}}}}",
				json);
		}

		[Test]
		public void EnableGravatar()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.EnableGravatar();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"gravatar\" : {\"settings\" : {\"enable\" : \"1\"}}}}", json);
		}

		[Test]
		public void EnableOpenTracking()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.EnableOpenTracking();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"opentrack\" : {\"settings\" : {\"enable\" : \"1\"}}}}", json);
		}

		[Test]
		public void EnableSpamCheck()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			var score = 5;
			var url = "http://www.example.com";
			sendgrid.EnableSpamCheck(score, url);

			var json = header.JsonString();
			Assert.AreEqual(
				"{\"filters\" : {\"spamcheck\" : {\"settings\" : {\"enable\" : \"1\",\"maxscore\" : \"5\",\"url\" : \"http:\\/\\/www.example.com\"}}}}",
				json);
		}

		[Test]
		public void EnableTemplate()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);
			var html = "<% hadhdhd %>";

			var escHtml = "<% hadhdhd %>";
			sendgrid.EnableTemplate(html);

			var json = header.JsonString();
			Assert.AreEqual(
				"{\"filters\" : {\"template\" : {\"settings\" : {\"enable\" : \"1\",\"text\\/html\" : \"" + escHtml + "\"}}}}", json);

			escHtml = "bad";
			Assert.Throws<Exception>(() => sendgrid.EnableTemplate(escHtml));
		}

		[Test]
		public void EnableUnsubscribe()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			var text = "<% %>";
			var html = "<% name %>";

			var jsonText = "\"text\\/plain\" : \"" + text + "\"";
			var jsonHtml = "\"text\\/html\" : \"" + html + "\"";

			sendgrid.EnableUnsubscribe(text, html);

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"subscriptiontrack\" : {\"settings\" : {\"enable\" : \"1\"," +
			                jsonText + "," + jsonHtml + "}}}}", json);

			header = new Header();
			sendgrid = new SendGrid(header);

			var replace = "John";
			var jsonReplace = "\"replace\" : \"" + replace + "\"";

			sendgrid.EnableUnsubscribe(replace);

			json = header.JsonString();
			Assert.AreEqual(
				"{\"filters\" : {\"subscriptiontrack\" : {\"settings\" : {\"enable\" : \"1\"," + jsonReplace + "}}}}", json);

			text = "bad";
			html = "<% name %>";
			Assert.Throws<Exception>(() => sendgrid.EnableUnsubscribe(text, html));

			text = "<% %>";
			html = "bad";
			Assert.Throws<Exception>(() => sendgrid.EnableUnsubscribe(text, html));
		}

		[Test]
		public void TestDisableGravatar()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableGravatar();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"gravatar\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}

		[Test]
		public void TestDisableOpenTracking()
		{
			var header = new Header();
			var sendgrid = new SendGrid(header);

			sendgrid.DisableOpenTracking();

			var json = header.JsonString();
			Assert.AreEqual("{\"filters\" : {\"opentrack\" : {\"settings\" : {\"enable\" : \"0\"}}}}", json);
		}
	}
}