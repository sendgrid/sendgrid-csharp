namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for mail
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Mail : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Mail"/> class.
        /// </summary>
        public Mail()
            : base("Mail")
        {
            this.Scopes = new[]
            {
                "mail.batch.create",
                "mail.batch.delete",
                "mail.batch.read",
                "mail.batch.update",
                "mail.send"
            };
        }
    }
}
