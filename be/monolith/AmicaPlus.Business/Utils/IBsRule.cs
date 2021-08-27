namespace AmicaPlus.Business.Utils
{
    public interface IBsRule
    {
        string ErrorContent { get; }
        /// <summary>
        /// Weryfikacja reguły biznesowej
        /// </summary>
        /// <returns>true albo false</returns>
        bool CheckBsRule();
    }
}