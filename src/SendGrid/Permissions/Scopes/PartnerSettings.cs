namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for partner_settings
    /// </summary>
    /// <seealso cref="SendGrid.Permissions.SendGridPermissionScope" />
    public class PartnerSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerSettings"/> class.
        /// </summary>
        public PartnerSettings()
            : base("partner_settings", "read")
        {
            this.SubScopes = new[]
            {
                new SendGridPermissionScope("new_relic", "read", "update"),
                new SendGridPermissionScope("sendwithus", "read", "update")
            };
        }
    }
}
