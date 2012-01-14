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
            Assert.AreEqual("1", Utils.Serialize(1));
            Assert.AreEqual("\"\\\"foo\\\"\"", Utils.Serialize("\"foo\""));
        }
    }
}
