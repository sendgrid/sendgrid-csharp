namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for credentials
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Credentials : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Credentials"/> class.
        /// </summary>
        public Credentials()
            : base("credentials")
        {
        }
    }
}
