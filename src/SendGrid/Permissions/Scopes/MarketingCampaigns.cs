namespace SendGrid.Permissions.Scopes
{
    /// <summary>
    /// Scopes for marketing_campaigns
    /// </summary>
    /// <seealso cref="SendGridPermissionScope" />
    public class MarketingCampaigns : SendGridPermissionScope
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarketingCampaigns"/> class.
        /// </summary>
        public MarketingCampaigns()
            : base("Marketing Campaigns")
        {
            this.Scopes = new[]
            {
                "marketing_campaigns.create",
                "marketing_campaigns.delete",
                "marketing_campaigns.read",
                "marketing_campaigns.update"
            };
        }
    }
}
