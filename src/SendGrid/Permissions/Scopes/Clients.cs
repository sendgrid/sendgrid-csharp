namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for clients
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Clients : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clients"/> class.
        /// </summary>
        public Clients()
            : base("clients", "read")
        {
            this.AllowedOptions = ScopeOptions.None;
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("desktop.stats", "read"),
                new SendGridPermissionScope("phone.stats", "read"),
                new SendGridPermissionScope("stats", "read"),
                new SendGridPermissionScope("tablet.stats", "read"),
                new SendGridPermissionScope("webmail.stats", "read")
            };
        }
    }
}
