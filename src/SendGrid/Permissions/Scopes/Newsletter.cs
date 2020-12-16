namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for newsletter
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Newsletter : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Newsletter"/> class.
        /// </summary>
        public Newsletter()
            : base("newsletter")
        {
            this.Scopes = new[]
            {
                "newsletter.create",
                "newsletter.delete",
                "newsletter.read",
                "newsletter.update"
            };
        }
    }
}
