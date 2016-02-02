using System.ComponentModel;

namespace SendGrid.Model
{
    public enum AggregateBy
    {
        [Description("none")]
        None,
        [Description("day")]
        Day,
        [Description("week")]
        Week,
        [Description("month")]
        Month
    }
}
