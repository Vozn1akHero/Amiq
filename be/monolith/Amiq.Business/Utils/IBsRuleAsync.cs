using System.Threading.Tasks;

namespace Amiq.Business.Utils
{
    public interface IBsRuleAsync
    {
        string TextContent { get; }
        /// <summary>
        /// Weryfikacja reguły biznesowej
        /// </summary>
        /// <returns>prawda lub fałsz</returns>
        Task<bool> CheckBsRuleAsync();
    }
}