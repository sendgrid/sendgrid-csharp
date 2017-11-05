namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for teammates
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Teammates : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Teammates"/> class.
        /// </summary>
        public Teammates()
            : base("teammates")
        {
        }
    }
}
