﻿using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Utils
{
    public interface IBsRuleAsync
    {
        string ErrorContent { get; }
        /// <summary>
        /// Weryfikacja reguły biznesowej
        /// </summary>
        /// <returns>prawda lub fałsz</returns>
        Task<bool> CheckBsRuleAsync();
    }
}