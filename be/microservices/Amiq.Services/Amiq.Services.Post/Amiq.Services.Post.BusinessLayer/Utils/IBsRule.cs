namespace Amiq.Services.Post.BusinessLayer.Utils
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