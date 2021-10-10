﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.Business.Utils
{
    public class BsServiceBase : IDomainService
    {
        protected void CheckBsRule(IBsRule businessRule){
            if (!businessRule.CheckBsRule()) {
                throw new BsRuleIsBrokenException(businessRule.ErrorContent);
            }
        }

        protected async Task CheckBsRuleAsync(IBsRuleAsync bsRuleAsync){
            if (!await bsRuleAsync.CheckBsRuleAsync())
            {
                throw new BsRuleIsBrokenException(bsRuleAsync.ErrorContent);
            }
        }
    }
}
