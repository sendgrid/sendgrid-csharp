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

        public void testNoAttach(Attachment attachment)
        {
            WebClient myWebClient = new WebClient();
            NameValueCollection collection = new NameValueCollection();
            
            StreamReader sr = new StreamReader(attachment.ContentStream);
            var data = sr.ReadToEnd();
            //byte[] bytes = new byte[attachment.ContentStream.Length];
            //attachment.ContentStream.Position = 0;

            //attachment.ContentStream.Read(bytes, 0, (int) attachment.ContentStream.Length);

            //String data = Encoding.Default.GetString(bytes);

            collection.Add("api_user", "cjbuchmann");
            collection.Add("api_key", "Gadget_15");
            collection.Add("from", "cj.buchmann@sendgrid.com");
            collection.Add("to", "cj.buchmann@sendgrid.com");
            collection.Add("subject", "hello world test");
            collection.Add("text", "hello world plain text");
            collection.Add("files[file1.jpg]", data);

            byte[] responseArray = myWebClient.UploadValues("https://sendgrid.com/api/mail.send.xml", collection);
            Console.WriteLine("\nResponse received was :\n{0}", Encoding.ASCII.GetString(responseArray));
            Console.WriteLine("DONE IN HERE!");
        }

        public static string UploadFileEx(string uploadfile, string url,
    string fileFormName, string contenttype, NameValueCollection querystring)
        {
            if ((fileFormName == null) ||
                (fileFormName.Length == 0))
            {
                fileFormName = "file";
            }

            if ((contenttype == null) ||
                (contenttype.Length == 0))
            {
                contenttype = "application/octet-stream";
            }


            string postdata;
            postdata = "?";
            if (querystring != null)
            {
                foreach (string key in querystring.Keys)
                {
                    postdata += key + "=" + querystring.Get(key) + "&";
                }
            }
            Uri uri = new Uri(url + postdata);

            Console.WriteLine("data is "+uri.AbsoluteUri);


            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);

            Console.WriteLine(webrequest.RequestUri);

            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webrequest.Method = "POST";


            // Build up the post message header

            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append(fileFormName);
            sb.Append("\"; filename=\"");
            sb.Append(Path.GetFileName(uploadfile));
            sb.Append("\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(contenttype);
            sb.Append("\r\n");
            sb.Append("\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            // Build the trailing boundary string as a byte array

            // ensuring the boundary appears on a line by itself

            byte[] boundaryBytes =
                   Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            FileStream fileStream = new FileStream(uploadfile,
                                        FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length +
                                                   boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // Write out our post header

            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // Write out the file contents

            byte[] buffer = new Byte[checked((uint)Math.Min(4096,
                                     (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // Write out the trailing boundary

            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            return sr.ReadToEnd();
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
            StreamAttachments();

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

        private void StreamAttachments()
        {
            Stream rs = _request.GetRequestStream();
            string formdataTemplate = "Content-Disposition: form-data; name=\"files[{0}]\"; filename=\"{1}\"";

            Console.Write("\r\n\r\n\r\n\r\n");
            Console.WriteLine("Request : "+_request.RequestUri);
            Console.Write(_request.Headers.ToString());

            Attachment attachment = _attachments.First();

            rs.Write(_boundaryBytes, 0, _boundaryBytes.Length);
            String formitem = String.Format(formdataTemplate, attachment.Name, attachment.ToString());
            byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
            rs.Write(formitembytes, 0, formitembytes.Length);

            rs.Write(_boundaryBytes, 0, _boundaryBytes.Length);
        }


    }
}
