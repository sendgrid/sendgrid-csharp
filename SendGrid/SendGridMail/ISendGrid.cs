using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using SendGrid.SmtpApi;

namespace SendGrid
{
	/// <summary>
	///     Represents the basic set of functions that will be called by the user
	///     includes basic message data manipulation and filter settings
	/// </summary>
	public interface ISendGrid
	{
		#region Properties

		MailAddress From { get; set; }
		MailAddress[] To { get; set; }
		MailAddress[] Cc { get; }
		MailAddress[] Bcc { get; }
		MailAddress[] ReplyTo { get; set; }
		Dictionary<String, MemoryStream> StreamedAttachments { get; set; }
		String[] Attachments { get; set; }
		String Subject { get; set; }
		Dictionary<String, String> Headers { get; set; }
		IHeader Header { get; set; }
		String Html { get; set; }
		String Text { get; set; }

		#endregion

		#region Interface for ITransport

		/// <summary>
		///     Used by the Transport object to create a MIME for SMTP
		/// </summary>
		/// <returns>MIME to be sent</returns>
		MailMessage CreateMimeMessage();

		#endregion

		#region Methods for setting data

		/// <summary>
		///     Add to the 'To' address.
		/// </summary>
		/// <param name="address">single string eg. 'you@company.com'</param>
		void AddTo(String address);

		/// <summary>
		///     Add to the 'To' address.
		/// </summary>
		/// <param name="addresses">list of email addresses as strings</param>
		void AddTo(IEnumerable<String> addresses);

		/// <summary>
		///     Add to the 'To' address.
		/// </summary>
		/// <param name="addresssInfo">
		///     the dictionary keys are the email addresses, which points to a dictionary of
		///     key substitutionValues pairs mapping to other address codes, such as { foo@bar.com => { 'DisplayName' => 'Mr Foo' }
		///     }
		/// </param>
		void AddTo(IDictionary<String, IDictionary<String, String>> addresssInfo);

		/// <summary>
		///     Defines a mapping between a replacement string in the text of the message to a list of
		///     substitution values to be used, one per each recipient, in the same order as the recipients were added.
		/// </summary>
		/// <param name="replacementTag">the string in the email that you'll replace eg. '-name-'</param>
		/// <param name="substitutionValues">
		///     a list of values that will be substituted in for the replacementTag, one for each
		///     recipient
		/// </param>
		void AddSubstitution(String replacementTag, List<String> substitutionValues);

		/// <summary>
		///     This adds parameters and values that will be bassed back through SendGrid's
		///     Event API if an event notification is triggered by this email.
		/// </summary>
		/// <param name="identifiers">parameter substitutionValues pairs to be passed back on event notification</param>
		void AddUniqueArgs(IDictionary<String, String> identifiers);

		/// <summary>
		///     This sets the category for this email.  Statistics are stored on a per category
		///     basis, so this can be useful for tracking on a per group basis.
		/// </summary>
		/// <param name="category">categories applied to the message</param>
		void SetCategory(String category);

		/// <summary>
		///     This sets the categories for this email.  Statistics are stored on a per category
		///     basis, so this can be useful for tracking on a per group basis.
		/// </summary>
		/// <param name="categories">categories applied to the message</param>
		void SetCategories(IEnumerable<String> categories);

		/// <summary>
		///     Add an attachment to the message.
		/// </summary>
		/// <param name="filePath">a fully qualified file path as a string</param>
		void AddAttachment(String filePath);

		/// <summary>
		///     Add a stream as an attachment to the message
		/// </summary>
		/// <param name="stream">Stream of file to be attached</param>
		/// <param name="name">Name of file to be attached</param>
		void AddAttachment(Stream stream, String name);

		/// <summary>
		///     GetRecipients returns a list of all the recepients by retrieving the to, cc, and bcc lists.
		/// </summary>
		/// <returns></returns>
		IEnumerable<String> GetRecipients();

		/// <summary>
		///     Add custom headers to the message
		/// </summary>
		/// <param name="headers">key substitutionValues pairs</param>
		void AddHeaders(IDictionary<String, String> headers);

		/// <summary>
		///     Gets the list of embedded images
		/// </summary>
		/// <returns></returns>
		IDictionary<string, string> GetEmbeddedImages();

