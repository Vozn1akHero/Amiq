﻿namespace Amiq.Business.Utils
{
    public interface IBsRule
    {
        string ErrorContent { get; }
        /// <summary>
        /// Weryfikacja reguły biznesowej
        /// </summary>
        bool CheckBsRule();
    }
}