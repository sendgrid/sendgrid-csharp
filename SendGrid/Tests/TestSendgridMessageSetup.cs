using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using Moq;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    public class TestSendgridMessageSetup
    {
        [Test]
        public void TestAddTo()
        {
            var foo = new Mock<IHeader>();

            var sg = new SendGrid(foo.Object);
            sg.AddTo("eric@sendgrid.com");
            Assert.AreEqual("eric@sendgrid.com", sg.To.First().ToString(), "Single To Address" );

            sg = new SendGrid(foo.Object);
            var strings = new String[2];
            strings[0] = "eric@sendgrid.com";
            strings[1] = "tyler@sendgrid.com";
            sg.AddTo(strings);
            Assert.AreEqual("eric@sendgrid.com", sg.To[0].ToString(), "Multiple To addresses, check first one");
            Assert.AreEqual("tyler@sendgrid.com", sg.To[1].ToString(), "Multiple To addresses, check second one");

            sg = new SendGrid(foo.Object);
            var a = new Dictionary<String, String>();
            a.Add("DisplayName", "Eric");
            var datastruct = new Dictionary<string, IDictionary<string, string>> {{"eric@sendgrid.com", a}};
            sg.AddTo(datastruct);
            Assert.AreEqual("Eric", sg.To.First().DisplayName, "Single address with args");
        }

        [Test]
        public void TestAddCc()
        {
            var foo = new Mock<IHeader>();

            var sg = new SendGrid(foo.Object);
            sg.AddCc("eric@sendgrid.com");
            Assert.AreEqual("eric@sendgrid.com", sg.Cc.First().ToString(), "Single CC Address");

            sg = new SendGrid(foo.Object);
            var strings = new String[2];
            strings[0] = "eric@sendgrid.com";
            strings[1] = "tyler@sendgrid.com";
            sg.AddCc(strings);
            Assert.AreEqual("eric@sendgrid.com", sg.Cc[0].ToString(), "Multiple CC addresses, check first one");
            Assert.AreEqual("tyler@sendgrid.com", sg.Cc[1].ToString(), "Multiple CC addresses, check second one");

            sg = new SendGrid(foo.Object);
            var a = new Dictionary<String, String>();
            a.Add("DisplayName", "Eric");
            var datastruct = new Dictionary<string, IDictionary<string, string>> { { "eric@sendgrid.com", a } };
            sg.AddCc(datastruct);
            Assert.AreEqual("Eric", sg.Cc.First().DisplayName, "Single CC address with args");
        }

        [Test]
        public void TestAddBcc()
        {
            var foo = new Mock<IHeader>();

            var sg = new SendGrid(foo.Object);
            sg.AddBcc("eric@sendgrid.com");
            Assert.AreEqual("eric@sendgrid.com", sg.Bcc.First().ToString(), "Single Bcc Address");

            sg = new SendGrid(foo.Object);
            var strings = new String[2];
            strings[0] = "eric@sendgrid.com";
            strings[1] = "tyler@sendgrid.com";
            sg.AddBcc(strings);
            Assert.AreEqual("eric@sendgrid.com", sg.Bcc[0].ToString(), "Multiple addresses, check first one");
            Assert.AreEqual("tyler@sendgrid.com", sg.Bcc[1].ToString(), "Multiple addresses, check second one");

            sg = new SendGrid(foo.Object);
            var a = new Dictionary<String, String>();
            a.Add("DisplayName", "Eric");
            var datastruct = new Dictionary<string, IDictionary<string, string>> { { "eric@sendgrid.com", a } };
            sg.AddBcc(datastruct);
            Assert.AreEqual("Eric", sg.Bcc.First().DisplayName, "Single address with args");
        }

        [Test]
        public void TestSGHeader()
        {
            var foo = new Mock<IHeader>();
            var sg = new SendGrid(foo.Object);

            sg.Subject = "New Test Subject";
            Assert.AreEqual("New Test Subject", sg.Subject, "Subject set ok");
            sg.Subject = null;
            Assert.AreEqual("New Test Subject", sg.Subject, "null subject does not overide previous subject");
        }

        /*
        [Test]
        public void TestAddSubVal()
        {
            var header = new Header();
            var sg = new SendGrid(header);

            var datastruct = new String[2];
            datastruct[0] = "eric";
            datastruct[1] = "tyler";

            sg.AddSubVal("-name-", datastruct);
            Assert.AreEqual("test", sg.Header);
        }
        */


        [Test]
        public void TestGetRcpts()
        {
            var foo = new Mock<IHeader>();
            var sg = new SendGrid(foo.Object);

            sg.AddTo("eric@sendgrid.com");
            sg.AddCc("tyler@sendgrid.com");
            sg.AddBcc("cj@sendgrid.com");
            sg.AddBcc("foo@sendgrid.com");

            var rcpts = sg.GetRecipients();
            Assert.AreEqual("eric@sendgrid.com", rcpts.First(), "getRecipients check To");
            Assert.AreEqual("tyler@sendgrid.com", rcpts.Skip(1).First(), "getRecipients check Cc");
            Assert.AreEqual("cj@sendgrid.com", rcpts.Skip(2).First(), "getRecipients check Bcc");
            Assert.AreEqual("foo@sendgrid.com", rcpts.Skip(3).First(), "getRecipients check Bcc x2");
        }

        [Test]
        public void TestAddAttachment()
        {
            var foo = new Mock<IHeader>();
            var sg = new SendGrid(foo.Object);

            var data = new Attachment("pnunit.framework.dll", MediaTypeNames.Application.Octet);
            sg.AddAttachment("pnunit.framework.dll");
            sg.AddAttachment("pnunit.framework.dll");
            Assert.AreEqual(data.ContentStream, sg.Attachments.First().ContentStream, "Attach via path");
            Assert.AreEqual(data.ContentStream, sg.Attachments.Skip(1).First().ContentStream, "Attach via path x2");

            sg = new SendGrid(foo.Object);
            sg.AddAttachment(data);
            sg.AddAttachment(data);
            Assert.AreEqual(data.ContentStream, sg.Attachments.First().ContentStream, "Attach via attachment");
            Assert.AreEqual(data.ContentStream, sg.Attachments.Skip(1).First().ContentStream, "Attach via attachment x2");

            sg = new SendGrid(foo.Object);
            sg.AddAttachment(data.ContentStream, data.ContentType);
            sg.AddAttachment(data.ContentStream, data.ContentType);
            Assert.AreEqual(data.ContentStream, sg.Attachments.First().ContentStream, "Attach via stream");
            Assert.AreEqual(data.ContentStream, sg.Attachments.Skip(1).First().ContentStream, "Attach via stream x2");
        }
    }
}
