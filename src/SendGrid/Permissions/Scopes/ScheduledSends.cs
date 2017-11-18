namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for user.scheduled_sends
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class ScheduledSends : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScheduledSends"/> class.
        /// </summary>
        public ScheduledSends()
            : base("user.scheduled_sends")
        {
        }
    }
}
