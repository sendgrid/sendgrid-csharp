namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for mail_settings
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class MailSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSettings"/> class.
        /// </summary>
        public MailSettings()
            : base("mail_settings", "read")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("address_whitelist", "read", "update"),
                new SendGridPermissionScope("bcc", "read", "update"),
                new SendGridPermissionScope("bounce_purge", "read", "update"),
                new SendGridPermissionScope("footer", "read", "update"),
                new SendGridPermissionScope("forward_bounce", "read", "update"),
                new SendGridPermissionScope("forward_spam", "read", "update"),
                new SendGridPermissionScope("plain_content", "read", "update"),
                new SendGridPermissionScope("spam_check", "read", "update"),
                new SendGridPermissionScope("template", "read", "update")
            };
        }
    }
}
