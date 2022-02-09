using System;

namespace Amiq.Services.Post.BusinessLayer.Utils
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        { }
    }
}
