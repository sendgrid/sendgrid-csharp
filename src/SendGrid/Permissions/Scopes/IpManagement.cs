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
            : base("ips", "read")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("assigned", "read"),
                new SendGridPermissionScope("pools")
                {
                    SubScopes = new[]
                    {
                        new SendGridPermissionScope("ips")
                    }
                },
                new SendGridPermissionScope("warmup"),
            };
        }
    }
}
