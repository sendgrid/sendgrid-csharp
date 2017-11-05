namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for api_keys
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class ApiKeys : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeys"/> class.
        /// </summary>
        public ApiKeys()
            : base("api_keys")
        {
        }
    }
}
