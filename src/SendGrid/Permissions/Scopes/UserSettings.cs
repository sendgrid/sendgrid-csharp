namespace SendGrid.Permissions.Scopes
{

    /// <summary>
    /// Scopes for user
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class UserSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserSettings"/> class.
        /// </summary>
        public UserSettings()
            : base("User Settings")
        {
            this.Scopes = new [] {
                "user.account.read",
                "user.credits.read",
                "user.email.create",
                "user.email.delete",
                "user.email.read",
                "user.email.update",
                "user.multifactor_authentication.create",
                "user.multifactor_authentication.delete",
                "user.multifactor_authentication.read",
                "user.multifactor_authentication.update",
                "user.password.read",
                "user.password.update",
                "user.profile.read",
                "user.profile.update",
                "user.settings.enforced_tls.read",
                "user.settings.enforced_tls.update",
                "user.timezone.read",
                "user.timezone.update",
                "user.username.read",
                "user.username.update"
            };
        }
    }
}
