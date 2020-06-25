using Newtonsoft.Json.Linq;
using SendGrid.Helpers.Errors.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace SendGrid.Helpers.Errors
{
    /// <summary>
    /// Error handler for requests.
    /// </summary>
    public class ErrorHandler
    {
        /// <summary>
        /// Throw the exception based on HttpResponseMessage
        /// </summary>
        /// <param name="message">Response Message from API</param>
        /// <returns>Return the exception. Obs: Exceptions from an Async Void Method Can’t Be Caught with Catch </returns>
        public static async Task ThrowException(HttpResponseMessage message)
        {
            var errorMessage = await ErrorHandler.GetErrorMessage(message).ConfigureAwait(false);

            var errorStatusCode = (int)message.StatusCode;

            switch (errorStatusCode)
            {
                // 400 - BAD REQUEST
                case 400:
                    throw new BadRequestException(errorMessage);

                // 401 - UNAUTHORIZED
                case 401:
                    throw new UnauthorizedException(errorMessage);

                // 403 - FORBIDDEN
                case 403:
                    throw new ForbiddenException(errorMessage);

                // 404 - NOT FOUND
                case 404:
                    throw new NotFoundException(errorMessage);

                // 405 - METHOD NOT ALLOWED
                case 405:
                    throw new MethodNotAllowedException(errorMessage);

                // 413 - PAYLOAD TOO LARGE
                case 413:
                    throw new PayloadTooLargeException(errorMessage);

                // 415 - UNSUPPORTED MEDIA TYPE
                case 415:
                    throw new UnsupportedMediaTypeException(errorMessage);

                // 429 - TOO MANY REQUESTS
                case 429:
                    throw new TooManyRequestsException(errorMessage);

                // 500 - SERVER UNAVAILABLE
                case 500:
                    throw new ServerUnavailableException(errorMessage);

                // 503 - SERVICE NOT AVAILABLE
                case 503:
                    throw new ServiceNotAvailableException(errorMessage);
            }

            // 4xx - Error with the request
            if (errorStatusCode >= 400 && errorStatusCode < 500)
            {
                throw new RequestErrorException(errorMessage);
            }

            // 5xx - Error made by SendGrid
            if (errorStatusCode >= 500)
            {
                throw new SendGridInternalException(errorMessage);
            }

            throw new BadRequestException(errorMessage);
        }

        /// <summary>
        /// Get error based on Response from SendGrid API
        /// Method taken from the StrongGrid project (https://github.com/Jericho/StrongGrid) with some minor changes. Thanks Jericho (https://github.com/Jericho)
        /// </summary>
        /// <param name="message">Response Message from API</param>
        /// <returns>Return string with the error Status Code and the Message</returns>
        private static async Task<string> GetErrorMessage(HttpResponseMessage message)
        {
            var errorStatusCode = (int)message.StatusCode;
            var errorReasonPhrase = message.ReasonPhrase;

            string errorValue = null;
            string fieldValue = null;
            string helpValue = null;

            if (message.Content != null)
            {
                var responseContent = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                if (!string.IsNullOrEmpty(responseContent))
                {
                    try
                    {
                        // Check for the presence of property called 'errors'
                        var jObject = JObject.Parse(responseContent);
                        var errorsArray = (JArray)jObject["errors"];
                        if (errorsArray != null && errorsArray.Count > 0)
                        {
                            // Get the first error message
                            errorValue = errorsArray[0]["message"].Value<string>();

                            // Check for the presence of property called 'field'
                            if (errorsArray[0]["field"] != null)
                            {
                                fieldValue = errorsArray[0]["field"].Value<string>();
                            }

                            // Check for the presence of property called 'help'
                            if (errorsArray[0]["help"] != null)
                            {
                                helpValue = errorsArray[0]["help"].Value<string>();
                            }
                        }
                        else
                        {
                            // Check for the presence of property called 'error'
                            var errorProperty = jObject["error"];
                            if (errorProperty != null)
                            {
                                errorValue = errorProperty.Value<string>();
                            }
                        }
                    }
                    catch
                    {
                        // Intentionally ignore parsing errors to return default error message
                    }
                }
            }

            SendGridErrorResponse errorResponse = new SendGridErrorResponse
            {
                ErrorHttpStatusCode = errorStatusCode,
                ErrorReasonPhrase = errorReasonPhrase,
                SendGridErrorMessage = errorValue,
                FieldWithError = fieldValue,
                HelpLink = helpValue
            };

            return Newtonsoft.Json.JsonConvert.SerializeObject(errorResponse);
        }
    }
}
