using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SendGrid;

namespace Tests
{
    [TestFixture]
    public class TestStreamedFileBody
    {
        [Test]
        public void TestGetContent()
        {
            var name = "foo";
            var file = "bar";
            var boundary = "raz";

            var memoryStream = new MemoryStream();
            var stream = new StreamWriter(memoryStream);
            stream.Write(file);
            stream.Flush();
            stream.Close();
            
            var streamedFile = new StreamedFileBody(memoryStream, name);
            var bytes = streamedFile.GetContent(boundary);
            var result = System.Text.Encoding.ASCII.GetString(bytes);
            var expected = "--raz\r\nContent-Disposition: form-data; name=\"files[foo]\"; filename=\"foo\"\r\nContent-Type: image/png\r\n\r\nbar\r\n";
            Assert.AreEqual(expected, result, "message formated correctly");
            
        }
    }
}
