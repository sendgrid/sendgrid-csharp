namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for asm.groups
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class AsmGroups : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AsmGroups"/> class.
        /// </summary>
        public AsmGroups()
            : base("Asm Groups")
        {
            this.Scopes = new[]
            {
                "asm.groups.create",
                "asm.groups.delete",
                "asm.groups.read",
                "asm.groups.update"
            };
        }
    }
}
