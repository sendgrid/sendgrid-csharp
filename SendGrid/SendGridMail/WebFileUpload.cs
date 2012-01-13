using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using CodeScales.Http;
using CodeScales.Http.Common;
using CodeScales.Http.Entity;
using CodeScales.Http.Entity.Mime;
using CodeScales.Http.Methods;

namespace SendGridMail
{
    internal class WebFileUpload
    {
        private HttpWebRequest _request;
        private List<Attachment> _attachments;

        private String newline = "\r\n";

        private byte[] _boundaryBytes;

        private String _boundary;

        public WebFileUpload(HttpWebRequest request)
        {

        }

        public void SendAttachments()
        {
            HttpClient client = new HttpClient();
                       //https://sendgrid.com/api/mail.send
            var url = "http://sendgrid.com/api/mail.send.xml";
            var notUrl = "http://www.postbin.org/1hv8rbe";
            HttpPost postMethod = new HttpPost(new Uri(url));



            //UrlEncodedFormEntity formEntity = new UrlEncodedFormEntity(nameValuePairList, Encoding.UTF8);
            //postMethod.Entity = formEntity;



            MultipartEntity multipartEntity = new MultipartEntity();
            postMethod.Entity = multipartEntity;

            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "api_user", "cjbuchmann"));
            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "api_key", "gadget15"));
            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "to", "cj.buchmann@sendgrid.com"));
            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "from", "cj.buchmann@sendgrid.com"));
            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "subject", "Hello World HttpClient Test"));
            multipartEntity.AddBody(new StringBody(Encoding.UTF8, "text", "here is some awesome text"));



            FileInfo fileInfo = new FileInfo(@"D:\att_proj\2.JPG");
            FileBody fileBody = new FileBody("files[file1.jpg]", "myfile.jpg", fileInfo);

            multipartEntity.AddBody(fileBody);

            HttpResponse response = client.Execute(postMethod);

            Console.WriteLine("Response Code: " + response.ResponseCode);
            Console.WriteLine("Response Content: " + EntityUtils.ToString(response.Entity));

            Console.WriteLine("done");
        }

    /*public void TestAddAttachment(Attachment attachment)
        {
            WebClient myWebClient = new WebClient();
            NameValueCollection collection = new NameValueCollection();
            
            StreamReader sr = new StreamReader(attachment.ContentStream);
            var data = sr.ReadToEnd();
            byte[] bytes = new byte[attachment.ContentStream.Length];

            byte[] responseArray = myWebClient.UploadValues("https://sendgrid.com/api/mail.send.xml", collection);
            Console.WriteLine("\nResponse received was :\n{0}", Encoding.ASCII.GetString(responseArray));
        }*/

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

        /*public void SendAttachments()
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
        }*/
    }
}
