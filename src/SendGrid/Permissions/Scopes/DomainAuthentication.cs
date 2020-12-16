namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for whitelabel
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class DomainAuthentication
        : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DomainAuthentication"/> class.
        /// </summary>
        public DomainAuthentication()
            : base("Domain Authentication (formerly Whitelabel)")
        {
            this.Scopes = new[]
            {
                "whitelabel.create",
                "whitelabel.delete",
                "whitelabel.read",
                "whitelabel.update"
            };
        }
    }
}
