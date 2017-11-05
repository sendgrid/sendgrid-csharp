namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for tracking_settings
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Tracking : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Tracking"/> class.
        /// </summary>
        public Tracking()
            : base("tracking_settings", "read")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("click", "read", "update"),
                new SendGridPermissionScope("google_analytics", "read", "update"),
                new SendGridPermissionScope("open", "read", "update"),
                new SendGridPermissionScope("subscription", "read", "update"),
            };
        }
    }
}
