﻿namespace Amiq.Services.BusinessLayer.Base
{
    public class BsRuleIsBrokenException : Exception
    {
        public BsRuleIsBrokenException(string message) : base(message)
        { }
    }
}
