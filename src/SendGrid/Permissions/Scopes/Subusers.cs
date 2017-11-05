namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for subusers
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Subusers : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subusers"/> class.
        /// </summary>
        public Subusers()
            : base("subusers")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("credits")
                {
                    SubScopes = new[] { new SendGridPermissionScope("remaining") }
                },
                new SendGridPermissionScope("monitor"),
                new SendGridPermissionScope("reputations", ScopeOptions.ReadOnly),
                new SendGridPermissionScope("stats", ScopeOptions.ReadOnly)
                {
                    SubScopes = new[]
                    {
                        new SendGridPermissionScope("monthly", ScopeOptions.ReadOnly),
                         new SendGridPermissionScope("sums", ScopeOptions.ReadOnly)
                    }
                },
                new SendGridPermissionScope("summary", ScopeOptions.ReadOnly),
            };
        }
    }
}
