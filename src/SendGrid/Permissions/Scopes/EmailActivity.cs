namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for email_activity
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    internal class EmailActivity : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmailActivity"/> class.
        /// </summary>
        public EmailActivity()
            : base("email_activity", "read")
        {
        }
    }
}
