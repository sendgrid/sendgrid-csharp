namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for categories
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Categories : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Categories"/> class.
        /// </summary>
        public Categories()
            : base("categories")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("stats", ScopeOptions.ReadOnly)
                {
                    SubScopes = new[] { new SendGridPermissionScope("sums", ScopeOptions.ReadOnly) }
                }
            };
        }
    }
}
