using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace SendGridMail
{
    class WebFileUpload
    {
        private HttpWebRequest _request;
        private List<Attachment> _attachments;

        private String newline = "\r\n";

        private byte[] _boundaryBytes;

        private String _boundary;

        public WebFileUpload(HttpWebRequest request)
        {
            
        }

        public void TestAddAttachment(Attachment attachment)
        {
            WebClient myWebClient = new WebClient();
            NameValueCollection collection = new NameValueCollection();
            
            StreamReader sr = new StreamReader(attachment.ContentStream);
            var data = sr.ReadToEnd();
            byte[] bytes = new byte[attachment.ContentStream.Length];

            byte[] responseArray = myWebClient.UploadValues("https://sendgrid.com/api/mail.send.xml", collection);
            Console.WriteLine("\nResponse received was :\n{0}", Encoding.ASCII.GetString(responseArray));
        }

        public void AddAttachment(String filename)
        {
            _attachments.Add(new Attachment(filename));
        }

        public void AddAttachments(List<Attachment> attachments)
        {
            _attachments = attachments;
        }

        public List<Attachment> GetAttachments()
        {
            return _attachments;
        }

        public void SendAttachments()
        {
            WebResponse _response = null;

            try
            {
                _response = _request.GetResponse();

                //Stream stream = _response.GetResponseStream();
                //StreamReader reader = new StreamReader(stream);
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to send attachments :: " + ex.Message);
            }

            _response.Close();
        }
    }
}
