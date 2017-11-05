namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for mailbox_providers
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    internal class MailboxProviders : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailboxProviders"/> class.
        /// </summary>
        public MailboxProviders()
            : base("mailbox_providers.stats", "read")
        {
            this.IsAdminOnly = true;
        }
    }
}
