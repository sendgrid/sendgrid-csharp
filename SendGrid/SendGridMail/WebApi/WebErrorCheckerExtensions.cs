namespace SendGridMail.WebApi
{
    public static class WebErrorCheckerExtensions
    {
        public static void ThrowOnSendGridError(this System.Xml.Linq.XDocument doc)
        {
            WebErrorChecker checker = new WebErrorChecker();
            checker.ThrowOnSendGridError(doc);
        }
    }
}