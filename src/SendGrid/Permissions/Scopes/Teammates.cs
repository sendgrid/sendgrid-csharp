namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for teammates
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Teammates : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Teammates"/> class.
        /// </summary>
        public Teammates()
            : base("Teammates")
        {
            this.Scopes = new []
            {
                "teammates.create",
                "teammates.read",
                "teammates.update",
                "teammates.delete"
            };
        }
    }
}
