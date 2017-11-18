namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for browser.stats
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    internal class Browsers : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Browsers"/> class.
        /// </summary>
        public Browsers()
            : base("browsers.stats", "read")
        {
        }
    }
}
