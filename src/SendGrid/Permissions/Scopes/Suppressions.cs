namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for suppressions
    /// </summary>
    /// <remarks>Why are there suppression and suppressions?</remarks>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Suppressions : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Suppressions"/> class.
        /// </summary>
        public Suppressions()
            : base("suppressions")
        {
        }
    }
}
