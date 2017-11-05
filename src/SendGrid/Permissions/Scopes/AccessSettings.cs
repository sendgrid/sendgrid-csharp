namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for access_settings
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class AccessSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccessSettings"/> class.
        /// </summary>
        public AccessSettings()
            : base("access_settings")
        {
            this.IsAdminOnly = true;
            // no top level scopes
            this.AllowedOptions = new string[0];
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("activity", "read"),
                new SendGridPermissionScope("whitelist")
            };
        }
    }
}
