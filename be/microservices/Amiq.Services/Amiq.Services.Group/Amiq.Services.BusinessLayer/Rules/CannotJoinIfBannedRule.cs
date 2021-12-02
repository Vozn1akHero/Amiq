using Amiq.Services.BusinessLayer.Base;

namespace Amiq.Services.BusinessLayer.Rules
{
    public class CannotJoinIfBannedRule : IBsRule
    {
        public string ErrorContent => throw new NotImplementedException();

        public bool CheckBsRule()
        {
            throw new NotImplementedException();
        }
    }
}
