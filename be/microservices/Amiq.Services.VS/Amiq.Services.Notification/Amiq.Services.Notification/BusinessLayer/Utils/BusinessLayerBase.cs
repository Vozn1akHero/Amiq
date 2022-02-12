namespace Amiq.Services.Notification.BusinessLayer.Utils
{
    public class BusinessLayerBase : IDomainService
    {
        protected void CheckBsRule(IBsRule businessRule)
        {
            if (!businessRule.CheckBsRule())
            {
                throw new BsRuleIsBrokenException(businessRule.ErrorContent);
            }
        }

        protected async Task CheckBsRuleAsync(IBsRuleAsync bsRuleAsync)
        {
            if (!await bsRuleAsync.CheckBsRuleAsync())
            {
                throw new BsRuleIsBrokenException(bsRuleAsync.ErrorContent);
            }
        }
    }
}
