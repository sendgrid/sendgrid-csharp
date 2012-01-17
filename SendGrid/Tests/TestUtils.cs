using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    public class TestUtils
    {
        [Test]
        public void TestSerialize()
        {
            var testcase = "foo";
            String result = Utils.Serialize(testcase);
            Assert.AreEqual("\"foo\"", result);

            var testcase2 = 1;
            result = Utils.Serialize(testcase2);
            Assert.AreEqual("1", result);
        }

        [Test]
        public void TestSerializeDictionary()
        {
            var test = new Dictionary<string, string>
                           {
                               {"a", "b"}, 
                               {"c", "d/e"}
                           };
            var result = Utils.SerializeDictionary(test);
            var expected = "{\"a\":\"b\",\"c\":\"d\\/e\"}";
            Assert.AreEqual(expected, result);
        }
    }
}
