using Amiq.Services.Group.BusinessLayer.Base;

namespace Amiq.Services.Group.BusinessLayer.Rules
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
