using Amiq.BusinessLayer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amiq.BusinessLayer.Group.Rules
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
