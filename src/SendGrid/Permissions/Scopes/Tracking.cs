namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for tracking_settings
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Tracking : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tracking"/> class.
        /// </summary>
        public Tracking()
            : base("Tracking")
        {
            this.Scopes = new[]
            {
                "tracking_settings.click.read",
                "tracking_settings.click.update",
                "tracking_settings.google_analytics.read",
                "tracking_settings.google_analytics.update",
                "tracking_settings.open.read",
                "tracking_settings.open.update",
                "tracking_settings.read",
                "tracking_settings.subscription.read",
                "tracking_settings.subscription.update"
            };
        }
    }
}
