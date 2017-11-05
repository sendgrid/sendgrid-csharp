namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for stats
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Stats : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Stats"/> class.
        /// </summary>
        public Stats()
            : base("stats", "read")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("global", ScopeOptions.ReadOnly)
            };
        }
    }
}
