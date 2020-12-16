namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for mail_settings
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class MailSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MailSettings"/> class.
        /// </summary>
        public MailSettings()
            : base("Mail Settings")
        {
            this.Scopes = new[]
            {
                "mail_settings.address_whitelist.read",
                "mail_settings.address_whitelist.update",
                "mail_settings.bcc.read",
                "mail_settings.bcc.update",
                "mail_settings.bounce_purge.read",
                "mail_settings.bounce_purge.update",
                "mail_settings.footer.read",
                "mail_settings.footer.update",
                "mail_settings.forward_bounce.read",
                "mail_settings.forward_bounce.update",
                "mail_settings.forward_spam.read",
                "mail_settings.forward_spam.update",
                "mail_settings.plain_content.read",
                "mail_settings.plain_content.update",
                "mail_settings.read",
                "mail_settings.spam_check.read",
                "mail_settings.spam_check.update",
                "mail_settings.template.read",
                "mail_settings.template.update"
            };
        }
    }
}
