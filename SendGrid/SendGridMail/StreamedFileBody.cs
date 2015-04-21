using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SendGridMail
{
    public class StreamedFileBody 
    {
        private string _name;
        private string _filename;
        private byte[] _content;

        public StreamedFileBody(MemoryStream stream, String name)
        {
            if (stream == null) throw new ArgumentException("Invalid attachment stream");
            if (String.IsNullOrEmpty(name)) throw new ArgumentException("Invalid attachment name");

            _name = "files[" + Path.GetFileName(name) + "]";
            _filename = name;
            _content = stream.ToArray();
        }

        public byte[] GetContent(string boundry)
        {
            var bytes = new List<byte>();

            string paramBoundry = "--" + boundry + "\r\n";
            string stringParam = "Content-Disposition: form-data; name=\"" + _name + "\"; filename=\"" + _filename + "\"\r\n";
            string paramEnd = "Content-Type: image/png\r\n\r\n";
     
            bytes.AddRange(Encoding.ASCII.GetBytes(paramBoundry + stringParam + paramEnd));
            bytes.AddRange(_content);
            bytes.AddRange(Encoding.ASCII.GetBytes("\r\n"));
            return bytes.ToArray();
        }
    }
}
