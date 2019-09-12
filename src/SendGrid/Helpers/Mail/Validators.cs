using System.Collections.Generic;
using System.Linq;
using SendGrid.Helpers.Mail.Model;

namespace SendGrid.Helpers.Mail
{
	/// <summary>
	/// This class is an extension method to check if the parameters is null or empty
	/// </summary>
	public static class Validators
	{
		/// <summary>
		/// Checks if the EmailAddress object is valid
		/// </summary>
		/// <param name="emailAddress"></param>
		/// <returns></returns>
		public static bool IsValid(this EmailAddress emailAddress)
		{
			return !string.IsNullOrEmpty(emailAddress.Email) && !string.IsNullOrEmpty(emailAddress.Name);
		}

		/// <summary>
		/// Checks if the List of EmailAddress object is valid
		/// </summary>
		/// <param name="addresses"></param>
		/// <returns></returns>
		public static bool IsValid(this List<EmailAddress> addresses)
		{
			return addresses != null && !addresses.Any();
		}
	}
}
