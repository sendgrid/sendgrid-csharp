namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for subusers
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class Subusers : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Subusers"/> class.
        /// </summary>
        public Subusers()
            : base("Subusers")
        {
            this.Scopes = new[]
            {
                "subusers.create",
                "subusers.delete",
                "subusers.read",
                "subusers.update",
                "subusers.credits.create",
                "subusers.credits.delete",
                "subusers.credits.read",
                "subusers.credits.update",
                "subusers.credits.remaining.create",
                "subusers.credits.remaining.delete",
                "subusers.credits.remaining.read",
                "subusers.credits.remaining.update",
                "subusers.monitor.create",
                "subusers.monitor.delete",
                "subusers.monitor.read",
                "subusers.monitor.update",
                "subusers.reputations.read",
                "subusers.stats.read",
                "subusers.stats.monthly.read",
                "subusers.stats.sums.read",
                "subusers.summary.read"
            };
        }
    }
}
