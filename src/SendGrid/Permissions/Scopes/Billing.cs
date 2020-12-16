namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for billing
    /// </summary>
    /// <remarks>
    /// The billing scope is mutually exclusive
    /// </remarks>
    /// <seealso cref="SendGridPermissionScope" />
    public class Billing : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Billing"/> class.
        /// </summary>
        public Billing()
            : base("Billing")
        {
            this.IsMutuallyExclusive = true;
            this.Scopes = new[]
            {
                "billing.create",
                "billing.delete",
                "billing.read",
                "billing.update"
            };
        }
    }
}
