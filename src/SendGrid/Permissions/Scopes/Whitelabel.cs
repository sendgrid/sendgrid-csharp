namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for whitelabel
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Whitelabel : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Whitelabel"/> class.
        /// </summary>
        public Whitelabel()
            : base("whitelabel")
        {
        }
    }
}
