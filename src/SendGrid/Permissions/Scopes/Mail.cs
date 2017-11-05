namespace SendGrid.Permissions.Scopes
{

    /// <summary>
    /// Scopes for mail
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Mail : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mail"/> class.
        /// </summary>
        public Mail()
            : base("mail")
        {
            this.AllowedOptions = new[] { "send" };
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("batch")
            };
        }
    }
}
