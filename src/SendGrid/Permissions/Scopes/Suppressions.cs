namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for suppressions
    /// </summary>
    /// <remarks>Why are there suppression and suppressions?</remarks>
    /// <seealso cref="SendGridPermissionScope" />
    public class Suppressions : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Suppressions"/> class.
        /// </summary>
        public Suppressions()
            : base("Suppressions")
        {
            this.Scopes = new[]
            {
                "suppression.create",
                "suppression.delete",
                "suppression.read",
                "suppression.update",
                "suppression.bounces.create",
                "suppression.bounces.read",
                "suppression.bounces.update",
                "suppression.bounces.delete",
                "suppression.blocks.create",
                "suppression.blocks.read",
                "suppression.blocks.update",
                "suppression.blocks.delete",
                "suppression.invalid_emails.create",
                "suppression.invalid_emails.read",
                "suppression.invalid_emails.update",
                "suppression.invalid_emails.delete",
                "suppression.spam_reports.create",
                "suppression.spam_reports.read",
                "suppression.spam_reports.update",
                "suppression.spam_reports.delete",
                "suppression.unsubscribes.create",
                "suppression.unsubscribes.read",
                "suppression.unsubscribes.update",
                "suppression.unsubscribes.delete"                
            };
        }
    }
}
