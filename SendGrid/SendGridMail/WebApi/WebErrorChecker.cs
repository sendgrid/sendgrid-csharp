using System;
using System.Linq;

namespace SendGridMail.WebApi
{
    public class WebErrorChecker
    {
        public void ThrowOnSendGridError(System.Xml.Linq.XDocument doc)
        {
            var resultNode = doc.Descendants("result").FirstOrDefault();
            if (resultNode != null)
            {
                var messageNodes = resultNode.Descendants("message");
                if (messageNodes == null && messageNodes.Count() == 0)
                {
                    return;
                }

                throw new ApplicationException(resultNode.Value); //concatenate all message node value text into one exception message.
            }
            resultNode = doc.Descendants("errors").FirstOrDefault();
            if (resultNode != null)
            {
                var errorNodes = resultNode.Descendants("error");
                if (errorNodes == null && errorNodes.Count() == 0)
                {
                    return;
                }

                throw new ApplicationException(resultNode.Value); //concatenate all message node value text into one exception message.
            }
        }
    }
}