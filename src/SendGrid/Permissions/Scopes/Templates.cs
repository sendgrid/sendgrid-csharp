namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for templates
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Templates : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Templates"/> class.
        /// </summary>
        public Templates()
            : base("templates")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("versions")
                {
                    SubScopes = new[]
                    {
                        new SendGridPermissionScope("activate")
                    }
                }
            };
        }
    }
}
