namespace SendGrid.Permissions.Scopes
{

    /// <summary>
    /// Scopes for user
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class UserSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
            : base("user")
        {
            this.AllowedOptions = new string[0];
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("account", ScopeOptions.ReadOnly),
                new SendGridPermissionScope("credits", ScopeOptions.ReadOnly),
                new SendGridPermissionScope("email"),
                new SendGridPermissionScope("multifactor_authentication"),
                new SendGridPermissionScope("password", "read", "update"),
                new SendGridPermissionScope("profile", "read", "update"),
                new SendGridPermissionScope("settings.enforced_tls", "read", "update"),
                new SendGridPermissionScope("timezone", "read", "update"),
                new SendGridPermissionScope("username", "read", "update"),
                new Webhooks()
            };
        }
    }
}
