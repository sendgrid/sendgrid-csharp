namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for categories
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Categories : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Categories"/> class.
        /// </summary>
        public Categories()
            : base("Categories")
        {
            this.Scopes = new[]
            {
                "categories.create",
                "categories.delete",
                "categories.read",
                "categories.update",
                "categories.stats.read",
                "categories.stats.sums.read"
            };
        }
    }
}
