using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using Moq;
using NUnit.Framework;
using SendGrid;

namespace Tests
{
    [TestFixture]
    public class TestHeader
    {
        [Test]
        public void TestAddSubVal()
        {
            var test = new Header();
            test.AddSubVal("foo", new List<string>{"bar", "raz"});
            var result = test.AsJson();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"bar\", \"raz\"]}}", result);            
        }

        [Test]
        public void TestAddUniqueIdentifier()
        {
            var test = new Header();
            test.AddUniqueIdentifier(new Dictionary<string, string>(){{"foo", "bar"}});
            var result = test.AsJson();
            Assert.AreEqual("{\"unique_args\" : {\"foo\" : \"bar\"}}", result);
        }

        [Test]
        public void TestSetCategory()
        {
            var test = new Header();
            test.SetCategory("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"category\" : \"foo\"}", result);
        }

        [Test]
        public void TestEnable()
        {
            var test = new Header();
            test.Enable("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"1\"}}}}", result); 
        }

        [Test]
        public void TestDisable()
        {
            var test = new Header();
            test.Disable("foo");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"enable\" : \"0\"}}}}", result);
        }

        [Test]
        public void TestAddFilterSetting()
        {
            var test = new Header();
            test.AddFilterSetting("foo", new List<string> { "a", "b" }, "bar");
            var result = test.AsJson();
            Assert.AreEqual("{\"filters\" : {\"foo\" : {\"settings\" : {\"a\" : {\"b\" : \"bar\"}}}}}", result);
            
        }

        [Test]
        public void TestAddHeader()
        {
            var test = new Header();
            test.AddSubVal("foo", new List<string> { "a", "b" });
            var mime = new MailMessage();
            test.AddHeader(mime);
            var result = mime.Headers.Get("x-smtpapi");
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"a\", \"b\"]}}", result);
        }

        [Test]
        public void TestAsJson()
        {
            var test = new Header();
            var result = test.AsJson();
            Assert.AreEqual("", result);

            test = new Header();
            test.AddSubVal("foo", new List<string>{"a", "b"});
            result = test.AsJson();
            Assert.AreEqual("{\"sub\" : {\"foo\" : [\"a\", \"b\"]}}", result);
        }
    }
}
