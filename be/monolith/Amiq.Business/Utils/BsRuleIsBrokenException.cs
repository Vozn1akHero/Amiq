using System;

namespace Amiq.Business.Utils
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        {}
    }
}
