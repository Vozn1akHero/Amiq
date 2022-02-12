namespace Amiq.Services.Group.BusinessLayer.Base
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
