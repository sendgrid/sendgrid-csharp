namespace SendGrid.Permissions.Scopes
{

    /// <summary>
    /// Scope for geo.stats
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    internal class Geo : SendGridPermissionScope
    {
        public Geo()
            : base("geo.stats", "read")
        {
            this.IsAdminOnly = true;
        }
    }
}
