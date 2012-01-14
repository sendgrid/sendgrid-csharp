using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    public class TestTreeNode
    {
        [Test]
        public void TestAddSetting()
        {
            var test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string>(), "foo");
            Assert.AreEqual("foo", test.GetLeaf(), "Get the leaf of the first node");

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string> { "foo" }, "bar");
            Assert.AreEqual("bar", test.GetSetting(new List<string>(){"foo"}), "Get the item in the first branch 'foo', make sure its set to 'bar'");

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string> {"foo"}, "bar");
            Assert.AreEqual("bar", test.GetSetting("foo"), "tests the convienence get setting function that omits the lists stuff...");

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string> { "foo", "bar", "raz" }, "foobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"), "tests a tree that is multiple branches deep");

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string> { "foo", "bar", "raz" }, "foobar");
            test.AddSetting(new List<string> { "barfoo", "barbar", "barraz" }, "barfoobar");
            Assert.AreEqual("foobar", test.GetSetting("foo", "bar", "raz"), "tests a tree that has multiple branches");
            Assert.AreEqual("barfoobar", test.GetSetting("barfoo", "barbar", "barraz"), "tests the other branch");

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string> { "foo" }, "bar");
            try
            {
                test.AddSetting(new List<string> {"foo", "raz"}, "blam");
                Assert.Fail("exception not thrown");
            }
            catch (ArgumentException ex)
            {
                Assert.AreEqual("Attempt to overwrite setting", ex.Message);
            }

        }

        [Test]
        public void TestToJSON()
        {
            var test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            
            var result = test.ToJson();
            Assert.AreEqual("{\"foo\" : {\"bar\" : {\"raz\" : \"foobar\"}}}", result);

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string>() { "foo", "bar", "raz" }, "foobar");
            test.AddSetting(new List<string>() { "barfoo", "barbar", "barraz" }, "barfoobar");
            
            result = test.ToJson();
            Assert.AreEqual("{\"foo\" : {\"bar\" : {\"raz\" : \"foobar\"}},\"barfoo\" : {\"barbar\" : {\"barraz\" : \"barfoobar\"}}}", result);

            test = new Header.HeaderSettingsNode();
            test.AddArray(new List<string>{"foo"}, new List<string>{"bar", "raz"});
            result = test.ToJson();
            Assert.AreEqual("{\"foo\" : [\"bar\", \"raz\"]}", result);

        }

        [Test]
        public void TestAddArray()
        {
            var test = new Header.HeaderSettingsNode();
            test.AddArray(new List<string>{"foo", "bar"}, new string[]{"raz", "blam"});
            var result = test.GetArray("foo", "bar");
            Assert.AreEqual(result.ToList()[0], "raz");
            Assert.AreEqual(result.ToList()[1], "blam");
        }

        [Test]
        public void TestIsEmpty()
        {
            var test = new Header.HeaderSettingsNode();
            Assert.IsTrue(test.IsEmpty());

            test = new Header.HeaderSettingsNode();
            test.AddSetting(new List<string>{"foo"}, "bar");
            Assert.IsFalse(test.IsEmpty());

            test = new Header.HeaderSettingsNode();
            test.AddArray(new List<string> { "raz" }, new List<string>{"blam"});
            Assert.IsFalse(test.IsEmpty());

        }
    }
}
