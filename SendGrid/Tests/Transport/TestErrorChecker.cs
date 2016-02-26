namespace Transport
{
    #region Using Directives

    using System.Net;
    using System.Net.Http;
    using Exceptions;
    using NUnit.Framework;
    using SendGrid;

    #endregion

    [TestFixture]
    public class TestErrorChecker
    {
        private const string BadUsernameOrPasswordResponseMessage =
            "<result><message>error</message><errors><error>Bad username / password</error></errors></result>";

        [Test]
        [ExpectedException(typeof (InvalidApiRequestException))]
        public void WhenHttpResponseContainsBadUserErrorItIsDetectedAndAInvalidApiRequestIsThrown()
        {
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(BadUsernameOrPasswordResponseMessage)
            };

            ErrorChecker.CheckForErrors(response);
        }
    }
}