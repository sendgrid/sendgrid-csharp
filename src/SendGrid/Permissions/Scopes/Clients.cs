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
            : base("Clients")
        {
            this.Scopes = new[]
            {
                "clients.desktop.stats.read",
                "clients.phone.stats.read",
                "clients.stats.read",
                "clients.tablet.stats.read",
                "clients.webmail.stats.read"
            };
        }
    }
}
