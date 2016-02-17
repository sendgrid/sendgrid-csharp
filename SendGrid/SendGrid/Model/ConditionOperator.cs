using System.Runtime.Serialization;

namespace SendGrid.Model
{
    public enum ConditionOperator
    {
        [EnumMember(Value = "contains")]
        Contains,
        [EnumMember(Value = "eq")]
        Equal,
        [EnumMember(Value = "ne")]
        NotEqual,
        [EnumMember(Value = "lt")]
        LessThan,
        [EnumMember(Value = "gt")]
        GreaterThan
    }
}
