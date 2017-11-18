namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for user.webhooks
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    internal class Webhooks : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Webhooks"/> class.
        /// </summary>
        public Webhooks()
            : base("user.webhooks")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("event")
                {
                    AllowedOptions = ScopeOptions.None,
                    SubScopes = new[]
                    {
                         new SendGridPermissionScope("settings", "read", "update"),
                         new SendGridPermissionScope("test", "create", "read", "update"),
                    }
                },
                new SendGridPermissionScope("parse")
                    {
                        AllowedOptions = ScopeOptions.None,
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
