using System;

namespace Amiq.Business.Utils
{
    public class BsIsBrokenException : Exception
    {
        public BsIsBrokenException(string message) : base(message)
        {}
    }
}
