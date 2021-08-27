using AmicaPlus.Business.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmicaPlus.Business.Group.Rules
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
