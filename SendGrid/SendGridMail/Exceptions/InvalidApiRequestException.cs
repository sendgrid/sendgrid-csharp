using System;
using System.Net;

namespace Exceptions
{
	public class InvalidApiRequestException : Exception
	{
		public InvalidApiRequestException(HttpStatusCode httpStatusCode, string[] errors, string httpResponsePhrase)
			: base(httpResponsePhrase + " Check `Errors` for a list of errors returned by the API.")
		{
			ResponseStatusCode = httpStatusCode;
			Errors = errors;
		}
		
		public String[] Errors { get; set; }

		public HttpStatusCode ResponseStatusCode { get; private set; }
	}
}
