namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for user.webhooks
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    internal class Webhook : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhook"/> class.
        /// </summary>
        public Webhook()
            : base("Webhook")
        {
            this.Scopes = new[]
            {
                "user.webhooks.event.settings.read",
                "user.webhooks.event.settings.update",
                "user.webhooks.event.test.create",
                "user.webhooks.event.test.read",
                "user.webhooks.event.test.update",
                "user.webhooks.parse.settings.create",
                "user.webhooks.parse.settings.delete",
                "user.webhooks.parse.settings.read",
                "user.webhooks.parse.settings.update",
                "user.webhooks.parse.stats.read"
            };
        }
    }
}
