namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for alerts
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Alerts : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Alerts"/> class.
        /// </summary>
        public Alerts()
            : base("alerts")
        {
        }
    }
}
