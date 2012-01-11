using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    public class TestTreeNode
    {
        [Test]
        public void TestAddToTree()
        {
            var test = new Header.FilterNode();
            test.AddSetting(new List<string>(), "foo");
            Assert.AreEqual("foo", test.GetLeaf(), "Get the leaf of the first node");

            test = new Header.FilterNode();
            test.AddSetting(new List<string>() { "foo" }, "bar");
            Assert.AreEqual("bar", test.GetSetting(new List<string>(){"foo"}), "Get the item in the first branch 'foo', make sure its set to 'bar'");

            test = new Header.FilterNode();
            test.AddSetting(new List<string>(){"foo"}, "bar");
            Assert.AreEqual("bar", test.GetSetting("foo"), "tests the convienence get setting function that omits the lists stuff...");

            test = new Header.FilterNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"), "tests a tree that is multiple branches deep");


            test = new Header.FilterNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            test.AddSetting(new List<string>() { "barfoo", "barbar", "barraz" }, "barfoobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"), "tests a tree that has multiple branches");
            Assert.AreEqual("barfoobar", test.GetSetting("barfoo", "barbar", "barraz"), "tests the other branch");
        }

        [Test]
        public void TestToJSON()
        {
            var test = new Header.FilterNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            
            var result = test.ToJson();
            Assert.AreEqual("{\"foo\":{\"bar\":{\"raz\":\"foobar\"}}}", result);

            test = new Header.FilterNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            test.AddSetting(new List<string>() { "barfoo", "barbar", "barraz" }, "barfoobar");
            
            result = test.ToJson();
            Assert.AreEqual("{\"foo\":{\"bar\":{\"raz\":\"foobar\"}},\"barfoo\":{\"barbar\":{\"barraz\":\"barfoobar\"}}}", result);
        }
    }
}
