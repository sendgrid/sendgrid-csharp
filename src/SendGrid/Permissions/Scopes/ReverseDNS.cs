namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for access_settings
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class ReverseDNS
        : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReverseDNS"/> class.
        /// </summary>
        public ReverseDNS()
            : base("Reverse DNS (formerly Whitelist)")
        {
            this.Scopes = new string[]
            {
                "access_settings.activity.read",
                "access_settings.whitelist.create",
                "access_settings.whitelist.delete",
                "access_settings.whitelist.read",
                "access_settings.whitelist.update"
            };
        }
    }
}
