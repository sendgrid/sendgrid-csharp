using System.Text;
using NUnit.Framework;
using SendGridMail;

namespace Tests
{
    [TestFixture]
    public class TestJsonUtils
    {
        [Test]
        public void TestSerialize()
        {
            Assert.AreEqual("1", JsonUtils.Serialize(1));
            Assert.AreEqual("\"\\\"foo\\\"\"", JsonUtils.Serialize("\"foo\""));

            var arg = Encoding.UTF8.GetString(Encoding.ASCII.GetBytes("добры дзень"));
            var result = JsonUtils.Serialize(arg);
            System.Console.WriteLine(arg + " => " + result);
            Assert.AreEqual("", result);
        }
    }
}
