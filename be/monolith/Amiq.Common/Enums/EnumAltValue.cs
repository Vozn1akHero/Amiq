using System;

namespace Amiq.Common.Enums
{
    public class EnumAltValue : Attribute
    {
        public string Value { get; set; }

        public EnumAltValue(string value)
        {
            Value = value;
        }
    }
}
