using System;
using System.Reflection;

namespace Amiq.Common.Enums
{
    public static class EnumExtensions
    {
        private const string ENUM_DOESNT_CONTAIN_STRING_ERROR = "Passed value is not contained by enum";

        public static string GetEnumAltValue(this Enum @enum)
        {
            var type = @enum.GetType();
            var memInfo = type.GetMember(@enum.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(EnumAltValue), false);
            if(attributes.Length > 0)
            {
                return ((EnumAltValue)attributes[0]).Value;
            }
            return @enum.ToString();
        }

        public static string TryMapStrValueToAltValue(Type type, string value)
        {
            var members = type.GetEnumNames();
            foreach(var member in members)
            {
                var memInfo = type.GetMember(member)[0];
                EnumAltValue attr = ((EnumAltValue)memInfo.GetCustomAttributes(typeof(EnumAltValue), false)[0]);
                if (attr != null && attr.Value.Equals(value))
                    return attr.Value;
            }
            throw new ArgumentOutOfRangeException(ENUM_DOESNT_CONTAIN_STRING_ERROR);
        }

        public static string TryMapStrValueToMember(Type type, string value)
        {
            var members = type.GetEnumNames();
            foreach (var member in members)
            {
                var memInfo = type.GetMember(member);
                string enValue = memInfo[0].Name;   
                if (!string.IsNullOrEmpty(enValue))
                    return enValue;
            }
            throw new ArgumentOutOfRangeException(ENUM_DOESNT_CONTAIN_STRING_ERROR);
        }
    }
}
