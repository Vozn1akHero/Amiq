using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business.Utils
{
    public class BsServiceBase : IDomainService
    {
        protected void CheckBsRule(IBsRule businessRule){
            if (businessRule.CheckBsRule()) {
                throw new BsIsBrokenException(businessRule.ErrorContent);
            }
        }
    }
}
