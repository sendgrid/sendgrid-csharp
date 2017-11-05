namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for clients
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Clients : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Clients"/> class.
        /// </summary>
        public Clients()
            : base("clients", "read")
        {
            this.IsAdminOnly = true;
            this.AllowedOptions = new string[0];
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
