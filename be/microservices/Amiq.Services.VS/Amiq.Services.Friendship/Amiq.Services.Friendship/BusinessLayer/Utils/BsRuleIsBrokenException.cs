using System;

namespace Amiq.Services.Friendship.BusinessLayer.Utils
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        { }
    }
}
