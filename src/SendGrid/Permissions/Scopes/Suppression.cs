namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for suppression
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Suppression : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Suppression"/> class.
        /// </summary>
        public Suppression()
            : base("suppression")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("bounces"),
                new SendGridPermissionScope("blocks"),
                new SendGridPermissionScope("invalid_emails"),
                new SendGridPermissionScope("spam_reports"),
                new SendGridPermissionScope("unsubscribes"),
            };
        }
    }
}
