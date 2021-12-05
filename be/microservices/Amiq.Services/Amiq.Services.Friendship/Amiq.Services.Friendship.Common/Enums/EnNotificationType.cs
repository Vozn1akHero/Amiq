using Amiq.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Services.Friendship.Common.Enums
{
    public enum EnNotificationType
    {
        [EnumAltValue("GroupPost")]
        GP,
        [EnumAltValue("UserPost")]
        UP
    }
}