		#endregion

		#region SMTP API Functions

		/// <summary>
		///     Disable the gravatar app
		/// </summary>
		void DisableGravatar();

		/// <summary>
		///     Disable the open tracking app
		/// </summary>
		void DisableOpenTracking();

		/// <summary>
		///     Disable the click tracking app
		/// </summary>
		void DisableClickTracking();

		/// <summary>
		///     Disable the spam check
		/// </summary>
		void DisableSpamCheck();

		/// <summary>
		///     Disable the unsubscribe app
		/// </summary>
		void DisableUnsubscribe();

		/// <summary>
		///     Disable the footer app
		/// </summary>
		void DisableFooter();

		/// <summary>
		///     Disable the Google Analytics app
		/// </summary>
		void DisableGoogleAnalytics();

		/// <summary>
		///     Disable the templates app
		/// </summary>
		void DisableTemplate();

		/// <summary>
		///     Disable Bcc app
		/// </summary>
		void DisableBcc();

		/// <summary>
		///     Disable the Bypass List Management app
		/// </summary>
		void DisableBypassListManagement();

		/// <summary>
		///     Inserts the gravatar image of the sender to the bottom of the message
		/// </summary>
		void EnableGravatar();

		/// <summary>
		///     Adds an invisible image to the end of the email which can track e-mail opens.
		/// </summary>
		void EnableOpenTracking();

		/// <summary>
		///     Causes all links to be overwritten, shortened, and pointed to SendGrid's servers so clicks will be tracked.
		/// </summary>
		/// <param name="includePlainText">true if links found in plain text portions of the message are to be overwritten</param>
		void EnableClickTracking(bool includePlainText = false);

		/// <summary>
		///     Provides notification when emails are deteched that exceed a predefined spam threshold.
		/// </summary>
		/// <param name="score">
		///     Emails with a SpamAssassin score over this substitutionValues will be considered spam and not be
		///     delivered.
		/// </param>
		/// <param name="url">SendGridMessage will send an HTTP POST request to this url when a message is detected as spam</param>
		void EnableSpamCheck(int score = 5, String url = null);

		/// <summary>
		///     Allow's SendGridMessage to manage unsubscribes and ensure these users don't get future emails from the sender
		/// </summary>
		/// <param name="text">String for the plain text email body showing what you want the message to look like.</param>
		/// <param name="html">String for the HTML email body showing what you want the message to look like.</param>
		void EnableUnsubscribe(String text, String html);

		/// <summary>
		///     Allow's SendGridMessage to manage unsubscribes and ensure these users don't get future emails from the sender
		/// </summary>
		/// <param name="replace">Tag in the message body to be replaced with the unsubscribe link and message</param>
		void EnableUnsubscribe(String replace);

		/// <summary>
		///     Attaches a message at the footer of the email
		/// </summary>
		/// <param name="text">Message for the plain text body of the email</param>
		/// <param name="html">Message for the HTML body of the email</param>
		void EnableFooter(String text = null, String html = null);

		/// <summary>
		///     Re-writes links to integrate with Google Analytics
		/// </summary>
		/// <param name="source">Name of the referrer source (e.g. Google, SomeDomain.com, NewsletterA)</param>
		/// <param name="medium">Name of the marketing medium (e.g. Email)</param>
		/// <param name="term">Identify paid keywords</param>
		/// <param name="content">Use to differentiate ads</param>
		/// <param name="campaign">Name of the campaign</param>
		void EnableGoogleAnalytics(String source, String medium, String term, String content = null, String campaign = null);

		/// <summary>
		///     Wraps an HTML template around your email content.
		/// </summary>
		/// <param name="html">HTML that your emails will be wrapped in, containing a body replacementTag.</param>
		void EnableTemplate(String html = null);

		/// <summary>
		///     Automatically sends a blind carbon copy to an address for every e-mail sent, without
		///     adding that address to the header.
		/// </summary>
		/// <param name="email">A single email recipient</param>
		void EnableBcc(String email = null);

		/// <summary>
		///     Enabing this app will bypass the normal unsubscribe / bounce / spam report checks
		///     and queue the e-mail for delivery.
		/// </summary>
		void EnableBypassListManagement();

		#endregion
	}
}