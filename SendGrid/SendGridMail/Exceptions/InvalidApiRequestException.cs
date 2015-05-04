using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Exceptions
{
	[Serializable] 
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

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            info.AddValue("InvalidApiRequestException.ResponseStatusCode", this.ResponseStatusCode);
            info.AddValue("MyException.Errors", this.Errors, typeof(IEnumerable<string>));
        }
	}
}
