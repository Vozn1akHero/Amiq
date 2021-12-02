namespace Amiq.Services.BusinessLayer.Base
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
