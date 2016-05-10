using System.ComponentModel;

namespace SendGrid.Model
{
    public enum CampaignStatus
    {
        [Description("draft")]
        Draft,
        [Description("scheduled")]
        Scheduled,
        [Description("sent")]
        Sent
    }
}
