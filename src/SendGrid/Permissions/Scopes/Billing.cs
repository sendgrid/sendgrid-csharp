namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for billing
    /// </summary>
    /// <remarks>
    /// The billing scope is mutually exclusive
    /// </remarks>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class Billing : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Billing"/> class.
        /// </summary>
        public Billing()
            : base("billing")
        {
            this.IsMutuallyExclusive = true;
        }
    }
}
