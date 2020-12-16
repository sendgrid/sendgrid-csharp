namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for ips
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class IpManagement : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IpManagement"/> class.
        /// </summary>
        public IpManagement()
            : base("IPs")
        {
            this.Scopes = new[]
            {
                "ips.assigned.read",
                "ips.read",
                "ips.pools.create",
                "ips.pools.delete",
                "ips.pools.read",
                "ips.pools.update",
                "ips.pools.ips.create",
                "ips.pools.ips.delete",
                "ips.pools.ips.read",
                "ips.pools.ips.update",
                "ips.warmup.create",
                "ips.warmup.delete",
                "ips.warmup.read",
                "ips.warmup.update"
            };
        }
    }
}
