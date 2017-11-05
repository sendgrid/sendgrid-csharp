namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for asm.groups
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class AsmGroups : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsmGroups"/> class.
        /// </summary>
        public AsmGroups()
            : base("asm.groups")
        {
        }
    }
}
