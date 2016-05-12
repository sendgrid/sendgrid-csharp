using System.Runtime.Serialization;

namespace SendGrid.Model
{
    public enum ConditionLogicalConjunction
    {
        [EnumMember(Value = "")]
        None,
        [EnumMember(Value = "and")]
        And,
        [EnumMember(Value = "or")]
        Or
    }
}
