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
            : base("Templates")
        {
            this.Scopes = new[]
            {
                "templates.create",
                "templates.delete",
                "templates.read",
                "templates.update",
                "templates.versions.activate.create",
                "templates.versions.activate.delete",
                "templates.versions.activate.read",
                "templates.versions.activate.update",
                "templates.versions.create",
                "templates.versions.delete",
                "templates.versions.read",
                "templates.versions.update"
            };
        }
    }
}
