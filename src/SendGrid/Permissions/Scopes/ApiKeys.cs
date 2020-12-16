namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for api_keys
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class ApiKeys : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiKeys"/> class.
        /// </summary>
        public ApiKeys()
            : base("Api Keys")
        {
            this.Scopes = new[]
            {
                "api_keys.create",
                "api_keys.delete",
                "api_keys.read",
                "api_keys.update"
            };
        }
    }
}
