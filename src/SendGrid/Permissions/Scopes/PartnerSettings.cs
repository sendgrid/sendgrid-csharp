namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for partner_settings
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class PartnerSettings : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartnerSettings"/> class.
        /// </summary>
        public PartnerSettings()
            : base("Partner Setings")
        {
            this.Scopes = new[]
            {
                "partner_settings.new_relic.read",
                "partner_settings.new_relic.update",
                "partner_settings.read"                
            };
        }
    }
}
