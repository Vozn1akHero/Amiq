using System;

namespace AmicaPlus.Business.Utils
{
    public class BsIsBrokenException : Exception
    {
        public BsIsBrokenException(string message) : base(message)
        {}
    }
}
