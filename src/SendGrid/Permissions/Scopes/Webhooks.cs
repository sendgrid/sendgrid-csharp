namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for user.webhooks
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    internal class Webhooks : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhooks"/> class.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        public Webhooks()
            : base("webhooks")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("event")
                {
                    AllowedOptions = new string[0],
                    SubScopes = new[]
                    {
                         new SendGridPermissionScope("settings", "read", "update"),
                         new SendGridPermissionScope("test", "create", "read", "update"),
                    }
                },
                new SendGridPermissionScope("parse")
                    {
                        AllowedOptions = new string[0],
                        SubScopes = new[]
                    {
                            new SendGridPermissionScope("settings"),
                            new SendGridPermissionScope("stats", ScopeOptions.ReadOnly),
                    }
                }
            };
        }
    }
}
