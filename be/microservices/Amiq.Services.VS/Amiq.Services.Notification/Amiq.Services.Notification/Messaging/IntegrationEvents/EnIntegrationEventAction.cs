using Amiq.Services.Common.Enums;

namespace Amiq.Services.Notification.Messaging.IntegrationEvents
{
    public enum EnIntegrationEventAction
    {
        [EnumAltValue("R")]
        Removed,
        [EnumAltValue("C")]
        Created,
        [EnumAltValue("E")]
        Edited
    }
}
