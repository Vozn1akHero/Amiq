using System;

namespace Amiq.Services.Notification.BusinessLayer.Utils
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        { }
    }
}
