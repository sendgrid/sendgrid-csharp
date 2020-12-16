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
            : base("Scheduled Sends")
        {
            this.Scopes = new[]
            {
                "user.scheduled_sends.create",
                "user.scheduled_sends.delete",
                "user.scheduled_sends.read",
                "user.scheduled_sends.update"
            };
        }
    }
}
