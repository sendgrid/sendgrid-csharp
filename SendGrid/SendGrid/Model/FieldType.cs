using System.Runtime.Serialization;

namespace SendGrid.Model
{
    public enum FieldType
    {
        [EnumMember(Value = "date")]
        Date,
        [EnumMember(Value = "text")]
        Text,
        [EnumMember(Value = "number")]
        Number
    }
}
