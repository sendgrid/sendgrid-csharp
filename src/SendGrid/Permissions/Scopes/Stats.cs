namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for stats
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Stats : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stats"/> class.
        /// </summary>
        public Stats()
            : base("Stats")
        {
            this.Scopes = new [] {
                "email_activity.read",
                "stats.read",
                "stats.global.read",
                "browsers.stats.read",
                "devices.stats.read",
                "geo.stats.read",
                "mailbox_providers.stats.read",
                "clients.desktop.stats.read",
                "clients.phone.stats.read",
                "clients.stats.read",
                "clients.tablet.stats.read",
                "clients.webmail.stats.read"
            };
        }
    }
}
