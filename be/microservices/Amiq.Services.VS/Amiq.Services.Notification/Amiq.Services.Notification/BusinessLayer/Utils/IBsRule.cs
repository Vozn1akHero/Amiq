namespace Amiq.Services.Notification.BusinessLayer.Utils
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