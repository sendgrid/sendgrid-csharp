using System;
using System.Collections.Generic;
using System.Linq;

namespace SendGrid.Helpers.Mail
{
	public static class Validators
	{
		public static bool IsValid(this EmailAddress emailAddress)
		{
			return !string.IsNullOrEmpty(emailAddress.Email) && !string.IsNullOrEmpty(emailAddress.Name);
		}

		public static bool IsValid(this List<EmailAddress> addresses)
		{
			return addresses != null && !addresses.Any();
		}
	}
}
