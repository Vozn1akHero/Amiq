using System;

namespace Amiq.Services.User.BusinessLayer.Utils
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        {}
    }
}
